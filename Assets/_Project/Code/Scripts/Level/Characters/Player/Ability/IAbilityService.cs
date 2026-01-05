using System;

namespace Project.Game
{
    public interface IAbilityService : IDisposable
    {
        float GetMowRange();
        void UpdateLevel(AbilityType type);
        bool HasUpgrade(AbilityType type);
    }
}