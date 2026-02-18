using Project.Game;
using Project.ScriptableObjects;
using Project.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Services
{
    public interface IPoolService : IInitializable
    {
        ResourceItem GetGardenResource();
        void ReleaseResourceItem(IResourceItem item);
    }
    
    public class PoolService : IPoolService
    {
        private ObjectPool<ResourceItem> _gardenResourcePool;
        private readonly PoolServiceConfig _config;

        [Inject]
        public PoolService(PoolServiceConfig config)
        {
            _config = config;
        }
        
        public void Initialize()
        {
            _gardenResourcePool = new ObjectPool<ResourceItem>(
                _config.ResourceItemPrefab,
                _config.ResourcePoolSize,
                new GameObject("GardenResourcePool").transform);
        }

        public ResourceItem GetGardenResource()
        {
            return _gardenResourcePool.Get();
        }

        public void ReleaseResourceItem(IResourceItem item)
        {
            _gardenResourcePool.Release(item as ResourceItem);
        }
    }
}