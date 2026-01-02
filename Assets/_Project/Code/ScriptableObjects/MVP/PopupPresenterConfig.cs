using Project.UI.MVP;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PopupPresenterConfig), menuName = "Config/MVP/Popup Presenter")]
    public class PopupPresenterConfig : ScriptableObject
    {
        [field: SerializeField] public PopupView ViewPrefab { get; private set; }
    }
}