using AnalogAlarmClock.ElectronicClock.Interfaces;
using TMPro;
using UnityEngine;

namespace AnalogAlarmClock.ElectronicClock
{
    public class ElectronicClockView : MonoBehaviour, IElectronicClockView
    {
        [SerializeField] private TextMeshProUGUI _hourText;
        [SerializeField] private TextMeshProUGUI _minuteText;
        [SerializeField] private TextMeshProUGUI _secondText;

        public void SetActive(bool active) => gameObject.SetActive(active);
        
        public void SetHourText(string hourText) => _hourText.SetText(hourText);
        public void SetMinuteText(string minuteText) => _minuteText.SetText(minuteText);
        public void SetSecondText(string secondText) => _secondText.SetText(secondText);

        public string GetHourText() => _hourText.text;
        public string GetMinuteText() => _minuteText.text;
        public string GetSecondText() => _secondText.text;
    }
}