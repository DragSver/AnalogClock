using System;
using System.Collections.Generic;
using AnalogAlarmClock.Interfaces;

namespace AnalogAlarmClock
{
    public class ClockTimeController
    {
        public event Action<DateTime> ChangeClockTime;
        
        private DateTime _dateTime;
        
        private float _intervalSyncTime = 3600;
        private float _timeForSync = 3600;

        private bool _pauseView = false;
        
        private readonly List<IClockController> _clockControllers;
        private readonly TimeCounting _timeCounting;
        private readonly ITimeSynchronizationController _timeSync;

        public ClockTimeController(List<IClockController> clockControllers, 
            ITimeSynchronizationController timeSynchronizationController, 
            TimeCounting timeCounting)
        {
            _clockControllers = clockControllers;
            _timeCounting = timeCounting;
            _timeSync = timeSynchronizationController;

            foreach (var clockController in _clockControllers)
                ChangeClockTime += clockController.SetClockView;
            
            _timeCounting.TimePass += AddSeconds;
            
            // SyncTime();
        }

        public void SetPauseView(bool pause) => _pauseView = pause;

        public DateTime GetAlarmTime()
        {
            return _clockControllers[0].GetAlarmTime();
        }
        
        public async void SyncTime()
        {
            // var startSyncTime = _dateTime;
            var resultSyncTime = await _timeSync.SyncTime();
            if (resultSyncTime.Success)
            {
                _dateTime = resultSyncTime.Message;// + (_dateTime - startSyncTime);
                SetViewClockTime();
            }
        }
        
        private void AddSeconds(float countSeconds)
        {
            _dateTime = _dateTime.AddSeconds(countSeconds);
            SetViewClockTime();
            
            _timeForSync += countSeconds;
            if (_timeForSync >= _intervalSyncTime)
            {
                _timeForSync = 0;
                SyncTime();
            }
        }

        private void SetViewClockTime()
        {
            if (!_pauseView)
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