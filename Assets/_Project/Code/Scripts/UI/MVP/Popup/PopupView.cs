using UnityEngine;

namespace Project.UI.MVP
{
    public interface IPopupView : IView
    {
        
    }
    
    public class PopupView : MonoBehaviour, IPopupView
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}