using Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Project.UI.MVP
{
    public interface IPopupPresenter : IPresenter
    {
        
    }
    
    public class PopupPresenter : IPopupPresenter
    {
        private readonly PopupPresenterConfig _config;
        
        private IPopupView _view;

        [Inject]
        public PopupPresenter(PopupPresenterConfig config)
        {
            _config = config;
        }

        public void SetActiveView(bool isActive)
        {
            if (isActive)
            {
                _view = Object.Instantiate(_config.ViewPrefab);
            }
            else
            {
                _view?.Destroy();
            }
        }
    }
}