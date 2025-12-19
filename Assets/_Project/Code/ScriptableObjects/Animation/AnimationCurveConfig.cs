using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(AnimationCurveConfig), menuName = "Config/Animation/Curve")]
    public class AnimationCurveConfig : ScriptableObject
    {
        [field: SerializeField] public AnimationCurve OutBounceEase { get; private set; }
        [field: SerializeField] public AnimationCurve InBounceEase { get; private set; }
    }
}