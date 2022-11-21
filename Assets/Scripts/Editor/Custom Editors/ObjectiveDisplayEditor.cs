using UnityEngine;
using UnityEditor;
using PsychedelicGames.RotorBall.UI;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Custom editor for ObjectiveDisplay.
    /// </summary>
    [CustomEditor(typeof(ObjectiveDisplay))]
    [CanEditMultipleObjects]
    public class ObjectiveDisplayEditor : Editor
    {
        private SerializedProperty objectiveNumberProp;
        private SerializedProperty useCurrentDataProp;
        private SerializedProperty levelNumberProp;
        private SerializedProperty imagesProp;
        private SerializedProperty completedProp;
        private SerializedProperty incompletedProp;

        private void OnEnable()
        {
            objectiveNumberProp = serializedObject.FindProperty("objectiveNumber");
            useCurrentDataProp = serializedObject.FindProperty("useCurrentData");
            levelNumberProp = serializedObject.FindProperty("levelNumber");
            imagesProp = serializedObject.FindProperty("images");
            completedProp = serializedObject.FindProperty("completed");
            incompletedProp = serializedObject.FindProperty("incompleted");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(objectiveNumberProp);
            if (objectiveNumberProp.enumValueIndex == (int)ObjectiveDisplay.Objective.All) { EditorGUILayout.HelpBox(new GUIContent("Ensure there are 3 elements under images.")); }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(useCurrentDataProp);
            if (!useCurrentDataProp.boolValue) { EditorGUILayout.PropertyField(levelNumberProp); }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(imagesProp, true);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(completedProp);
            EditorGUILayout.PropertyField(incompletedProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
