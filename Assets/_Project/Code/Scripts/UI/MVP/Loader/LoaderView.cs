using DG.Tweening;
using Project.UI.Custom;
using UnityEngine;

namespace Project.UI.MVP
{
    public interface ILoaderView : IView
    {
        CustomSlider SliderProgress { get; }
    }
    
    public class LoaderView : MonoBehaviour, ILoaderView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [field: SerializeField] public CustomSlider SliderProgress { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Destroy()
        {
            _canvasGroup.DOFade(0.0f, 0.5f)
                .From(1.0f)
                .SetEase(Ease.Linear)
                .OnComplete(()=> Destroy(gameObject));
        }
    }
}