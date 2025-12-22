using UnityEngine;

namespace Project.Game
{
    public interface IGardenItem
    {
        bool CanMow { get; }
        Transform Transform { get; }
        void Initialize(Material material);
        void Mow(Material resourceMaterial, out IResourceItem resourceItem);
        void Grow();
    }
}