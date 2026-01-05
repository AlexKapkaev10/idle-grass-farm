using UnityEngine;

namespace Project.Game
{
    public class AnimatorComponent : MonoBehaviour, IAnimatorComponent
    {
        [SerializeField] private Animator _animator;
        [field: SerializeField] public AnimationEventsReceiver EventsReceiver { get; private set; }

        public void SetBool(int id, bool value)
        {
            _animator.SetBool(id, value);
        }

        public void SetTrigger(int id)
        {
            _animator.SetTrigger(id);
        }
    }
}