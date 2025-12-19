using System;
using System.Collections.Generic;
using Project.ScriptableObjects;
using UnityEngine;

namespace Project.Game
{
    public sealed class SearchModel : ISearchModel
    {
        private readonly SearchModelConfig _config;
        private readonly Transform _owner;

        private HashSet<Collider> _cachedColliders = new();
        private HashSet<Collider> _removedColliders = new();

        private Collider[] _colliders = new Collider[100];

        private int _counter;
        private float _lastProcessTime;

        public event Action<IInteractable, bool> Interacted;

        public SearchModel(Transform owner, SearchModelConfig config)
        {
            _owner = owner;
            _config = config;
        }

        public void Process()
        {
            if (!_owner)
            {
                return;
            }
            
            if (Time.time < _lastProcessTime + _config.Cooldown)
            {
                return;
            }
            
            _counter = Physics
                .OverlapSphereNonAlloc(_owner.position, _config.Radius, _colliders, _config.LayerMask);
            
            for (int i = 0; i < _counter; i++)
            {
                var collider = _colliders[i];
                
                if (_cachedColliders.Contains(collider))
                {
                    continue;
                }
                
                CheckEnter(collider);
            }
            
            CheckExit();
            
            _lastProcessTime = Time.time;
        }

        private void CheckEnter(Collider collider)
        {
            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                _cachedColliders.Add(collider);
                interactable.Enter();
                Interacted?.Invoke(interactable, true);
            }
        }

        private void CheckExit()
        {
            if (_cachedColliders.Count == 0)
            {
                return;
            }
            
            foreach (var cachedCollider in _cachedColliders)
            {
                bool isStillActive = false;
                
                for (int i = 0; i < _counter; i++)
                {
                    if (_colliders[i] == cachedCollider)
                    {
                        isStillActive = true;
                        break;
                    }
                }

                if (!isStillActive)
                {
                    _removedColliders.Add(cachedCollider);
                }
            }
            
            foreach (var collider in _removedColliders)
            {
                if (collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Exit();
                    Interacted?.Invoke(interactable, false);
                }
                
                _cachedColliders.Remove(collider);
            }
            
            _removedColliders.Clear();
        }

        public void Dispose()
        {
            _cachedColliders.Clear();
            _removedColliders.Clear();

            _colliders = null;
            _cachedColliders = null;
            _removedColliders = null;
        }
    }
}