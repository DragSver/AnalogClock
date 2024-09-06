using System;
using AnalogAlarmClock.Interfaces;

namespace AnalogAlarmClock.AnalogClock
{
    public class AnalogClockController : IClockController
    {
        private readonly AnalogClockView _analogClockView;

        public AnalogClockController(AnalogClockView analogClockView)
        {
            _analogClockView = analogClockView;
        }

        public void SetClockView(DateTime dateTime)
        {
            SetHours(dateTime);
            SetMinutes(dateTime);
            SetSeconds(dateTime);
        }
        
        public void SetHours(DateTime dateTime) => 
            _analogClockView.SetHours(ConvertClockIntoEulerAnglesHours(dateTime));
        public void SetMinutes(DateTime dateTime) => 
            _analogClockView.SetMinutes(ConvertClockIntoEulerAnglesMinutes(dateTime));
        public void SetSeconds(DateTime dateTime) => 
            _analogClockView.SetSeconds(ConvertClockIntoEulerAnglesSeconds(dateTime));
        
        
        private float ConvertClockIntoEulerAnglesHours(DateTime dateTime)
        {
            var hoursAngle = 360f / 12f * dateTime.Hour;
            var minutesAngle = 360f / 12f / 60f * dateTime.Minute;
            return (minutesAngle + hoursAngle) * -1;
        }
        private float ConvertClockIntoEulerAnglesMinutes(DateTime dateTime)
        {
            var minutesAngle = 360f / 60f * dateTime.Minute;
            var secondsAngle = 360f / 60f / 60f * dateTime.Second;
            return (minutesAngle + secondsAngle) * -1;
        }
        private float ConvertClockIntoEulerAnglesSeconds(DateTime dateTime)
        {
            var secondsAngle = 360 / 60 * dateTime.Second;
            return secondsAngle * -1;
        }
    }
}