using System;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(AbilityServiceConfig), menuName = "Config/Service/Ability")]
    public class AbilityServiceConfig : ScriptableObject
    {
        [SerializeField] private AbilityData[] _abilityDates;

        [field: SerializeField] public int StartToolLevel { get; private set; }

        public float GetToolRangeByLevel(int level)
        {
            foreach (var data in _abilityDates)
            {
                if (level == data.Level)
                {
                    return data.Value;
                }
            }
            
            return 0.0f;
        }
    }

    [Serializable]
    public struct AbilityData
    {
        public int Level;
        public float Value;
    }
}