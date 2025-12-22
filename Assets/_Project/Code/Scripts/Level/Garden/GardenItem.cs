using DG.Tweening;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Game
{
    public sealed class GardenItem : MonoBehaviour, IGardenItem
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private GardenItemConfig _config;
        public Transform Transform => transform;

        public bool CanMow { get; private set; }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }

        public void Initialize(Material material)
        {
            _renderer.material = material;
            CanMow = true;
        }

        public void Mow()
        {
            CanMow = false;
            
            transform.DOScale(_config.EndMowValue, _config.EndMowDuration)
                .SetEase(_config.CurveConfig.InBounceEase)
                .OnComplete(Grow);
        }

        private void Grow()
        {
            transform.DOScale(Vector3.one, _config.EndGrowDuration)
                .SetDelay(_config.DelayGrowValue)
                .SetEase(_config.CurveConfig.OutBounceEase)
                .OnComplete(()=> CanMow = true);
        }
    }
}