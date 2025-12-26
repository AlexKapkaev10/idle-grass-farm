using DG.Tweening;
using Project.Game;
using Project.UI.Custom;
using TMPro;
using UnityEngine;

namespace Project.UI.MVP
{
    public interface IInfoView : IView
    {
        CustomSlider GreenSlider { get; }
        CustomSlider YellowSlider { get; }
        void SetCurrencyAmount(ResourceType resourceType, int oldAmount, int newAmount);
    }
    
    public sealed class InfoView : MonoBehaviour, IInfoView
    {
        [SerializeField] private TMP_Text _textFirstCurrency;
        [SerializeField] private TMP_Text _textSecondCurrency;
        
        [field: SerializeField] public CustomSlider GreenSlider { get; private set; }
        [field: SerializeField] public CustomSlider YellowSlider { get; private set; }
        
        private Tween _tweenFirst;
        private Tween _tweenSecond;

        private void OnDisable()
        {
            _tweenFirst?.Kill();
            _tweenSecond?.Kill();
        }

        public void SetCurrencyAmount(ResourceType resourceType, int oldAmount, int newAmount)
        {
            switch (resourceType)
            {
                case ResourceType.First:
                    _tweenFirst?.Kill();

                    _tweenFirst = DOTween.To(() => oldAmount, 
                        x =>
                        {
                            oldAmount = x;
                            _textFirstCurrency.SetText(oldAmount.ToString());
                        },
                        newAmount,
                        1.0f
                    )
                    .SetEase(Ease.Linear);
                    break;
                case ResourceType.Second:
                    _tweenSecond?.Kill();

                    _tweenSecond = DOTween.To(() => oldAmount,
                        x =>
                        {
                            oldAmount = x;
                            _textSecondCurrency.SetText(oldAmount.ToString());
                        },
                        newAmount,
                        1.0f)
                        .SetEase(Ease.Linear);
                    break;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}