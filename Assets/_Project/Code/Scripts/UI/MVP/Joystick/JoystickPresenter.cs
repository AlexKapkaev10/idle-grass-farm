using Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Project.UI.MVP
{
    public interface IJoystickPresenter : IPresenter
    {
        
    }
    
    public sealed class JoystickPresenter : IJoystickPresenter
    {
        private readonly JoystickPresenterConfig _config;
        
        private IJoystickView _view;

        [Inject]
        public JoystickPresenter(JoystickPresenterConfig config)
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
                _view = null;
            }
        }
    }
}