namespace AnalogAlarmClock
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Message { get; set; }
        
        public Result(bool success, T message)
        {
            Success = success;
            Message = message;
        }
    }
}