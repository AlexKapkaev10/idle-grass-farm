using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(CustomerConfig), menuName = "Config/Level/Customer")]
    public class CustomerConfig : ScriptableObject
    {
        [SerializeField] private Sprite _firstResourceSprite;
        [SerializeField] private Sprite _secondResourceSprite;

        public Sprite GetResourceSprite(ResourceType type)
        {
            return type == ResourceType.First ? _firstResourceSprite : _secondResourceSprite;
        }
    }
}