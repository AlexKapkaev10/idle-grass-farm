using UnityEngine;

namespace Project.Game
{
    public interface IPlayer
    {
        Movement Movement { get; }
        AnimatorComponent AnimatorComponent { get; }
        Transform Transform { get; }
    }
}