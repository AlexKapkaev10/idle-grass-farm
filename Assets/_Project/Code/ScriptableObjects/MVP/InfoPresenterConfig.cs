using Project.UI.MVP;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(InfoPresenterConfig), menuName = "Config/MVP/Info Presenter")]
    public class InfoPresenterConfig : ScriptableObject
    {
        [field: SerializeField] public InfoView InfoViewPrefab { get; private set; }
    }
}