using Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Project.Services
{
    public interface ICameraService
    {
        void SetTarget(Transform target);
        void Follow();
    }
    
    public class CameraService : ICameraService
    {
        private readonly Transform _cameraTransform;
        private readonly CameraServiceConfig _config;
        private Transform _target;

        [Inject]
        public CameraService(CameraServiceConfig config)
        {
            _config = config;
            _cameraTransform = Object.Instantiate(_config.CameraPrefab, null).transform;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Follow()
        {
            _cameraTransform.position = new Vector3(
                _target.position.x + _config.Offset.x,
                _cameraTransform.position.y,
                _target.position.z + _config.Offset.z
            );
        }
    }
}