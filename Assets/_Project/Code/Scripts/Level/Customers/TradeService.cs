using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.ScriptableObjects;
using Project.Services;
using VContainer;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Game
{
    public interface ITradeService : IDisposable
    {
        void Start();
    }

    public class TradeService : ITradeService
    {
        private readonly ICashboxController _cashboxController;
        private readonly IInventoryService _inventoryService;
        private readonly TradeServiceConfig _config;

        private readonly CancellationTokenSource _cts = new();

        private readonly List<Customer> _customers = new();

        private Customer _firstCustomer;
        private bool _hasPlayer;

        [Inject]
        public TradeService(
            ICashboxController cashboxController,
            IInventoryService inventoryService,
            TradeServiceConfig config)
        {
            _cashboxController = cashboxController;
            _inventoryService = inventoryService;
            _config = config;
        }

        public void Start()
        {
            _cashboxController.PlayerEntered += OnPlayerEnter;

            RunSpawnLoop(_cts.Token).Forget();
        }

        public void Dispose()
        {
            if (_firstCustomer != null)
            {
                _firstCustomer.Arrived -= OnFirstCustomerArrived;
                _firstCustomer = null;
            }

            _cashboxController.PlayerEntered -= OnPlayerEnter;

            _cts.Cancel();
            _cts.Dispose();
        }

        private void OnPlayerEnter(bool isEnter)
        {
            _hasPlayer = isEnter;

            if (_hasPlayer)
            {
                TrySellToFirstCustomer();
            }
        }
        
        private async UniTaskVoid RunSpawnLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var maxQueue = _cashboxController.QueuePointsCount;

                if (maxQueue <= 0)
                {
                    await UniTask.Yield(token);
                    continue;
                }

                if (_customers.Count >= maxQueue)
                {
                    await UniTask.WaitUntil(() => _customers.Count < maxQueue, cancellationToken: token);
                    continue;
                }

                var delay = Random.Range(_config.SpawnDelayMin, _config.SpawnDelayMax);
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

                if (_customers.Count >= maxQueue)
                {
                    continue;
                }

                SpawnOneCustomer();
            }
        }

        private void SpawnOneCustomer()
        {
            var point = _cashboxController.GetFreeQueuePoint();
            if (point == null)
            {
                return;
            }

            var customer = Object.Instantiate(_config.CustomerPrefab);
            customer.Initialize(point, _config.CustomerSpawnPosition);

            _customers.Add(customer);

            RebuildQueue();
        }
        
        private void TrySellToFirstCustomer()
        {
            if (_firstCustomer == null)
            {
                return;
            }

            if (_firstCustomer.IsMoving)
            {
                return;
            }

            var isSold = _inventoryService.TrySold(_firstCustomer.GetWishfulType());
            if (!isSold)
            {
                return;
            }

            var leavingCustomer = _firstCustomer;
            leavingCustomer.Arrived -= OnFirstCustomerArrived;

            leavingCustomer.ReleasePoint();
            leavingCustomer.StartMove(_config.CustomerExitPosition, true);

            _customers.Remove(leavingCustomer);
            _firstCustomer = null;

            RebuildQueue();
            
            if (_hasPlayer)
            {
                TrySellToFirstCustomer();
            }
        }

        private void OnFirstCustomerArrived()
        {
            if (_hasPlayer)
            {
                TrySellToFirstCustomer();
            }
        }

        private void RebuildQueue()
        {
            if (_cashboxController.QueuePoints == null)
            {
                return;
            }

            if (_firstCustomer != null)
            {
                _firstCustomer.Arrived -= OnFirstCustomerArrived;
                _firstCustomer = null;
            }

            foreach (var c in _customers)
            {
                c.ReleasePoint();
            }

            foreach (var c in _customers)
            {
                var point = _cashboxController.GetFreeQueuePoint();
                if (point == null)
                {
                    break;
                }

                c.SetPoint(point);

                if (point.IsPayPoint())
                {
                    _firstCustomer = c;
                    _firstCustomer.Arrived += OnFirstCustomerArrived;
                }
            }
        }
    }
}
