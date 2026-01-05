using System;
using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(AbilityServiceConfig), menuName = "Config/Service/Ability")]
    public class AbilityServiceConfig : ScriptableObject
    {
        [SerializeField] private UpgradeAbilityRecipe[] _toolAbilityRecipes;
        [SerializeField] private UpgradeAbilityRecipe[] _inventoryAbilityRecipes;
        
        private const string _saveInventoryLevelKey = "InventoryLevelSaveKey";
        private const string _saveToolLevelKey = "ToolLevelSaveKey";
        
        public string GetSaveInventoryLevelKey()
        {
            return _saveInventoryLevelKey;
        }

        public string GetSaveToolLevelKey()
        {
            return _saveToolLevelKey;
        }

        public float GetToolRangeByLevel(int level)
        {
            foreach (var data in _toolAbilityRecipes)
            {
                if (level == data.Level)
                {
                    return data.Value;
                }
            }
            
            return 0.0f;
        }

        public UpgradeAbilityRecipe GetRecipe(AbilityType type, int level)
        {
            var recipeCollection = type == AbilityType.Tool 
                ? _toolAbilityRecipes 
                : _inventoryAbilityRecipes;

            foreach (var recipe in recipeCollection)
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
    public class UpgradeAbilityRecipe
    {
        public int Level;
        public int FirstCurrencyAmount;
        public int SecondCurrencyAmount;
        public float Value;
    }
}