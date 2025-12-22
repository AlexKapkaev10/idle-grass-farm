using System;
using UnityEngine;
using VContainer.Unity;

namespace Project.Services
{
    public interface IPlayerService : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        event Action Mowed;
        public Transform Transform { get; }
        public Transform BodyTransform { get; }
        void SetMow(int animationID, bool isActive);
    }
}