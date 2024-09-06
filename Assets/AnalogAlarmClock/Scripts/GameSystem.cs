using System.Collections.Generic;
using AnalogAlarmClock.AnalogClock;
using AnalogAlarmClock.ElectronicClock;
using AnalogAlarmClock.Interfaces;
using UnityEngine;

namespace AnalogAlarmClock
{
    public class GameSystem : MonoBehaviour
    {
        [SerializeField] private AnalogClockView _analogClockView;
        [SerializeField] private ElectronicClockView _electronicClockView;
        [SerializeField] private TimeCounting _timeCounting;

        private ClockTimeController _clockTimeController;
        private List<IClockController> _clockControllers;

        private void Awake()
        {
            _clockControllers = new List<IClockController>
            {
                new AnalogClockController(_analogClockView),
                new ElectronicClockController(_electronicClockView)
            };

            _clockTimeController = new ClockTimeController(_clockControllers, _timeCounting);
        }
    }
}