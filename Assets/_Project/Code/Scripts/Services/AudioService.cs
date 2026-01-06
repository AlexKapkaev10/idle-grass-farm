using Project.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Services
{
    public interface IAudioService : IStartable
    {
        void PlayClip(AudioClip clip);
    }
    
    public sealed class AudioService : IAudioService
    {
        private readonly AudioServiceConfig _config;
        private AudioSource _audioSourceAmbient;
        private AudioSource _audioSourceEffects;
        
        [Inject]
        public AudioService(AudioServiceConfig config)
        {
            _config = config;
        }

        public void Start()
        {
            _audioSourceAmbient = Object.Instantiate(_config.AudioSourcePrefab);
            _audioSourceEffects = Object.Instantiate(_config.AudioSourcePrefab);
            
            _audioSourceAmbient.clip = _config.AmbientClip;
            _audioSourceAmbient.loop = true;
            _audioSourceAmbient.Play();
        }

        public void PlayClip(AudioClip clip)
        {
            _audioSourceEffects.PlayOneShot(clip);
        }
    }
}