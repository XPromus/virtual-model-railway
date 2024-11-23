using Train.Scripts;
using UnityEditor;
using UnityEngine;

namespace Train.Editor
{
    [CustomEditor(typeof(TrainController))]
    public class TrainControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var trainControllerTarget = target as TrainController;

            EditorGUILayout.BeginVertical();

            var targetTrack = serializedObject.FindProperty("targetTrack");
            EditorGUILayout.PropertyField(targetTrack);

            var maxTrainSpeed = serializedObject.FindProperty("maxTrainSpeed");
            EditorGUILayout.PropertyField(maxTrainSpeed);

            var currentTrainSpeed = serializedObject.FindProperty("currentTrainSpeed");
            EditorGUILayout.PropertyField(currentTrainSpeed);

            var rotationSpeed = serializedObject.FindProperty("rotationSpeed");
            EditorGUILayout.PropertyField(rotationSpeed);

            var lockLocalYRotation = serializedObject.FindProperty("lockLocalYRotation");
            EditorGUILayout.PropertyField(lockLocalYRotation);
            
            EditorGUILayout.Space();

            if (GUILayout.Button("Teleport Train to 1st Knot"))
            {
                trainControllerTarget!.TeleportTrainToFirstKnot();
            }
            
            EditorGUILayout.EndVertical();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}