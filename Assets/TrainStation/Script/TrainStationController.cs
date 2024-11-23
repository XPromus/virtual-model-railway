using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace TrainStation.Script
{
    public class TrainStationController : MonoBehaviour
    {
        [SerializeField] private SplineContainer targetTrack;
        public SplineContainer TargetTrack
        {
            get => targetTrack;
            set => targetTrack = value;
        }
        
        [SerializeField] private int knotBeforeTrainStation;
        public int KnotBeforeTrainStation
        {
            get => knotBeforeTrainStation;
            set => knotBeforeTrainStation = value;
        }
        
        [SerializeField] private Transform startPoint;
        public Transform StartPoint
        {
            get => startPoint;
            set => startPoint = value;
        }
        
        [SerializeField] private Transform endPoint;
        public Transform EndPoint
        {
            get => endPoint;
            set => endPoint = value;
        }

        private void AlignTrackToTrainStation()
        {
            var beginKnotPosition = TargetTrack.transform.InverseTransformPoint(StartPoint.position);
            var startKnot = CreateNewKnot(beginKnotPosition);
            
            var endKnotPosition = TargetTrack.transform.InverseTransformPoint(EndPoint.position);
            var endKnot = CreateNewKnot(endKnotPosition);
            
            var knotList = TargetTrack.Spline.Knots.ToList();
        }

        private static BezierKnot CreateNewKnot(Vector3 position)
        {
            return new BezierKnot
            {
                Position = position
            };
        }
    }
}