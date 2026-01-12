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
        event Action Traded;
        void Initialize();
    }

    public sealed class TradeService : ITradeService
    {
        private readonly ICashboxController _cashboxController;
        private readonly IInventoryService _inventoryService;
        private readonly IAudioService _audioService;
        private readonly TradeServiceConfig _config;
        private readonly CancellationTokenSource _cts = new();
        private readonly List<Customer> _customers = new();

        private Customer _firstCustomer;
        private bool _hasSeller;

        public event Action Traded;

        [Inject]
        public TradeService(
            ICashboxController cashboxController,
            IInventoryService inventoryService,
            IAudioService audioService,
            TradeServiceConfig config)
        {
            _cashboxController = cashboxController;
            _inventoryService = inventoryService;
            _audioService = audioService;
            _config = config;
        }

        public void Initialize()
        {
            _cashboxController.SellerEntered += OnSellerEnter;

            LoopCustomersAsync(_cts.Token).Forget();
        }

        public void Dispose()
        {
            if (_firstCustomer != null)
            {
                _firstCustomer.GetMoveModel().Arrived -= OnFirstCustomerArrived;
                _firstCustomer = null;
            }

            _cashboxController.SellerEntered -= OnSellerEnter;

            _cts.Cancel();
            _cts.Dispose();
        }

        private void OnSellerEnter(bool isEnter)
        {
            _hasSeller = isEnter;

            if (_hasSeller)
            {
                TrySell();
            }
        }
        
        private async UniTaskVoid LoopCustomersAsync(CancellationToken token)
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

                SpawnCustomer();
            }
        }

        private void SpawnCustomer()
        {
            var point = _cashboxController.GetFreeQueuePoint();
            if (point == null)
            {
                return;
            }

            var customer = Object.Instantiate(_config.GetRandomCustomer());
            
            var random = Random.Range(0, 2);
            var wishfulType = (ResourceType)random;
            
            customer.Initialize(point, _config.CustomerSpawnPosition, wishfulType);
            customer.SetEmoji(_config.GetResourceSprite(wishfulType));

            _customers.Add(customer);

            RebuildQueue();
        }
        
        private void TrySell()
        {
            if (_firstCustomer == null)
            {
                return;
            }

            if (_firstCustomer.GetMoveModel().IsMoving)
            {
                return;
            }

            var isSold = _inventoryService.TrySold(_firstCustomer.GetWishfulType());
            if (!isSold)
            {
                return;
            }
            
            Sell();
        }

        private void Sell()
        {
            _firstCustomer.SetEmoji(_config.GetRandomEmoji());
            _firstCustomer.ReleasePoint();
            _firstCustomer.StartMove(_config.CustomerExitPosition, true);

            _customers.Remove(_firstCustomer);
            _firstCustomer = null;

            RebuildQueue();
            
            _audioService.PlayClip(_config.SellAudioClip);
        }

        private void OnFirstCustomerArrived()
        {
            _firstCustomer.GetMoveModel().Arrived -= OnFirstCustomerArrived;
            
            if (_hasSeller)
            {
                TrySell();
            }
        }

        private void RebuildQueue()
        {
            if (_firstCustomer != null)
            {
                _firstCustomer.GetMoveModel().Arrived -= OnFirstCustomerArrived;
                _firstCustomer = null;
            }

            foreach (var customer in _customers)
            {
                customer.ReleasePoint();
                
                var point = _cashboxController.GetFreeQueuePoint();
                if (point == null)
                {
                    break;
                }

                customer.SetPoint(point);

                if (!point.IsPayPoint())
                {
                    continue;
                }
                
                _firstCustomer = customer;
                _firstCustomer.GetMoveModel().Arrived += OnFirstCustomerArrived;
            }
        }
    }
}
