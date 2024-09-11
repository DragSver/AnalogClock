using System;
using System.Collections.Generic;
using AnalogAlarmClock.AlarmClock;
using AnalogAlarmClock.AnalogClock;
using AnalogAlarmClock.ElectronicClock;
using AnalogAlarmClock.Interfaces;
using AnalogAlarmClock.TimeSynchronization;
using UnityEngine;

namespace AnalogAlarmClock
{
    public class GameSystem : MonoBehaviour
    {
        [SerializeField] private AnalogClockView _analogClockView;
        [SerializeField] private InputFieldElectronicClockView _electronicClockView;
        [SerializeField] private TimeCounting _timeCounting;
        [SerializeField] private AlarmClockView _alarmClockView;
        
        private ClockTimeController _clockTimeController;
        private AlarmClockController _alarmClockController;
        private List<IClockController> _clockControllers;

        private void Awake()
        {
            _electronicClockView.OnSelect += StartAlarmSetting;
            
            _clockControllers = new List<IClockController>
            {
                new AnalogClockController(_analogClockView),
                new ElectronicClockController(_electronicClockView)
            };

            _clockTimeController = new ClockTimeController(_clockControllers, new TimeSynchronizationController(), _timeCounting);
            _alarmClockController = new AlarmClockController(_alarmClockView);

            _alarmClockController.OnConfirmAlarmSetting += ConfirmAlarmSetting;
            _alarmClockController.OnCancelAlarmSetting += CancelAlarmSetting;
        }
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                _clockTimeController.SyncTime();
            }
        }

        private void StartAlarmSetting()
        {
            _clockTimeController.SetPauseView(true);
            _alarmClockController.StartAlarmSetting();
        }
        private void ConfirmAlarmSetting()
        {
            _clockTimeController.SetPauseView(false);
            var alarmDateTime = _clockTimeController.GetAlarmTime();
            _alarmClockController.SetAlarm(alarmDateTime);
        }
        private void CancelAlarmSetting()
        {
            _clockTimeController.SetPauseView(false);
        }
    }
}