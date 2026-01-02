using Project.ScriptableObjects;
using VContainer;

namespace Project.UI.MVP
{
    public interface IPopupPresenter
    {
        
    }
    
    public class PopupPresenter : IPopupPresenter
    {
        private readonly PopupPresenterConfig _config;

        [Inject]
        public PopupPresenter(PopupPresenterConfig config)
        {
            _config = config;
        }
    }
}