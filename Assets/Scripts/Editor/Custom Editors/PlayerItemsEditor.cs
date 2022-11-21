using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    using Files;

    /// <summary>
    /// Custom editor for PlayerItems.
    /// </summary>
    [CustomEditor(typeof(PlayerItems))]
    [CanEditMultipleObjects]
    public class PlayerItemsEditor : Editor
    {
        private SerializedProperty stylesProp;
        private SerializedProperty trailsProp;
        private SerializedProperty explosionsProp;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(stylesProp, true);
            EditorGUILayout.PropertyField(trailsProp, true);
            EditorGUILayout.PropertyField(explosionsProp, true);

            if (GUILayout.Button("Give All Items To Player"))
            {
                PlayerFile file = PlayerFile.GetFile();

                file.StyleItems.Clear();
                file.TrailItems.Clear();
                file.ExplosionItems.Clear();

                int styleCount = stylesProp.arraySize;
                int trailCount = trailsProp.arraySize;
                int explosionCount = explosionsProp.arraySize;

                for (int i = 0; i < styleCount; i++)
                {
                    file.StyleItems.Add(stylesProp.GetArrayElementAtIndex(i).objectReferenceValue.name);
                }
                for (int i = 0; i < trailCount; i++)
                {
                    file.TrailItems.Add(trailsProp.GetArrayElementAtIndex(i).objectReferenceValue.name);
                }
                for (int i = 0; i < explosionCount; i++)
                {
                    file.ExplosionItems.Add(explosionsProp.GetArrayElementAtIndex(i).objectReferenceValue.name);
                }

                PlayerFile.SaveFile();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            stylesProp = serializedObject.FindProperty("styles");
            trailsProp = serializedObject.FindProperty("trails");
            explosionsProp = serializedObject.FindProperty("explosions");
        }
    }
}
