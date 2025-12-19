using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(GameSceneServiceConfig), menuName = "Config/Service/Game Scene")]
    public class GameSceneServiceConfig : ScriptableObject
    {
        [field: SerializeField] public bool NeedJoystick { get; private set; }
    }
}