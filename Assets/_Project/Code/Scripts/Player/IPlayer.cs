using UnityEngine;

namespace Project.Game
{
    public interface IPlayer
    {
        Movement Movement { get; }
        AnimatorComponent AnimatorComponent { get; }
        Transform Transform { get; }
        Transform GroundTransform { get;}
        public Transform ToolParent { get; }
        public Transform ToolRangeTransform { get; }
    }
}