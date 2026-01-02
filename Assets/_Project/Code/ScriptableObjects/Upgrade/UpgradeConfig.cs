using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(UpgradeConfig), menuName = "Config/Level/Upgrade")]
    public class UpgradeConfig : ScriptableObject
    {
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public AbilityType Type { get; private set; }
    }
}