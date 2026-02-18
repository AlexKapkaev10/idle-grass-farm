using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PoolServiceConfig), menuName = "Config/Service/Pool")]
    public class PoolServiceConfig : ScriptableObject
    {
        [field: SerializeField] public ResourceItem ResourceItemPrefab { get; private set; }
        [field: SerializeField] public GardenParticleItem ParticleSystemPrefab { get; private set; }
        [field: SerializeField] public int ResourcePoolSize { get; private set; }
        [field: SerializeField] public int ParticleSystemPoolSize { get; private set; }
    }
}