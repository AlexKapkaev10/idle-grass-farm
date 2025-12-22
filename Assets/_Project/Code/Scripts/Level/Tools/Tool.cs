using System;
using DG.Tweening;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Game
{
    public sealed class Tool : MonoBehaviour, ITool
    {
        [SerializeField] private ToolConfig _config;
        
        private void Awake()
        {
            transform.DOScale(Vector3.one, _config.ScaleDuration)
                .From(0)
                .SetEase(_config.CurveConfig.OutBounceEase);
        }

        public void Destroy()
        {
            transform.DOScale(Vector3.zero, _config.ScaleDuration)
                .From(1)
                .SetEase(_config.CurveConfig.InBounceEase);
        }
    }
}