using System;
using AnalogAlarmClock.Interfaces;

namespace AnalogAlarmClock.ElectronicClock
{
    public class ElectronicClockController : IClockController
    {
        private readonly ElectronicClockView _electronicClockView;

        public ElectronicClockController(ElectronicClockView electronicClockView)
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
    }
}