using DG.Tweening;
using Project.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class ViewComponent : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private AnimationCurveConfig _animationConfig;

        private Tween _scaleTween;

        private void OnDestroy()
        {
            _scaleTween?.Kill();
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
            
            _scaleTween?.Kill();
            _scaleTween = _image.transform
                .DOScale(1f, 0.5f)
                .From(0f)
                .SetEase(_animationConfig.OutBounceEase)
                .OnComplete(()=> _scaleTween = null);
        }
    }
}