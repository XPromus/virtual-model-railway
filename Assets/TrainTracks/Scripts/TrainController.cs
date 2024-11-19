using System;
using UnityEngine;
using UnityEngine.Splines;

namespace TrainTracks.Scripts
{
    [RequireComponent(typeof(SplineAnimate))]
    public class TrainController : MonoBehaviour
    {
        [SerializeField] private SplineContainer targetTrack;
        public SplineContainer TargetTrack
        {
            get => targetTrack;
            set => targetTrack = value;
        }

        [SerializeField] private float trainSpeed;
        public float TrainSpeed
        {
            get => trainSpeed;
            set
            {
                trainSpeed = Mathf.Clamp(value, 0f, maxTrainSpeed);
                _splineAnimate.Play();
                OnTrainSpeedSet();
            }
        }

        [SerializeField] private float maxTrainSpeed;

        public float MaxTrainSpeed
        {
            get => maxTrainSpeed;
            set
            {
                maxTrainSpeed = value;
                trainSpeed = Mathf.Clamp(trainSpeed, 0f, maxTrainSpeed);
            }
        }

        private SplineAnimate _splineAnimate;

        private void Awake()
        {
            _splineAnimate = GetComponent<SplineAnimate>();
            _splineAnimate.Update();
            OnTrainSpeedSet();
        }

        private void OnTrainSpeedSet()
        {
            _splineAnimate.MaxSpeed = trainSpeed;
        }
    }
}