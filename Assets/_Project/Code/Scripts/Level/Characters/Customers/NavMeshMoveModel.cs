using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Game
{
    public sealed class NavMeshMoveModel : INavMeshMoveModel
    {
        private readonly NavMeshAgent _agent;
        private CancellationTokenSource _cts;

        public event Action Arrived;
        public event Action ExitReached;
        public event Action<bool> MoveStateChanged;
        public bool IsMoving { get; private set; }

        public NavMeshMoveModel(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public void PlaceOn(Vector3 position, float sampleRadius = 2f)
        {
            if (!CanTouchAgent())
            {
                _agent.transform.position = position;
                return;
            }

            if (NavMesh.SamplePosition(position, out var hit, sampleRadius, NavMesh.AllAreas))
            {
                _agent.Warp(hit.position);
                return;
            }

            _agent.transform.position = position;
        }

        public void StartMove(Vector3 destination, bool isExit = false)
        {
            if (_agent == null)
            {
                return;
            }

            StopMoveInternal();

            if (!CanTouchAgent())
            {
                return;
            }

            _agent.isStopped = false;
            _agent.SetDestination(destination);

            SetMove(true);

            _cts = new CancellationTokenSource();
            PathProcessAsync(isExit, _cts.Token).Forget();
        }

        private void StopMove()
        {
            StopMoveInternal();

            if (_agent == null)
            {
                return;
            }

            if (!CanTouchAgent())
            {
                return;
            }

            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _agent.ResetPath();
        }

        public void Dispose()
        {
            StopMove();
        }

        private void StopMoveInternal()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            SetMove(false);
        }

        private void SetMove(bool value)
        {
            if (IsMoving == value)
            {
                return;
            }

            IsMoving = value;
            MoveStateChanged?.Invoke(IsMoving);
        }

        private bool CanTouchAgent()
        {
            return _agent.enabled && _agent.isOnNavMesh;
        }

        private bool CheckEndPath()
        {
            return !_agent.pathPending &&
                   _agent.remainingDistance <= _agent.stoppingDistance &&
                   (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
        }

        private async UniTaskVoid PathProcessAsync(bool isExit, CancellationToken token)
        {
            while (IsMoving)
            {
                token.ThrowIfCancellationRequested();
                    
                if (!CanTouchAgent())
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                    continue;
                }

                if (CheckEndPath())
                {
                    SetMove(false);

                    if (isExit)
                    {
                        ExitReached?.Invoke();
                        return;
                    }

                    Arrived?.Invoke();
                    return;
                }

                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}