using UnityEngine;
using UnityEditor;
using PsychedelicGames.RotorBall.Colours;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Editor for any ColourScheme asset.
    /// </summary>
    [CustomEditor(typeof(ColourScheme))]
    [CanEditMultipleObjects]
    public class ColourSchemeEditor : Editor
    {
        private SerializedProperty backgroundProp;
        private SerializedProperty backgroundPatternOneProp;
        private SerializedProperty backgroundPatternTwoProp;
        private SerializedProperty gradientProp;
        private SerializedProperty UIGradientProp;
        private SerializedProperty backgroundBricksOneProp;
        private SerializedProperty backgroundBricksTwoProp;
        private SerializedProperty backgroundBricksThreeProp;
        private SerializedProperty UIMainOneProp;
        private SerializedProperty UIMainTwoProp;
        private SerializedProperty UIMainThreeProp;
        private SerializedProperty UIIconOneProp;
        private SerializedProperty UIIconTwoProp;
        private SerializedProperty UIIconThreeProp;
        private SerializedProperty UITextOneProp;
        private SerializedProperty UITextTwoProp;
        private SerializedProperty UITextThreeProp;
        private SerializedProperty brickStandardProp;
        private SerializedProperty brickDurableProp;
        private SerializedProperty brickHazardProp;
        private SerializedProperty brickBlockerProp;
        private SerializedProperty brickPowerupInnerProp;
        private SerializedProperty brickPowerupOuterProp;
        private SerializedProperty brickBonusProp;
        private SerializedProperty ballProp;
        private SerializedProperty ballExplosionProp;
        private SerializedProperty ballTrailProp;
        private SerializedProperty highlightedDotProp;
        private SerializedProperty slingshotProp;

        private ColourScheme targetEditor;

        private void OnEnable()
        {
            backgroundProp = serializedObject.FindProperty("background");
            backgroundPatternOneProp = serializedObject.FindProperty("backgroundPatternOne");
            backgroundPatternTwoProp = serializedObject.FindProperty("backgroundPatternTwo");
            gradientProp = serializedObject.FindProperty("gradient");
            UIGradientProp = serializedObject.FindProperty("UIGradient");
            backgroundBricksOneProp = serializedObject.FindProperty("backgroundBricksOne");
            backgroundBricksTwoProp = serializedObject.FindProperty("backgroundBricksTwo");
            backgroundBricksThreeProp = serializedObject.FindProperty("backgroundBricksThree");
            UIMainOneProp = serializedObject.FindProperty("UIMainOne");
            UIMainTwoProp = serializedObject.FindProperty("UIMainTwo");
            UIMainThreeProp = serializedObject.FindProperty("UIMainThree");
            UIIconOneProp = serializedObject.FindProperty("UIIconOne");
            UIIconTwoProp = serializedObject.FindProperty("UIIconTwo");
            UIIconThreeProp = serializedObject.FindProperty("UIIconThree");
            UITextOneProp = serializedObject.FindProperty("UITextOne");
            UITextTwoProp = serializedObject.FindProperty("UITextTwo");
            UITextThreeProp = serializedObject.FindProperty("UITextThree");
            brickStandardProp = serializedObject.FindProperty("brickStandard");
            brickDurableProp = serializedObject.FindProperty("brickDurable");
            brickHazardProp = serializedObject.FindProperty("brickHazard");
            brickBlockerProp = serializedObject.FindProperty("brickBlocker");
            brickPowerupInnerProp = serializedObject.FindProperty("brickPowerupInner");
            brickPowerupOuterProp = serializedObject.FindProperty("brickPowerupOuter");
            brickBonusProp = serializedObject.FindProperty("brickBonus");
            ballProp = serializedObject.FindProperty("ball");
            ballExplosionProp = serializedObject.FindProperty("ballExplosion");
            ballTrailProp = serializedObject.FindProperty("ballTrail");
            highlightedDotProp = serializedObject.FindProperty("highlightedDot");
            slingshotProp = serializedObject.FindProperty("slingshot");

            targetEditor = target as ColourScheme;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(backgroundProp, true);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(backgroundPatternOneProp, true);
            EditorGUILayout.PropertyField(backgroundPatternTwoProp, true);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(gradientProp, true);
            EditorGUILayout.PropertyField(UIGradientProp, true);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(backgroundBricksOneProp, true);
            EditorGUILayout.PropertyField(backgroundBricksTwoProp, true);
            EditorGUILayout.PropertyField(backgroundBricksThreeProp, true);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(UIMainOneProp, true);
            EditorGUILayout.PropertyField(UIMainTwoProp, true);
            EditorGUILayout.PropertyField(UIMainThreeProp, true);
            EditorGUILayout.PropertyField(UIIconOneProp, true);
            EditorGUILayout.PropertyField(UIIconTwoProp, true);
            EditorGUILayout.PropertyField(UIIconThreeProp, true);
            EditorGUILayout.PropertyField(UITextOneProp, true);
            EditorGUILayout.PropertyField(UITextTwoProp, true);
            EditorGUILayout.PropertyField(UITextThreeProp, true);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(brickStandardProp, true);
            EditorGUILayout.PropertyField(brickDurableProp, true);
            EditorGUILayout.PropertyField(brickHazardProp, true);
            EditorGUILayout.PropertyField(brickBlockerProp, true);
            EditorGUILayout.PropertyField(brickPowerupInnerProp, true);
            EditorGUILayout.PropertyField(brickPowerupOuterProp, true);
            EditorGUILayout.PropertyField(brickBonusProp, true);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(ballProp, true);
            EditorGUILayout.PropertyField(ballExplosionProp, true);
            EditorGUILayout.PropertyField(ballTrailProp, true);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(highlightedDotProp, true);
            EditorGUILayout.PropertyField(slingshotProp, true);


            if (!targetEditor.name.Equals("Template")) { if (GUILayout.Button("Apply")) { targetEditor.Apply(); } }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
