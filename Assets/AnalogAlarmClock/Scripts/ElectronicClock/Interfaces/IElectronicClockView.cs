using AnalogAlarmClock.Interfaces;

namespace AnalogAlarmClock.ElectronicClock.Interfaces
{
    public interface IElectronicClockView : IView
    {
        public void SetHourText(string hourText);
        public void SetMinuteText(string minuteText);
        public void SetSecondText(string secondText);
        
        public string GetHourText();
        public string GetMinuteText();
        public string GetSecondText();
    }
}