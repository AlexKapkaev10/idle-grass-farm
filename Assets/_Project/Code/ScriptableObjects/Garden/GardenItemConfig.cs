using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(GardenItemConfig), menuName = "Config/Level/Garden Item")]
    public class GardenItemConfig : ScriptableObject
    {
        [field: SerializeField] public ResourceItem ResourceItemPrefab { get; private set; }
        [field: SerializeField] public Vector3 EndMowValue { get; private set; }
        [field: SerializeField] public float EndMowDuration { get; private set; }
        [field: SerializeField] public float EndGrowDuration { get; private set; }
        [field: SerializeField] public float DelayGrowValue { get; private set; }
        [field: SerializeField] public AnimationCurveConfig CurveConfig { get; private set; }
    }
}