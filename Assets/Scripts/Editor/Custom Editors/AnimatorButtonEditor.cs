using UnityEngine;
using UnityEditor;
using PsychedelicGames.RotorBall.UI;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Custom editor for AnimatorButton.
    /// </summary>
    [CustomEditor(typeof(AnimatorButton))]
    [CanEditMultipleObjects]
    public class AnimatorButtonEditor : Editor
    {
        private SerializedProperty parameterNameProp;
        private SerializedProperty parameterBoolProp;
        private SerializedProperty parameterTypeProp;
        private SerializedProperty animatorProp;

        private void OnEnable()
        {
            parameterNameProp = serializedObject.FindProperty("parameterName");
            parameterBoolProp = serializedObject.FindProperty("parameterBool");
            parameterTypeProp = serializedObject.FindProperty("parameterType");
            animatorProp = serializedObject.FindProperty("animator");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(animatorProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(parameterNameProp);

            if (parameterTypeProp.enumValueIndex == (int)AnimatorButton.Parameter.Bool)
            {
                EditorGUILayout.PropertyField(parameterBoolProp);
            }

            EditorGUILayout.PropertyField(parameterTypeProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}