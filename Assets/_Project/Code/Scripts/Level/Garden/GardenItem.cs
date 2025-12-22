using DG.Tweening;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Game
{
    public sealed class GardenItem : MonoBehaviour, IGardenItem
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private GardenItemConfig _config;
        private IResourceItem _resourceItem;

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

        public void Mow(Material resourceMaterial, out IResourceItem resourceItem)
        {
            CanMow = false;
            
            _resourceItem = Instantiate(_config.ResourceItemPrefab);
            _resourceItem.Initialize(transform.position, resourceMaterial);

            transform.DOScale(_config.EndMowValue, _config.EndMowDuration)
                .SetEase(_config.CurveConfig.InBounceEase)
                .OnComplete(()=> _resourceItem.Show());
            
            _resourceItem.Collected += OnResourceCollected;
            
            resourceItem = _resourceItem;
        }

        private void OnResourceCollected()
        {
            Grow();
            _resourceItem = null;
        }

        public void Grow()
        {
            transform.DOScale(Vector3.one, _config.EndGrowDuration)
                .SetDelay(_config.DelayGrowValue)
                .SetEase(_config.CurveConfig.OutBounceEase)
                .OnComplete(()=> CanMow = true);
        }
    }
}