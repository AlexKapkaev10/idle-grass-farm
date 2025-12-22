using System;
using Project.ScriptableObjects;

namespace Project.Game
{
    public interface IGardenController : IDisposable
    {
        void Initialize(IGardenItem[] items, GardenConfig config);
        void Enter();
        void Exit();
    }
}