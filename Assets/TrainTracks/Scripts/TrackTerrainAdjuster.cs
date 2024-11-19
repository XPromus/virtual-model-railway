using System;
using UnityEngine;
using UnityEngine.Splines;

namespace TrainTracks.Scripts
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class TrackTerrainAdjuster : MonoBehaviour
    {
        public Terrain targetTerrain;
        public SplineContainer targetTrack;

        public void AdjustSplineKnots()
        {
            var knots = targetTrack.Spline.ToArray();
            for (var i = 0; i < knots.Length; i++)
            {
                var targetKnot = knots[i];
                var knotPosition = new Vector3(targetKnot.Position.x, targetKnot.Position.y, targetKnot.Position.z);
                var worldKnotPosition = targetTrack.transform.position + knotPosition;
                var terrainHeightAtKnot = targetTerrain.SampleHeight(worldKnotPosition);
                
                targetKnot.Position = new Vector3
                (
                    targetKnot.Position.x, 
                    terrainHeightAtKnot - targetTrack.transform.position.y, 
                    targetKnot.Position.z
                );
                targetTrack.Spline.SetKnot(i, targetKnot);    
            }
        }

        private Vector3 ToWorldPosition(Vector3 reference, Vector3 input)
        {
            return reference + input;
        }
    }
}