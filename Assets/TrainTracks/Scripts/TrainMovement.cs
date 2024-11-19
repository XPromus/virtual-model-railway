using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

namespace TrainTracks.Scripts
{
    [CustomEditor(typeof(TrackTerrainAdjuster))]
    public class TrainMovement : MonoBehaviour
    {
        [SerializeField] private SplineContainer trainTrack;

        private float _lengthOfTrack;

        private void Start()
        {
            _lengthOfTrack = trainTrack.CalculateLength();
        }

        private void Update()
        {
            trainTrack.EvaluatePosition(Time.deltaTime);
        }
    }
}