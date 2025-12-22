using Unity.Cinemachine;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(CameraServiceConfig), menuName = "Config/Service/Camera")]
    public class CameraServiceConfig : ScriptableObject
    {
        [field: SerializeField] public CinemachineCamera CinemachineCameraPrefab { get; private set; }
        [field: SerializeField] public CinemachineBrain CinemachineBrainPrefab { get; private set; }
        
    }
}