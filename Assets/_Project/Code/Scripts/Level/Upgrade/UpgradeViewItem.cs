using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class UpgradeViewItem : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _textHeader;
        [SerializeField] private TMP_Text _textDescription;

        public void SetHeader(string header)
        {
            _textHeader.SetText(header);
        }

        public void SetDescription(string description)
        {
            _textDescription.SetText(description);
        }

        public void SetActiveIndicator(bool isActive)
        {
            if (_image.enabled == isActive)
            {
                return;
            }
            
            _image.enabled = isActive;
        }
    }
}