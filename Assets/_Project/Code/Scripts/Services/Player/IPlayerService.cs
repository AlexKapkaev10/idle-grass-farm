using System;
using UnityEngine;
using VContainer.Unity;

namespace Project.Services
{
    public interface IPlayerService : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        event Action Mowed;
        public Transform Transform { get; }
        void SetMow(int animationID, bool isActive);
    }
}