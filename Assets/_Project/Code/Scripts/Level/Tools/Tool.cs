using DG.Tweening;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Game
{
    public sealed class Tool : MonoBehaviour, ITool
    {
        [SerializeField] private ToolConfig _config;

        private Tween _tweenScale;
        
        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        public void Display(bool isActive)
        {
            _tweenScale?.Kill();
            
            _tweenScale = transform.DOScale(isActive ? 1.0f : 0.0f, _config.ScaleDuration)
                .From(isActive ? 0.0f : 1.0f)
                .SetEase(isActive ? _config.CurveConfig.OutBounceEase : _config.CurveConfig.InBounceEase);
        }
    }
}