using Unity.Cinemachine;
using UnityEngine;

namespace Project.Services
{
    public interface ICameraHandler
    {
        void SetTarget(Transform target);
    }
    
    public class CameraHandler : MonoBehaviour, ICameraHandler
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        public void SetTarget(Transform target)
        {
            _cinemachineCamera.Follow = target;
        }
    }
}