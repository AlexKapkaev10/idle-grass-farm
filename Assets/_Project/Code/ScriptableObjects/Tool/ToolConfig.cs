using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ToolConfig), menuName = "Config/Level/Tool")]
    public class ToolConfig : ScriptableObject
    {
        [field: SerializeField] public AnimationCurveConfig CurveConfig { get; private set; }
        [field: SerializeField] public float ScaleDuration { get; private set; }
    }
}