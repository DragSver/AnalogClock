using System;
using AnalogAlarmClock.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnalogAlarmClock.AlarmClock
{
    public class AlarmClockView : MonoBehaviour, IView
    {
        [SerializeField] private Button _cancelAlarmSetting;
        [SerializeField] private Button _confirmAlarmSetting;
        [SerializeField] private TextMeshProUGUI _alert;

        public event Action OnConfirmAlarmSetting;
        public event Action OnCancelAlarmSetting;


        private void Start()
        {
            if (OnConfirmAlarmSetting != null) _confirmAlarmSetting.onClick.AddListener(OnConfirmAlarmSetting.Invoke);
            if (OnCancelAlarmSetting != null) _cancelAlarmSetting.onClick.AddListener(OnCancelAlarmSetting.Invoke);
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetAlertText(string alertText)
        {
            _alert.SetText(alertText);
        }

        public void SetButtonsActive(bool active)
        {
            _cancelAlarmSetting.gameObject.SetActive(active);
            _confirmAlarmSetting.gameObject.SetActive(active);
        }
    }
}