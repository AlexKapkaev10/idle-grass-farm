using UnityEngine;

namespace Project.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        [field: SerializeField] public Movement Movement { get; private set; }
        [field: SerializeField] public AnimatorComponent AnimatorComponent { get; private set; }
        public Transform Transform => transform;
    }
}