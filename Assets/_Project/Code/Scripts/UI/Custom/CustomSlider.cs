using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Custom
{
    public sealed class CustomSlider : MonoBehaviour
    {
        [SerializeField] private Image _imageFront;
        [SerializeField] private Image _imageBack;
        [SerializeField] private TMP_Text _textHeader;

        private int _currentAmount;
        private int _capacity;
        
        public void SetData(int amount, int capacity)
        {
            _currentAmount = amount;
            _capacity = Mathf.Max(1, capacity);
            
            UpdateFill();
        }
        
        public void UpdateText(string text)
        {
            _textHeader.SetText(text);
        }

        public void UpdateSlider(float sliderValue)
        {
            _imageFront.fillAmount = sliderValue;
        }
        
        public void SetAmount(int amount)
        {
            _currentAmount = amount;
            UpdateFill();
        }
        
        public void SetCapacity(int capacity)
        {
            _capacity = Mathf.Max(1, capacity);
            UpdateFill();
        }
        
        private void UpdateFill()
        {
            float normalizedValue = (float)_currentAmount / _capacity;
            _imageFront.fillAmount = Mathf.Clamp01(normalizedValue);
        }
    }
}