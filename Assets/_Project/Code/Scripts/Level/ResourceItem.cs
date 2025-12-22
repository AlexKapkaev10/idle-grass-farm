using System;
using DG.Tweening;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Game
{
    public interface IResourceItem
    {
        event Action Collected;
        Transform Transform { get; }
        void Initialize(Vector3 position, Material material, int count = 1);
        void Show();
        void MoveToTarget(Transform target, float delay, Action<int> callback);
    }

    public sealed class ResourceItem : MonoBehaviour, IResourceItem
    {
        [SerializeField] private AnimationCurveConfig _animationConfig;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Transform _viewTransform;
        
        [SerializeField] private float _rotationDuration = 5.0f;
        [SerializeField] private float _scaleDuration = 0.2f;
        
        private int _count;
        private Sequence _moveToSequence;

        public event Action Collected;
        public Transform Transform => transform;

        private void OnDestroy()
        {
            DOTween.Kill(transform);
            DOTween.Kill(_viewTransform);
            _moveToSequence?.Kill();
            
            Collected = null;
        }

        public void Initialize(Vector3 position, Material material, int count = 1)
        {
            _count = count;
            _meshRenderer.material = material;
            transform.localScale = Vector3.zero;
            SetPosition(position);
        }

        public void Show()
        {
            transform
                .DOScale(Vector3.one, _scaleDuration)
                .SetEase(_animationConfig.OutBounceEase);

            _viewTransform
                .DORotate(_animationConfig.RotationAngle, _rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        public void MoveToTarget(Transform target, float delay, Action<int> callback)
        {
            DOVirtual.DelayedCall(delay, () =>
            {
                DOTween.Kill(transform);
                DOTween.Kill(_viewTransform);
                
                _moveToSequence = DOTween.Sequence();
                transform.SetParent(target);

                _moveToSequence.Append(transform
                        .DOLocalMove(Vector3.zero, 0.5f)
                        .SetEase(Ease.Linear))
                    .Join(transform.DOScale(Vector3.zero, 0.5f)
                        .SetEase(Ease.Linear))
                    .Join(_viewTransform.DORotate(Vector3.zero, 0.2f))
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        callback?.Invoke(_count);
                        Collected?.Invoke();
                        Destroy(gameObject);
                    });
            });
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}