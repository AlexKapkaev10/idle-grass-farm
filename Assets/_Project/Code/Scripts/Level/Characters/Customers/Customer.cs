using Project.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Game
{
    public interface ICustomer
    {
        void Initialize(QueuePoint point, Vector3 spawnPosition, ResourceType wishfulType);
        void SetEmoji(Sprite sprite);
        void SetPoint(QueuePoint point);
        void ReleasePoint();
        void StartMove(Vector3 destination, bool isExit = false);
        ResourceType GetWishfulType();
        INavMeshMoveModel GetMoveModel();
    }
    
    public class Customer : MonoBehaviour, ICustomer
    {
        [SerializeField] private CustomerConfig _config;
        [SerializeField] private AnimatorComponent _animatorComponent;
        [SerializeField] private ViewComponent _viewComponent;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private ResourceType _wishfulType;
        private QueuePoint _currentPoint;

        private INavMeshMoveModel _moveModel;

        private void OnDestroy()
        {
            _moveModel.ExitReached -= OnExitReached;
            _moveModel.MoveStateChanged -= OnMoveStateChanged;
                
            _moveModel.Dispose();
        }

        public void Initialize(QueuePoint point, Vector3 spawnPosition, ResourceType wishfulType)
        {
            _moveModel = new NavMeshMoveModel(_navMeshAgent);
            _moveModel.ExitReached += OnExitReached;
            _moveModel.MoveStateChanged += OnMoveStateChanged;
            _moveModel.PlaceOn(spawnPosition);

            _wishfulType = wishfulType;
            SetPoint(point);
        }

        public void SetEmoji(Sprite sprite)
        {
            _viewComponent.SetSprite(sprite);
        }

        public void SetPoint(QueuePoint point)
        {
            ReleasePoint();

            _currentPoint = point;
            _currentPoint.SetBusy(true);

            StartMove(_currentPoint.GetPosition());
        }

        public void ReleasePoint()
        {
            if (_currentPoint == null)
            {
                return;
            }
            
            _currentPoint.SetBusy(false);
            _currentPoint = null;
        }

        public void StartMove(Vector3 destination, bool isExit = false)
        {
            _moveModel.StartMove(destination, isExit);
        }

        public ResourceType GetWishfulType()
        {
            return _wishfulType;
        }

        public INavMeshMoveModel GetMoveModel()
        {
            return _moveModel;
        }

        private void OnMoveStateChanged(bool isMoving)
        {
            _animatorComponent.SetBool(_config.MoveAnimationID, isMoving);
        }

        private void OnExitReached()
        {
            Destroy(gameObject);
        }
    }
}
