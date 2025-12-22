using Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public sealed class GardenView : MonoBehaviour, IInteractable
    {
        [SerializeField] private GardenConfig _config;
        
        private IGardenController _controller;

        [Inject]
        private void Construct(IGardenController controller)
        {
            _controller = controller;
            _controller.Initialize(GetComponentsInChildren<IGardenItem>(), _config);
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