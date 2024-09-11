using System;
using System.Threading.Tasks;
using AnalogAlarmClock.Interfaces;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace AnalogAlarmClock.TimeSynchronization
{
    public class WebRequest : IWebRequest
    {
        private const int _countRequestAttempts = 5;

        public async Task<Result<string>> Get(string url)
        {
            try
            {
                for (int i = 0; i < _countRequestAttempts; i++)
                {
                    var webRequest = UnityWebRequest.Get(url);
                    Debug.Log($"Launch {i+1}/{_countRequestAttempts} request for a \"{url}\"...");
                    await webRequest.SendWebRequest();
                    if (webRequest.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log($"Success request for a \"{url}\".");
                        return new Result<string>(true, webRequest.downloadHandler.text);
                    }
                    else
                    {
                        Debug.Log($"Bad request for a \"{url}\".");
                    }
                }

                Debug.Log($"Error fetching time for a \"{url}\".");
                return new Result<string>(false, string.Empty);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return new Result<string>(false, string.Empty);
            }
        }
    }
}