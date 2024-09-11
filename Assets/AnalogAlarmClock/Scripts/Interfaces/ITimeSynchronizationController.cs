using System;
using System.Threading.Tasks;

namespace AnalogAlarmClock.Interfaces
{
    public interface ITimeSynchronizationController
    {
        public Task<Result<DateTime>> SyncTime();
    }
}