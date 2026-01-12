using System;
using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public interface ICashboxController : IDisposable
    {
        event Action<string, Color> BalanceUpdates;
        event Action<bool> SellerEntered;
        QueuePoint[] QueuePoints { get; }
        int QueuePointsCount { get; }
        void Enter();
        void Exit();
        void SetQueuePoints(QueuePoint[] queuePoints);
        QueuePoint GetFreeQueuePoint();
    }

    public class CashboxController : ICashboxController
    {
        private readonly IBankService _bankService;
        
        public QueuePoint[] QueuePoints { get; private set; }
        public int QueuePointsCount => QueuePoints.Length;

        public event Action<string, Color> BalanceUpdates;
        public event Action<bool> SellerEntered;

        [Inject]
        public CashboxController(IBankService bankService)
        {
            _bankService = bankService;
            
            _bankService.BankUpdated += OnBankUpdated;
        }

        private void OnBankUpdated(BankMessageData obj)
        {
            var value = obj.NewAmount - obj.OldAmount;
            var text = "";
            var positive = value > 0;
            if (positive)
            {
                text = $"+{value}";
            }
            
            BalanceUpdates?.Invoke(text, positive ? Color.green : Color.red);
        }

        public void SetQueuePoints(QueuePoint[] queuePoints)
        {
            QueuePoints = queuePoints;
        }

        public void Enter()
        {
            SellerEntered?.Invoke(true);
        }

        public void Exit()
        {
            SellerEntered?.Invoke(false);
        }

        public QueuePoint GetFreeQueuePoint()
        {
            if (QueuePoints == null)
            {
                return null;
            }

            foreach (var point in QueuePoints)
            {
                if (!point.IsBusy)
                {
                    return point;
                }
            }

            return null;
        }

        public void Dispose()
        {
            _bankService.BankUpdated -= OnBankUpdated;
        }
    }
}