using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Custom inspector LevelModifiers.
    /// </summary>
    [CustomEditor(typeof(LevelModifiers))]
    [CanEditMultipleObjects]
    public class LevelModifiersEditor : Editor
    {
        private SerializedProperty brickCountProp;
        private SerializedProperty ballCountProp;
        private SerializedProperty extraBallCountProp;
        private SerializedProperty ballLifeTimeProp;
        private SerializedProperty ballSpeedProp;
        private SerializedProperty ballSizeProp;
        private SerializedProperty designatedSceneProp;

        private void OnEnable()
        {
            brickCountProp = serializedObject.FindProperty("brickCount");
            ballCountProp = serializedObject.FindProperty("ballCount");
            extraBallCountProp = serializedObject.FindProperty("extraBallCount");
            ballLifeTimeProp = serializedObject.FindProperty("ballLifeTime");
            ballSpeedProp = serializedObject.FindProperty("ballSpeed");
            ballSizeProp = serializedObject.FindProperty("ballSize");
            designatedSceneProp = serializedObject.FindProperty("designatedScene");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Brick Count: " + brickCountProp.intValue.ToString());
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(ballCountProp);
            EditorGUILayout.PropertyField(extraBallCountProp);
            EditorGUILayout.PropertyField(ballLifeTimeProp);
            EditorGUILayout.PropertyField(ballSpeedProp);
            EditorGUILayout.PropertyField(ballSizeProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(designatedSceneProp);

            if (GUILayout.Button("Set Modifiers"))
            {
                var currentScene = EditorSceneManager.GetActiveScene();
                var designatedScene = designatedSceneProp.objectReferenceValue as GameScene;

                if (designatedScene)
                {
                    if (currentScene.path.Equals(designatedScene.ScenePath)) { brickCountProp.intValue = GameObject.FindGameObjectsWithTag("Destructible").Length; }
                    else { Debug.LogWarning("This is not the designated scene!"); }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
