using UnityEngine;

namespace Project.Game
{
    public interface IAnimatorComponent
    {
        void SetBool(int id, bool value);
    }
    
    public class AnimatorComponent : MonoBehaviour, IAnimatorComponent
    {
        private readonly int Name = Animator.StringToHash("name");
        [SerializeField] private Animator _animator;
        public void SetBool(int id, bool value)
        {
            _animator.SetBool(name, value);
        }
    }
}