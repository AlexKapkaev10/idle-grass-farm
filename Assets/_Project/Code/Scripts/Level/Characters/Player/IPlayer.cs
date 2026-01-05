using UnityEngine;

namespace Project.Game
{
    public interface IPlayer
    {
        Movement Movement { get; }
        AnimatorComponent AnimatorComponent { get; }
        Transform Transform { get; }
        Transform BodyTransform { get; }
        Transform GroundTransform { get;}
        Transform ToolParent { get; }
        Transform ToolRangeTransform { get; }
    }
}