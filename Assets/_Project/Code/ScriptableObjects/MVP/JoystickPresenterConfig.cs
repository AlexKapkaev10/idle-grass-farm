using Project.UI.MVP;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(JoystickPresenterConfig), menuName = "Config/MVP/Joystick Presenter")]
    public class JoystickPresenterConfig : ScriptableObject
    {
        [field: SerializeField] public JoystickView ViewPrefab { get; private set; }
    }
}