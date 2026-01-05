using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(CustomerConfig), menuName = "Config/Level/Customer")]
    public class CustomerConfig : ScriptableObject
    {
        [SerializeField] private string _moveAnimationName;
        
        public int MoveAnimationID => Animator.StringToHash(_moveAnimationName);
    }
}