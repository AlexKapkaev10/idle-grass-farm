using System;
using UnityEngine;

namespace Project.Game
{
    public class AnimationEventsReceiver : MonoBehaviour
    {
        public event Action Mowed;
        
        public void Mow()
        {
            Mowed?.Invoke();
        }
    }
}