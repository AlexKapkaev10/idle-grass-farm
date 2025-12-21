using Project.Services;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(GardenConfig), menuName = "Config/Level/Garden")]
    public class GardenConfig : ScriptableObject
    {
        [SerializeField] private string _animationName;
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        public int InteractAnimation => Animator.StringToHash(_animationName);
    }
}