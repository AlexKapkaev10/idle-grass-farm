using UnityEngine;

namespace Project.Game
{
    public interface IMovement
    {
        void Move(Vector3 direction, float speed);
        void UpdateRotation(Vector3 direction, float speed);
    }
    
    public class Movement : MonoBehaviour, IMovement
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _body;

        private readonly float _minMagnitude = 0.0001f;
        private bool _wasRunning;

        public void Move(Vector3 direction, float speed)
        {
            _characterController.Move(direction * speed * Time.deltaTime);
            CheckRun();
        }
        
        public void UpdateRotation(Vector3 direction, float speed)
        {
            if (direction.sqrMagnitude < _minMagnitude)
            {
                return;
            }
            
            _body.rotation = Quaternion.RotateTowards(
                _body.rotation, 
                Quaternion.LookRotation(direction),
                speed * Time.deltaTime);
        }

        private void CheckRun()
        {
            bool isRunning = IsRun();

            if (isRunning == _wasRunning)
            {
                return;
            }

            _wasRunning = isRunning;

            if (isRunning)
            {
                Debug.Log(isRunning);
            }
            else
            {
                Debug.Log(isRunning);
            }
        }

        private bool IsRun()
        {
            return transform.position.sqrMagnitude > _minMagnitude * _minMagnitude;
        }
    }
}