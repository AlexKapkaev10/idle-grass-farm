using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Custom
{
    public sealed class CustomSlider : MonoBehaviour
    {
        [SerializeField] private Image _imageFront;
        [SerializeField] private Image _imageBack;

        public void UpdateSlider(float sliderValue)
        {
            _imageFront.fillAmount = sliderValue;
        }
    }
}