using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(InventoryServiceConfig), menuName = "Config/Service/Inventory")]
    public class InventoryServiceConfig : ScriptableObject
    {
        [SerializeField] private int _minRandomPrice = 2;
        [SerializeField] private int _maxRandomPrice = 12;
        public int GetRandomPrice()
        {
            return Random.Range(_minRandomPrice, _maxRandomPrice);
        }
    }
}