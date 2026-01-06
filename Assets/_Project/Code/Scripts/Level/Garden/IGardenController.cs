using System;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Game
{
    public interface IGardenController : IDisposable
    {
        void Initialize(IGardenItem[] items, AudioSource audioSource, GardenConfig config);
        void Enter();
        void Exit();
    }
}