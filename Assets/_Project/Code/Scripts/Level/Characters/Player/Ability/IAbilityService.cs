namespace Project.Game
{
    public interface IAbilityService
    {
        float GetMowRange();
        void UpdateLevel(AbilityType type);
        bool HasUpgrade(AbilityType type);
    }
}