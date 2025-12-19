using UnityEngine;

namespace Project.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        [field: SerializeField] public Movement Movement { get; private set; }
        [field: SerializeField] public AnimatorComponent AnimatorComponent { get; private set; }
        [field: SerializeField] public Transform GroundTransform { get; private set; }
        [field: SerializeField] public Transform ToolParent { get; private set; }
        [field: SerializeField] public Transform ToolRangeTransform { get; private set; }
        public Transform Transform => transform;
    }
}