namespace AnalogAlarmClock.Interfaces
{
    public interface IClockView : IView
    {
        public void SetHours(int hours);
        public void SetMinutes(int minutes);
        public void SetSeconds(int seconds);
    }
}