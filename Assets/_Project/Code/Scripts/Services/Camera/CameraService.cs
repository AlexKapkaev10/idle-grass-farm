using Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Project.Services
{
    public interface ICameraService
    {
        void SetTarget(Transform target);
    }
    
    public class CameraService : ICameraService
    {
        private readonly ICameraHandler _handler;

        [Inject]
        public CameraService(CameraServiceConfig config)
        {
            _handler = Object.Instantiate(config.CameraPrefab, null);
        }

        public void SetTarget(Transform target)
        {
            _handler.SetTarget(target);
        }
    }
}