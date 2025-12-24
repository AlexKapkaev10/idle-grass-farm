using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Project.UI.MVP
{
    public interface IJoystickView :  IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        void Destroy();
    }
    
    public sealed class JoystickView : MonoBehaviour, IJoystickView
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private OnScreenStick _screenStick;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = _rectTransform.position;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
            _screenStick.OnPointerDown(eventData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _screenStick.OnDrag(eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _rectTransform.position = _startPosition;
            _screenStick.OnPointerUp(eventData);
        }

        void IJoystickView.Destroy()
        {
            Destroy(gameObject);
        }
    }
}