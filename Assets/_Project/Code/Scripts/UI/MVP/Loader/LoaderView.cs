using System;
using Project.UI.Custom;
using UnityEngine;

namespace Project.UI.MVP
{
    public interface ILoaderView : IView
    {
        CustomSlider SliderProgress { get; }
    }
    
    public class LoaderView : MonoBehaviour, ILoaderView
    {
        [field: SerializeField] public CustomSlider SliderProgress { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}