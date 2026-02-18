using System.Collections.Generic;
using UnityEngine;

namespace Project.Utils
{
    public sealed class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Stack<T> _stack = new();
        private readonly Transform _releasedParent;

        public ObjectPool(T prefab, int size, Transform releasedParent = null)
        {
            _prefab = prefab;
            _releasedParent = releasedParent;
            Warmup(size);
        }

        public T Get(Transform parent = null)
        {
            T item = _stack.Count > 0 ? _stack.Pop() : CreateNew();
            item.transform.SetParent(parent);
            item.gameObject.SetActive(true);
            
            return item;
        }

        public void Release(T item)
        {
            if (!item)
            {
                return;
            }

            item.gameObject.SetActive(false);

            item.transform.SetParent(_releasedParent, false);
            _stack.Push(item);
        }

        public void Clear(bool destroyObjects = true)
        {
            if (!destroyObjects)
            {
                _stack.Clear();
                return;
            }

            while (_stack.Count > 0)
            {
                var item = _stack.Pop();
                if (item)
                {
                    Object.Destroy(item.gameObject);
                }
            }
        }

        private void Warmup(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = CreateNew();
                Release(item);
            }
        }

        private T CreateNew()
        {
            var item = Object.Instantiate(_prefab);
            item.gameObject.SetActive(false);
            return item;
        }
    }
}
