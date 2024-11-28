using UnityEngine;

namespace Train.Scripts
{
    public class SpeedRegulatorController : MonoBehaviour
    {
        [SerializeField] private float minEulerRotation = 0f;
        [SerializeField] private float maxEulerRotation = 300f;
        [SerializeField] private Transform knobTransform;
        [SerializeField] private Transform baseTransform;

        [SerializeField] private TrainController trainController;

        private float _currentSpeedRegulatorPosition;
        private float currentSpeedRegulatorPosition
        {
            get => _currentSpeedRegulatorPosition;
            set
            {
                _currentSpeedRegulatorPosition = Mathf.Clamp01(value);
                // Debug.Log(_currentSpeedRegulatorPosition);
                trainController.CurrentTrainSpeed = trainController.MaxTrainSpeed * _currentSpeedRegulatorPosition;
            }
        }

        private void Update()
        {
            //var newRotation = new Vector3(-90f, ClampAngle(knobTransform.rotation.y, minEulerRotation, maxEulerRotation), 0f);
            //knobTransform.rotation = Quaternion.Euler(newRotation);
            currentSpeedRegulatorPosition = knobTransform.localRotation.eulerAngles.y / maxEulerRotation;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < 0f) angle = 360f + angle;
            return angle > 180f ? Mathf.Max(angle, 360f + min) : Mathf.Min(angle, max);
        }
    }
}