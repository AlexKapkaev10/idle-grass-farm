using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.ScriptableObjects;
using Project.Services;
using VContainer;
using Object = UnityEngine.Object;

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
        private readonly CancellationTokenSource _cts = new ();
        private readonly List<Customer> _customers = new ();

        private Customer _firstCustomer;
        private bool _hasPlayer;

        [Inject]
        public TradeService(ICashboxController cashboxController, 
            IInventoryService inventoryService, 
            TradeServiceConfig config)
        {
            _config = config;
            _cashboxController = cashboxController;
            _inventoryService = inventoryService;
        }

        public void Start()
        {
            _cashboxController.PlayerEntered += OnPlayerEnter;
            SpawnCustomersAsync().Forget();
        }

        private void OnPlayerEnter(bool isEnter)
        {
            _hasPlayer = isEnter;

            SoldResource();
        }

        private void SoldResource()
        {
            if (_firstCustomer && !_firstCustomer.IsMoving)
            {
                var isSold = _inventoryService.TrySold(ResourceType.First);

                if (isSold)
                {
                    _firstCustomer.ReleasePoint();

                    _firstCustomer.StartMove(_config.CustomerExitPosition, true);
                    _customers.Remove(_firstCustomer);
                    _firstCustomer = null;

                    if (_customers.Count == 0)
                    {
                        SpawnCustomersAsync().Forget();
                        return;
                    }

                    RebuildQueue();
                    
                    if (_hasPlayer)
                    {
                        SoldResource();
                    }
                }
            }
        }
        
        private void RebuildQueue()
        {
            if (_firstCustomer != null)
            {
                _firstCustomer.Arrived -= OnFirstCustomerArrived;
                _firstCustomer = null;
            }
            
            foreach (var customer in _customers)
            {
                customer.ReleasePoint();
            }
            
            foreach (var customer in _customers)
            {
                var point = _cashboxController.GetFreeQueuePoint();
                if (point == null)
                {
                    break;
                }

                customer.SetPoint(point);

                if (point.IsPayPoint())
                {
                    _firstCustomer = customer;
                    _firstCustomer.Arrived += OnFirstCustomerArrived;
                }
            }
        }

        private void OnFirstCustomerArrived()
        {
            _firstCustomer.Arrived -= OnFirstCustomerArrived;

            if (_hasPlayer)
            {
                SoldResource();
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cashboxController.PlayerEntered -= OnPlayerEnter;
        }

        private async UniTask SpawnCustomersAsync()
        {
            foreach (var point in _cashboxController.QueuePoints)
            {
                await Task.Delay(TimeSpan.FromSeconds(_config.CustomerSpawnDelay), _cts.Token);
                
                var customer = Object.Instantiate(_config.CustomerPrefab);

                if (point.IsPayPoint())
                {
                    _firstCustomer = customer;
                    _firstCustomer.Arrived += OnFirstCustomerArrived;
                }
                
                customer.Initialize(point, _config.CustomerSpawnPosition);
                
                _customers.Add(customer);
            }
        }
    }
}