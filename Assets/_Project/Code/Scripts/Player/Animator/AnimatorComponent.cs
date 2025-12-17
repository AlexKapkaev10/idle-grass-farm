using UnityEngine;

namespace Project.Game
{
    public class AnimatorComponent : MonoBehaviour, IAnimatorComponent
    {
        [SerializeField] private Animator _animator;
        public void SetBool(int id, bool value)
        {
            _animator.SetBool(id, value);
        }
    }
}