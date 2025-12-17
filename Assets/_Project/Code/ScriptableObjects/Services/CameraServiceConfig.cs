using Project.Services;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(CameraServiceConfig), menuName = "Config/Service/Camera")]
    public class CameraServiceConfig : ScriptableObject
    {
        [field: SerializeField] public CameraHandler CameraPrefab { get; private set; }
    }
}