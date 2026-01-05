using System;
using UnityEngine;

namespace Project.Game
{
    public interface INavMeshMoveModel
    {
        event Action Arrived;
        event Action ExitReached;
        event Action<bool> MoveStateChanged;
        bool IsMoving { get; }
        void PlaceOn(Vector3 position, float sampleRadius = 2f);
        void StartMove(Vector3 destination, bool isExit = false);
        void Dispose();
    }
}