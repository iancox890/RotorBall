using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Editor for any GameScene asset.
    /// </summary>
    [CustomEditor(typeof(GameScene))]
    public class GameSceneEditor : Editor
    {
        private SerializedProperty pathProp;
        private SerializedProperty nameProp;

        public override void OnInspectorGUI()
        {
            var currentScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathProp.stringValue);
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUILayout.ObjectField("Scene", currentScene, typeof(SceneAsset), false) as SceneAsset;

            if (newScene)
            {
                if (EditorGUI.EndChangeCheck())
                {
                    pathProp.stringValue = AssetDatabase.GetAssetPath(newScene);
                    nameProp.stringValue = newScene.name;
                }

                if (GUILayout.Button("Open")) { EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(newScene)); }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            pathProp = serializedObject.FindProperty("scenePath");
            nameProp = serializedObject.FindProperty("sceneName");
        }
    }
}
