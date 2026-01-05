using System;
using UnityEngine;

namespace Project.Game
{
    public interface IMovement
    {
        event Action<bool> Running;
        void Move(Vector3 direction, float speed);
        void UpdateRotation(Vector3 direction, float speed);
    }
}