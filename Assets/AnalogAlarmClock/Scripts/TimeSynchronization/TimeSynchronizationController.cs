using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace AnalogAlarmClock.TimeSynchronization
{
    public class TimeSynchronizationController
    {
        private string _pattern = @"""datetime"":\s*""([^""]*)""";
        private IEnumerable<string> _urlTimeServices = new[]
        {
            "https://timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow",
            "https://worldtimeapi.org/api/timezone/Europe/Moscow",
        };
        
        
        public async Task<DateTime> SyncTime()
        {
            var finalTime = new DateTime();
            while (finalTime == new DateTime())
            {
                foreach (var url in _urlTimeServices)
                {
                    var timeService = await GetTimeFromService(url);
                    if (timeService != string.Empty)
                    {
                        var match = Regex.Match(timeService, _pattern, RegexOptions.IgnoreCase);
                        var dateTime = match.Success ? match.Groups[1].Value : null;
                        var time = DateTimeOffset.Parse(dateTime);
                        if (finalTime != new DateTime())
                        {
                            if (finalTime.TimeOfDay != time.TimeOfDay)
                            {
                                var averageTime = (finalTime.TimeOfDay + time.TimeOfDay) / 2;

                                finalTime = DateTime.Today.Add(averageTime);

                            }
                        }
                        else
                        {
                            finalTime = time.DateTime;
                        }
                    }
                }
            }
            
            return finalTime;
        }

        private async Task<string> GetTimeFromService(string url)
        {
            try
            {
                using UnityWebRequest webRequest = UnityWebRequest.Get(url);
                await webRequest.SendWebRequest();
                for (int i = 0; i < 5; i++)
                {
                    if (webRequest.result == UnityWebRequest.Result.Success)
                    {
                        return webRequest.downloadHandler.text;
                    }
                }

                Debug.Log("Error fetching time");
                return string.Empty;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return string.Empty;
            }
        }

    }
}