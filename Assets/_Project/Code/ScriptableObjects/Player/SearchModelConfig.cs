using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(SearchModelConfig), menuName = "Config/Search/Model")]
    public sealed class SearchModelConfig : ScriptableObject
    {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public float Radius { get; private set; } = 1f;
        [field: SerializeField] public float Cooldown { get; private set; } = 1f;
    }
}