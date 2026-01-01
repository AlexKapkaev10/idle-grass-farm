using System;
using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(UpgradeConfig), menuName = "Config/Level/Upgrade")]
    public class UpgradeConfig : ScriptableObject
    {
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public UpgradeRecipe[] Recipe { get; private set; }

        public UpgradeRecipe GetRecipeByLevel(int level)
        {
            foreach (var recipe in Recipe)
            {
                if (recipe.Level == level)
                {
                    return recipe;
                }
            }
            
            return null;
        }
    }

    [Serializable]
    public class UpgradeRecipe
    {
        public int Level;
        public int FirstCurrencyAmount;
        public int SecondCurrencyAmount;
    }
}