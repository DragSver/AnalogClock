using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AnalogAlarmClock.Interfaces;

namespace AnalogAlarmClock.TimeSynchronization
{
    public class TimeSynchronizationController : ITimeSynchronizationController
    {
        private readonly IWebRequest _webRequest = new WebRequest();
        
        private readonly string _pattern = @"""datetime"":\s*""([^""]*)""";
        
        private readonly IEnumerable<string> _urlTimeServices = new[]
        {
            "https://timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow",
            // "https://worldtimeapi.org/api/timezone/Europe/Moscow",
        };


        public async Task<Result<DateTime>> SyncTime()
        {
            var finalTime = new DateTime();
            foreach (var url in _urlTimeServices)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var timeServiceRequest = await _webRequest.Get(url);
                stopwatch.Stop();
                if (timeServiceRequest.Success)
                {
                    var time = TimeParse(timeServiceRequest.Message);
                    if (finalTime != new DateTime())
                    {
                        if (finalTime.TimeOfDay != time.TimeOfDay)
                        {
                            // var averageTime = (finalTime.TimeOfDay + time.TimeOfDay) / 2;
                            // var roundedTimeSpan = TimeSpan.FromSeconds(Math.Ceiling(averageTime.TotalSeconds));
                            //
                            // finalTime = DateTime.Today.Add(roundedTimeSpan);

                            finalTime = DateTime.Today.Add(TimeSpan.FromSeconds(
                                Math.Max(
                                    time.TimeOfDay.Seconds+TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).Seconds, 
                                    finalTime.TimeOfDay.Seconds)));
                        }
                    }
                    else
                    {
                        finalTime = time;
                    }
                }
            }

            return new Result<DateTime>(finalTime != new DateTime(), finalTime);
        }

        private DateTime TimeParse(string timeServiceRequest)
        {
            var match = Regex.Match(timeServiceRequest, _pattern, RegexOptions.IgnoreCase);
            var dateTime = match.Success ? match.Groups[1].Value : null;
            return DateTimeOffset.Parse(dateTime).DateTime;
        }

    }
}