using System;

namespace Project.Game
{
    public interface ICashboxController
    {
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
        public QueuePoint[] QueuePoints { get; private set; }
        public int QueuePointsCount => QueuePoints.Length;

        public event Action<bool> SellerEntered;

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
    }
}