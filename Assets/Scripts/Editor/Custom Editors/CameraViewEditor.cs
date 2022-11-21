using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Custom editor for the CameraSize.
    /// </summary>
    [CustomEditor(typeof(CameraView))]
    public class CameraViewEditor : Editor
    {
        private SerializedProperty closeSizeProp;
        private SerializedProperty mediumSizeProp;
        private SerializedProperty farSizeProp;
        private SerializedProperty overridePreferenceProp;

        private enum View { Close, Medium, Far }
        private View view;

        private CameraView targetEditor;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(closeSizeProp);
            EditorGUILayout.PropertyField(mediumSizeProp);
            EditorGUILayout.PropertyField(farSizeProp);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(overridePreferenceProp);

            EditorGUILayout.Space();

            if (GUILayout.Button("Apply Close"))
            {
                targetEditor.SetView(0);
            }
            else if (GUILayout.Button("Apply Medium"))
            {
                targetEditor.SetView(1);
            }
            else if (GUILayout.Button("Apply Far"))
            {
                targetEditor.SetView(2);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            closeSizeProp = serializedObject.FindProperty("closeSize");
            mediumSizeProp = serializedObject.FindProperty("mediumSize");
            farSizeProp = serializedObject.FindProperty("farSize");
            overridePreferenceProp = serializedObject.FindProperty("overridePreference");

            targetEditor = target as CameraView;
        }
    }
}
