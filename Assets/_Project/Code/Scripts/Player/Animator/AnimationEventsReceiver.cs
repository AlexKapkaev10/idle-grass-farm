using UnityEngine;

namespace Project.Game
{
    public class AnimationEventsReceiver : MonoBehaviour
    {
        public void Mow()
        {
            Debug.Log("Mow animation event received");
        }
    }
}