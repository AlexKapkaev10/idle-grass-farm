using System;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(InventoryServiceConfig), menuName = "Config/Service/Inventory")]
    public class InventoryServiceConfig : ScriptableObject
    {
        [SerializeField] public ResourceData[] _resourceDates;

        public int GetMaxAmountByLevel(int level)
        {
            foreach (var data in _resourceDates)
            {
                if (level == data.Level)
                {
                    return data.MaxAmount;
                }
            }
            
            return 0;
        }
    }

    [Serializable]
    public struct ResourceData
    {
        public int Level;
        public int MaxAmount;
    }
}