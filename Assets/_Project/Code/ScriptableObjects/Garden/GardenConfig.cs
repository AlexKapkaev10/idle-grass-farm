using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(GardenConfig), menuName = "Config/Level/Garden")]
    public class GardenConfig : ScriptableObject
    {
        [field: SerializeField] public Material Material { get; set; }
        [SerializeField] private string _animationName;
        public int InteractAnimation => Animator.StringToHash(_animationName);
    }
}