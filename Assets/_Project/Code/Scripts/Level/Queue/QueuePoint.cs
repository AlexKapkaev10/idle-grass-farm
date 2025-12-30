using UnityEngine;

namespace Project.Game
{
    public class QueuePoint : MonoBehaviour
    {
        [SerializeField] private bool _isPayPoint;
        public bool IsBusy { get; private set; }

        public void SetBusy(bool isBusy)
        {
            IsBusy = isBusy;
        }
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool IsPayPoint()
        {
            return _isPayPoint;
        }
    }
}