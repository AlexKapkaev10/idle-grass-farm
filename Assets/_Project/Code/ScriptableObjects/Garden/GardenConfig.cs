using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(GardenConfig), menuName = "Config/Level/Garden")]
    public class GardenConfig : ScriptableObject
    {
        [SerializeField] private string _animationName;
        public int InteractAnimation => Animator.StringToHash(_animationName);
        [field: SerializeField] public Material CellMaterial { get; private set; }
        [field: SerializeField] public Material ResourceMaterial { get; private set; }
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
    }
}