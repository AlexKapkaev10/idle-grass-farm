namespace Project.Game
{
    public interface IAbilityService
    {
        float MowRange { get; }
        void UpdateLevel(AbilityType type);
    }
}