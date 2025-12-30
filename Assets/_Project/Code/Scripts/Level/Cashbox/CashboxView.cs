using UnityEngine;
using VContainer;

namespace Project.Game
{
    public class CashboxView : MonoBehaviour, IInteractable
    {
        private QueuePoint[] _queuePoints;
        private ICashboxController _controller;

        [Inject]
        private void Construct(ICashboxController controller)
        {
            _controller = controller;
            _queuePoints = GetComponentsInChildren<QueuePoint>();
            _controller.SetQueuePoints(_queuePoints);
        }

        public void Enter()
        {
            _controller.Enter();
        }

        public void Exit()
        {
            _controller.Exit();
        }
    }
}