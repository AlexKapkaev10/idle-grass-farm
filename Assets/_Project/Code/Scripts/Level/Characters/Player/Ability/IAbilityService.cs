using System;

namespace Project.Game
{
    public interface IAbilityService : IDisposable
    {
        event Action<AbilityType> AbilitiesUpdated;
        float GetMowRange();
        void UpdateLevel(AbilityType type);
        int GetLevelByType(AbilityType type);
        bool HasUpgrade(AbilityType type);
    }
}