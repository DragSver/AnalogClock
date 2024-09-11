using System;
using AnalogAlarmClock.ElectronicClock.Interfaces;
using AnalogAlarmClock.Interfaces;

namespace AnalogAlarmClock.ElectronicClock
{
    public class ElectronicClockController : IClockController
    {
        private readonly IElectronicClockView _electronicClockView;

        public ElectronicClockController(IElectronicClockView electronicClockView)
        {
            _electronicClockView = electronicClockView;
        }
        
        public void SetClockView(DateTime dateTime)
        {
            SetHours(dateTime);
            SetMinutes(dateTime);
            SetSeconds(dateTime);
        }

        public void SetHours(DateTime dateTime) => _electronicClockView.SetHourText(dateTime.Hour.ToString("D2"));
        public void SetMinutes(DateTime dateTime) => _electronicClockView.SetMinuteText(dateTime.Minute.ToString("D2"));
        public void SetSeconds(DateTime dateTime) => _electronicClockView.SetSecondText(dateTime.Second.ToString("D2"));

        public DateTime GetAlarmTime()
        {
            var hour = int.Parse(_electronicClockView.GetHourText());
            var minute = int.Parse(_electronicClockView.GetMinuteText());
            var second = int.Parse(_electronicClockView.GetSecondText());
            var dateTime = DateTime.Today;
            dateTime = dateTime.Add(new TimeSpan(hour, minute, second));
            return dateTime;
        }
    }
}