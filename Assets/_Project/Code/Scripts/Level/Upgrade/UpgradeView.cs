using Project.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Project.Game
{
    public sealed class UpgradeView : MonoBehaviour, IInteractable
    {
        [SerializeField] private UpgradeConfig _config;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private UpgradeViewItem _viewItem;
        
        private IUpgradeController _controller;

        [Inject]
        private void Construct(IUpgradeController controller)
        {
            _controller = controller;
            _controller.Initialize(_viewItem, _config);
            _meshRenderer.material = _config.Material;
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