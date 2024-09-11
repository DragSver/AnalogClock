using System;

namespace AnalogAlarmClock.Interfaces
{
    public interface IClockController
    {
        public void SetClockView(DateTime clockTime);
        
        public void SetHours(DateTime clockTime);
        public void SetMinutes(DateTime clockTime);
        public void SetSeconds(DateTime clockTime);

        public DateTime GetAlarmTime();
    }
}