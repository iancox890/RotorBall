using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Reflects a given object about a specified axis.
    /// </summary>
    public  class AxisReflection
    {
        private enum Axes { X, Y }

        [MenuItem("GameObject/Psychedelic Games/Reflect Upon Axis/x %#x", false, -1)]
        private static void xAxisReflectionMenu() => Reflect(Axes.X);

        [MenuItem("GameObject/Psychedelic Games/Reflect Upon Axis/y %#y", false, -1)]
        private static void yAxisReflectionMenu() => Reflect(Axes.Y);

        private static void Reflect(Axes axis)
        {
            var selectedTransform = Selection.activeGameObject.transform;
            var newEulerAngles = new Vector3
            (
                selectedTransform.eulerAngles.x,
                selectedTransform.eulerAngles.y,
                selectedTransform.eulerAngles.z
            );
            var newPosition = new Vector3
            (
                 selectedTransform.position.x,
                 selectedTransform.position.y,
                 selectedTransform.position.z
            );

            if (axis == Axes.X)
            {
                //This calculation will flip the game object appropriately based on its rotation
                newPosition.y = -newPosition.y;
                newEulerAngles.z = -newEulerAngles.z + 180;
            }
            else if (axis == Axes.Y)
            {
                newEulerAngles.z = -newEulerAngles.z;
                newPosition.x = -newPosition.x;
            }

            selectedTransform.eulerAngles = newEulerAngles;
            selectedTransform.position = newPosition;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}