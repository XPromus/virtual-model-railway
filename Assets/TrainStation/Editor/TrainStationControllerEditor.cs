using TrainStation.Script;
using UnityEditor;
using UnityEngine;

namespace TrainStation.Editor
{
    [CustomEditor(typeof(TrainStationController))]
    public class TrainStationControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var trainStationControllerTarget = target as TrainStationController;

            EditorGUILayout.BeginVertical();

            var targetTrack = serializedObject.FindProperty("targetTrack");
            EditorGUILayout.PropertyField(targetTrack);
            
            var knotBeforeTrainStation = serializedObject.FindProperty("knotBeforeTrainStation");
            EditorGUILayout.PropertyField(knotBeforeTrainStation);
            
            var startPoint = serializedObject.FindProperty("startPoint");
            EditorGUILayout.PropertyField(startPoint);
            
            var endPoint = serializedObject.FindProperty("endPoint");
            EditorGUILayout.PropertyField(endPoint);
            
            EditorGUILayout.Space();

            if (GUILayout.Button("Align Station"))
            {
                
            }
            
            EditorGUILayout.EndVertical();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}