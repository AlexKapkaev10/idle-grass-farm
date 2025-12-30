using System;
using System.Collections;
using Project.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Game
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private CustomerConfig _config;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Image _image;

        private QueuePoint _currentPoint;
        private Coroutine _moveCoroutine;

        private ResourceType _wishfulType;

        public event Action Arrived;
        public bool IsMoving { get; private set; }

        private void OnDestroy()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
        }

        public void Initialize(QueuePoint point, Vector3 spawnPosition)
        {
            PlaceOnNavMesh(spawnPosition);
            SetPoint(point);

            var random = Random.Range(0, 2);
            _wishfulType = (ResourceType)random;
            
            _image.sprite = _config.GetResourceSprite(_wishfulType);
        }

        public void SetPoint(QueuePoint point)
        {
            ReleasePoint();

            _currentPoint = point;
            _currentPoint.SetBusy(true);

            StartMove(_currentPoint.GetPosition(), false);
        }

        public void ReleasePoint()
        {
            if (_currentPoint != null)
            {
                _currentPoint.SetBusy(false);
                _currentPoint = null;
            }
        }

        public void StartMove(Vector3 destination, bool isExit = false)
        {
            if (IsMoving)
            {
                StopMove();
            }

            if (!_navMeshAgent.enabled)
            {
                return;
            }

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(destination);

            IsMoving = true;
            _moveCoroutine = StartCoroutine(CheckPathComplete(isExit));
        }

        public ResourceType GetWishfulType()
        {
            return _wishfulType;
        }

        private void StopMove()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }

            IsMoving = false;

            if (_navMeshAgent.enabled)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.velocity = Vector3.zero;
                _navMeshAgent.ResetPath();
            }
        }

        private IEnumerator CheckPathComplete(bool isExit)
        {
            while (IsMoving)
            {
                if (!_navMeshAgent.enabled || !_navMeshAgent.isOnNavMesh)
                {
                    yield return null;
                    continue;
                }

                if (!_navMeshAgent.pathPending)
                {
                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                    {
                        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            if (isExit)
                            {
                                Destroy(gameObject);
                                yield break;
                            }

                            IsMoving = false;
                            Arrived?.Invoke();
                        }
                    }
                }

                yield return null;
            }

            _moveCoroutine = null;
        }

        private bool PlaceOnNavMesh(Vector3 desiredPos)
        {
            const float sampleRadius = 2f;

            if (NavMesh.SamplePosition(desiredPos, out var hit, sampleRadius, NavMesh.AllAreas))
            {
                _navMeshAgent.Warp(hit.position);
                return true;
            }

            transform.position = desiredPos;
            return false;
        }
    }
}
