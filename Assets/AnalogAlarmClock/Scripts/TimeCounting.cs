using System;
using UnityEngine;

namespace AnalogAlarmClock
{
    public class TimeCounting : MonoBehaviour
    {
        public event Action<float> TimePass;
        
        private readonly float _intervalTime = 1.0f;
        
        public void Start()
        {
            InvokeRepeating(nameof(TimePassInvoke), _intervalTime, _intervalTime); 
        }

        private void TimePassInvoke()
        {
            TimePass?.Invoke(_intervalTime);
        }
    }
}