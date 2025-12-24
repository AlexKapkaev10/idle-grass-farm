using Project.ScriptableObjects;
using UnityEngine;

namespace Project.UI.MVP
{
    public sealed class LoaderPresenter : ILoaderPresenter
    {
        private readonly LoaderPresenterConfig _config;

        private ILoaderView _view;

        public LoaderPresenter(LoaderPresenterConfig config)
        {
            _config = config;
        }

        public void SetActiveView(bool isActive)
        {
            if (isActive)
            {
                _view = Object.Instantiate(_config.ViewPrefab, null);
            }
            else
            {
                _view?.Destroy();
                _view = null;
            }
        }

        public void UpdateSlider(float sliderValue)
        {
            _view?.SliderProgress.UpdateSlider(sliderValue);
        }
    }
}