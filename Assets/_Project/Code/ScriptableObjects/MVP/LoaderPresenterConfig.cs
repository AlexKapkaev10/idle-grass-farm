using Project.UI.MVP;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(LoaderPresenterConfig), menuName = "Config/MVP/Loader Presenter")]
    public class LoaderPresenterConfig : ScriptableObject
    {
        [field: SerializeField] public LoaderView ViewPrefab { get; private set; }
    }
}