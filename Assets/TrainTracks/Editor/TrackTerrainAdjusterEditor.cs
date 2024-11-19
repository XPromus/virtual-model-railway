using TrainTracks.Scripts;
using UnityEditor;
using UnityEngine;

namespace TrainTracks.Editor
{
    [CustomEditor(typeof(TrackTerrainAdjuster))]
    public class TrackTerrainAdjusterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var trackTerrainAdjuster = target as TrackTerrainAdjuster;

            var toggleButtonStyle = new GUIStyle(GUI.skin.button);
            toggleButtonStyle.fontStyle = FontStyle.Bold;
            toggleButtonStyle.fixedHeight = 20;
            toggleButtonStyle.fixedWidth = 270;
            toggleButtonStyle.alignment = TextAnchor.MiddleCenter;

            var originalColor = GUI.backgroundColor;
            
            EditorGUILayout.BeginVertical();
            
            GUILayout.FlexibleSpace();

            var targetTerrain = serializedObject.FindProperty("targetTerrain");
            EditorGUILayout.PropertyField(targetTerrain);
            EditorGUILayout.Space();
            
            var targetTrack = serializedObject.FindProperty("targetTrack");
            EditorGUILayout.PropertyField(targetTrack);
            EditorGUILayout.Space();
            
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Adjust Track to Terrain"))
            {
                trackTerrainAdjuster!.AdjustSplineKnots();
            }

            GUI.backgroundColor = originalColor;
            
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}