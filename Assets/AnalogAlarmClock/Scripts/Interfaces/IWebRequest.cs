using System.Threading.Tasks;

namespace AnalogAlarmClock.Interfaces
{
    public interface IWebRequest
    {
        public Task<Result<string>> Get(string url);
    }
}