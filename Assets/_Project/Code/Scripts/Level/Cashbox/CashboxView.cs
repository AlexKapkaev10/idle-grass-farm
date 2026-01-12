using TMPro;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public class CashboxView : MonoBehaviour, IInteractable
    {
        [SerializeField] private TMP_Text _textCounter;
        
        private QueuePoint[] _queuePoints;
        private ICashboxController _controller;

        [Inject]
        private void Construct(ICashboxController controller)
        {
            _controller = controller;
            _queuePoints = GetComponentsInChildren<QueuePoint>();
            _controller.SetQueuePoints(_queuePoints);

            SetCounterText(null);
        }

        public void Enter()
        {
            _controller.BalanceUpdates += OnBalanceUpdates;
            _controller.Enter();
        }

        private void OnBalanceUpdates(string text, Color color)
        {
            SetCounterText(text);
            _textCounter.color = color;
        }

        public void Exit()
        {
            _controller.Exit();
            SetCounterText(null);
        }

        private void SetCounterText(string text)
        {
            _textCounter.SetText(text);
        }
    }
}