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
        [SerializeField] private Rigidbody _rigidbody;

        private readonly float _minMagnitude = 0.01f;
        private bool _wasRunning;

        public void Move(Vector3 direction, float speed)
        {
            _rigidbody.linearVelocity = direction * speed;
            CheckRun();
        }
        
        public void UpdateRotation(Vector3 direction, float speed)
        {
            if (direction.magnitude < _minMagnitude)
            {
                return;
            }
            
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, 
                Quaternion.LookRotation(direction), 
                speed * Time.deltaTime));
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
            return _rigidbody.linearVelocity.sqrMagnitude > _minMagnitude * _minMagnitude;
        }
    }
}