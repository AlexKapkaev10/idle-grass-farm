using UnityEngine;

namespace Project.Input
{
    public interface IInputService
    {
        void SwitchMap(InputMapType type);
        Vector3 MoveDirection { get; }
    }
}