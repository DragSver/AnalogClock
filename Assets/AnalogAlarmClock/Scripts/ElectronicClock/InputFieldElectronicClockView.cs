using System;
using AnalogAlarmClock.ElectronicClock.Interfaces;
using TMPro;
using UnityEngine;

namespace AnalogAlarmClock.ElectronicClock
{
    public class InputFieldElectronicClockView : MonoBehaviour, IElectronicClockView
    {
        [SerializeField] private TMP_InputField _hourText;
        [SerializeField] private TMP_InputField _minuteText;
        [SerializeField] private TMP_InputField _secondText;

        public event Action OnSelect;

        private void Awake()
        {
            _hourText.onSelect.AddListener(OnSelectInvoke);
            _minuteText.onSelect.AddListener(OnSelectInvoke);
            _secondText.onSelect.AddListener(OnSelectInvoke);
        }
        

        public void SetActive(bool active) => gameObject.SetActive(active);

        public void SetHourText(string hourText) => _hourText.SetTextWithoutNotify(hourText);
        public void SetMinuteText(string minuteText) => _minuteText.SetTextWithoutNotify(minuteText);
        public void SetSecondText(string secondText) => _secondText.SetTextWithoutNotify(secondText);

        public string GetHourText() => _hourText.text;
        public string GetMinuteText() => _minuteText.text;
        public string GetSecondText() => _secondText.text;

        private void OnSelectInvoke(string fieldName) => OnSelect?.Invoke();
    }
}