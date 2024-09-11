using System;

namespace AnalogAlarmClock.AlarmClock
{
    public class AlarmClockController
    {
        private readonly AlarmClockView _alarmClockView;
        private string _messageSetAlarm = "Будильник установлен на ";

        public event Action OnConfirmAlarmSetting;
        public event Action OnCancelAlarmSetting;

        public AlarmClockController(AlarmClockView alarmClockView)
        {
            _alarmClockView = alarmClockView;
            _alarmClockView.OnConfirmAlarmSetting += ConfirmAlarmSetting;
            _alarmClockView.OnCancelAlarmSetting += CancelAlarmSetting;
        }

        public void StartAlarmSetting()
        {
            _alarmClockView.SetActive(true);
            _alarmClockView.SetButtonsActive(true);
        }
        private void CancelAlarmSetting()
        {
            _alarmClockView.SetActive(false);
            _alarmClockView.SetButtonsActive(false);
            _alarmClockView.SetAlertText("");
            OnCancelAlarmSetting?.Invoke();
        }
        private void ConfirmAlarmSetting()
        {
            _alarmClockView.SetButtonsActive(false);
            OnConfirmAlarmSetting?.Invoke();
        }

        public void SetAlarm(DateTime alarmDataTime)
        {
            _alarmClockView.SetAlertText(_messageSetAlarm + $"{alarmDataTime.Hour}:{alarmDataTime.Minute}:{alarmDataTime.Second}");
        }
    }
}