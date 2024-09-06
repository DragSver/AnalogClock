using System;
using System.Collections.Generic;
using AnalogAlarmClock.Interfaces;
using AnalogAlarmClock.TimeSynchronization;

namespace AnalogAlarmClock
{
    public class ClockTimeController
    {
        public event Action<DateTime> ChangeClockTime;
        
        private DateTime _dateTime;
        
        private readonly List<IClockController> _clockControllers;
        private readonly TimeCounting _timeCounting;
        private readonly TimeSynchronizationController _timerSync;

        public ClockTimeController(List<IClockController> clockControllers, TimeCounting timeCounting)
        {
            _clockControllers = clockControllers;
            _timeCounting = timeCounting;
            _timerSync = new();

            foreach (var clockController in _clockControllers)
                ChangeClockTime += clockController.SetClockView;
            
            _timeCounting.TimePass += AddSeconds;
            
            SyncTime();
        }

        private async void SyncTime()
        {
            _dateTime = await _timerSync.SyncTime();
            ChangeClockTime?.Invoke(_dateTime);
        }
        
        private void AddSeconds(float countSeconds)
        {
            _dateTime = _dateTime.AddSeconds(countSeconds);
            
            // Debug.Log($"{_dateTime.Hour}:{_dateTime.Minute}:{_dateTime.Second}");
            
            ChangeClockTime?.Invoke(_dateTime);
        }
        
        // private void EditTime()
        // {
        //     if (_dateTime.Seconds > 59)
        //     {
        //         _dateTime.Seconds %= 60;
        //         _dateTime.Minutes++;
        //     }
        //     if (_dateTime.Minutes > 59)
        //     {
        //         _dateTime.Minutes %= 60;
        //         _dateTime.Hours++;
        //     }
        //     if (_dateTime.Hours > 23)
        //     {
        //         _dateTime.Hours %= 24;
        //     }
        //     Debug.Log($"{_dateTime.Hours}:{_dateTime.Minutes}:{_dateTime.Seconds}");
        //     
        //     ChangeClockTime?.Invoke(_dateTime);
        // }
    }
}