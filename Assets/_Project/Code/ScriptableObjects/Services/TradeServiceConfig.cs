using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(TradeServiceConfig), menuName = "Config/Service/Trade")]
    public class TradeServiceConfig : ScriptableObject
    {
        [field: Header("Prefabs")]
        [field: SerializeField] public Customer[] CustomerPrefabs { get; private set; }
        [field: Header("Position Settings")]
        [field: SerializeField] public Vector3 CustomerSpawnPosition { get; private set; }
        [field: SerializeField] public Vector3 CustomerExitPosition { get; private set; }
        
        [field: Header("Spawn Settings")]
        [field: SerializeField] public float CustomerSpawnDelay { get; private set; } = 2.0f;
        [field: SerializeField] public float SpawnDelayMin { get; set; } = 1f;
        [field: SerializeField] public float SpawnDelayMax { get; set; } = 3f;

        [field: Header("View Settings")]
        [field: SerializeField] public Sprite FirstResourceSprite { get; private set; }
        [field: SerializeField] public Sprite SecondResourceSprite { get; private set; }
        [field: SerializeField] public Sprite[] Emojis { get; private set; }
        
        public Sprite GetResourceSprite(ResourceType type)
        {
            return type == ResourceType.First ? FirstResourceSprite : SecondResourceSprite;
        }

        public Sprite GetRandomEmoji()
        {
            var randomIndex = Random.Range(0, Emojis.Length);
            return Emojis[randomIndex];
        }

        public Customer GetRandomCustomer()
        {
            var randomIndex = Random.Range(0, CustomerPrefabs.Length);
            return CustomerPrefabs[randomIndex];
        }
    }
}