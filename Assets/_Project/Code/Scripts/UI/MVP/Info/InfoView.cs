using Project.UI.Custom;
using UnityEngine;

namespace Project.UI.MVP
{
    public interface IInfoView : IView
    {
        CustomSlider GreenSlider { get; }
        CustomSlider YellowSlider { get; }
    }
    
    public sealed class InfoView : MonoBehaviour, IInfoView
    {
        [field: SerializeField] public CustomSlider GreenSlider { get; private set; }
        [field: SerializeField] public CustomSlider YellowSlider { get; private set; }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}