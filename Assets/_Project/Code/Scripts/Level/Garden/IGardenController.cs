using Project.ScriptableObjects;

namespace Project.Game
{
    public interface IGardenController
    {
        void Initialize(IGardenItem[] items, GardenConfig config);
        void Enter();
        void Exit();
    }
}