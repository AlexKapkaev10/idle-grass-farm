using Project.ScriptableObjects;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Project.Services
{
    public sealed class CameraService : ICameraService
    {
        private readonly CinemachineCamera _cinemachineCamera;

        [Inject]
        public CameraService(CameraServiceConfig config)
        {
            Object.Instantiate(config.CinemachineBrainPrefab);
            _cinemachineCamera = Object.Instantiate(config.CinemachineCameraPrefab);
        }

        public void SetTarget(Transform target)
        {
            _cinemachineCamera.Follow = target;
        }
    }
}