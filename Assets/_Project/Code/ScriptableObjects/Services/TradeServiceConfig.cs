using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(TradeServiceConfig), menuName = "Config/Service/Trade")]
    public class TradeServiceConfig : ScriptableObject
    {
        [field: SerializeField] public Customer CustomerPrefab { get; private set; }
        [field: SerializeField] public Vector3 CustomerSpawnPosition { get; private set; }
        [field: SerializeField] public Vector3 CustomerExitPosition { get; private set; }
        [field: SerializeField] public float CustomerSpawnDelay { get; private set; } = 2.0f;
    }
}