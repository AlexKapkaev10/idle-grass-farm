using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class UpgradeViewItem : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _textHeader;

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