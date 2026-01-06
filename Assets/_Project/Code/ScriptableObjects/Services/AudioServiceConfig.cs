using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(AudioServiceConfig), menuName = "Config/Service/Audio")]
    public class AudioServiceConfig : ScriptableObject
    {
        [field: SerializeField] public AudioSource AudioSourcePrefab { get; private set; }
        [field: SerializeField] public AudioClip AmbientClip { get; private set; }
    }
}