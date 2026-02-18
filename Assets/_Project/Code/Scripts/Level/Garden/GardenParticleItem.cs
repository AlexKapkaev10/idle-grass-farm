using UnityEngine;

namespace Project.Game
{
    public interface IGardenParticleItem
    {
        void Play(Vector3 position);
    }
    
    public class GardenParticleItem : MonoBehaviour, IGardenParticleItem
    {
        [SerializeField] public ParticleSystem[] particleSystems;

        public void Play(Vector3 position)
        {
            transform.position = position;
            
            foreach (var particle in particleSystems)
            {
                particle.Play();
            }
        }
    }
}