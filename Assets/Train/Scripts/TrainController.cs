using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace Train.Scripts
{
    public class TrainController : MonoBehaviour
    {
        [SerializeField] private SplineContainer targetTrack;
        public SplineContainer TargetTrack
        {
            get => targetTrack;
            set => targetTrack = value;
        }
        
        [SerializeField] private float maxTrainSpeed;
        public float MaxTrainSpeed
        {
            get => maxTrainSpeed;
            set => maxTrainSpeed = value;
        }
        
        [SerializeField] private float currentTrainSpeed = 1f;
        public float CurrentTrainSpeed
        {
            get => currentTrainSpeed;
            set => currentTrainSpeed = value;
        }
        
        [SerializeField] private float rotationSpeed = 1f;
        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }
        
        private float _currentDistance;

        private void Start()
        {
            TeleportTrainToFirstKnot();
        }

        private void Update()
        {
            AnimateTrainAlongSpline();
        }
        
        private void AnimateTrainAlongSpline()
        {
            var targetPosition = TargetTrack.EvaluatePosition(_currentDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentTrainSpeed * Time.deltaTime);
            var targetDirection = (Vector3) TargetTrack.EvaluateTangent(_currentDistance);
            
            if (targetDirection != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(targetDirection, transform.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }

            if (_currentDistance >= 1f)
            {
                _currentDistance = 0f;
            }
            else
            {
                var splineLength = TargetTrack.CalculateLength();
                var movement = currentTrainSpeed * Time.deltaTime / splineLength;
                _currentDistance += movement;
            }
        }

        public void TeleportTrainToFirstKnot()
        {
            if (!TargetTrack)
            {
                Debug.LogError("Target track hasn't been set.");
                return;
            }
            
            var firstKnot = TargetTrack.Spline.Knots.ToArray()[0];
            transform.rotation = firstKnot.Rotation;
            transform.position = TargetTrack.transform.TransformPoint(firstKnot.Position);
        }
    }
}