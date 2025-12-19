using VContainer;

namespace Project.Game
{
    public interface IAbilityService
    {
        float MowRange { get; }
    }
    
    public class AbilityService : IAbilityService
    {
        public float MowRange { get; private set; }

        [Inject]
        public AbilityService()
        {
            MowRange = 1.5f;
        }
    }
}