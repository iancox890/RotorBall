using UnityEngine;
using UnityEditor;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Custom editor for BrickAudio.
    /// </summary>
    [CustomEditor(typeof(BrickAudio))]
    [CanEditMultipleObjects]
    public class BrickAudioEditor : Editor
    {
        private SerializedProperty hitClipsProp;
        private SerializedProperty finalHitsClipProp;
        private SerializedProperty bonusClipProp;
        private SerializedProperty isDurableProp;

        private BrickAudio targetEditor;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(hitClipsProp, true);

            if (targetEditor.GetComponent<Durable>() != null)
            {
                isDurableProp.boolValue = true;
                EditorGUILayout.PropertyField(finalHitsClipProp, true);
            }
            EditorGUILayout.PropertyField(bonusClipProp);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            hitClipsProp = serializedObject.FindProperty("hitClips");
            finalHitsClipProp = serializedObject.FindProperty("finalHitClips");
            bonusClipProp = serializedObject.FindProperty("bonusClip");
            isDurableProp = serializedObject.FindProperty("isDurable");

            targetEditor = target as BrickAudio;
        }
    }
}
