using Project.ScriptableObjects;

namespace Project.Game
{
    public interface IAbilityService
    {
        float MowRange { get; }
        int GetNextLevel(AbilityType type);
        void UpdateLevel(AbilityType type);
        void TryUpgrade(AbilityType type, UpgradeRecipe recipe);
    }
}