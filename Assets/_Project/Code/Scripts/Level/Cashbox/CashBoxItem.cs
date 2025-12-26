using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public class CashBoxItem : MonoBehaviour, IInteractable
    {
        private IInventoryService _inventoryService;

        [Inject]
        private void Construct(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void Enter()
        {
            _inventoryService.TrySold(ResourceType.First);
            _inventoryService.TrySold(ResourceType.Second);
        }

        public void Exit()
        {
            
        }
    }
}