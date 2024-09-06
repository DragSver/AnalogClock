using UnityEngine;

namespace AnalogAlarmClock.AnalogClock
{
    public class AnalogClockView : MonoBehaviour
    {
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minuteHand;
        [SerializeField] private Transform _secondHand;

        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            _hourHand.rotation = Quaternion.identity;
            _minuteHand.rotation = Quaternion.identity;
            _secondHand.rotation = Quaternion.identity;
        }
        
        public void SetHours(float hoursEulerAngle) => EulerRotate(hoursEulerAngle, _hourHand);
        public void SetMinutes(float minutesEulerAngle) => EulerRotate(minutesEulerAngle, _minuteHand);
        public void SetSeconds(float secondsEulerAngle) => EulerRotate(secondsEulerAngle, _secondHand);

        private void EulerRotate(float eulerAngle, Transform handTransform)
        {
            Vector3 rotate = handTransform.eulerAngles;
            rotate.z = eulerAngle;
            handTransform.rotation = Quaternion.Euler(rotate);
        }
    }
}