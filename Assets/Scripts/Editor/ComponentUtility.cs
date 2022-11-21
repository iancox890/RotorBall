using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Utility class for dealing with components in edit time.
    /// </summary>
    public static class ComponentUtility
    {
        /// <summary>
        /// Copy and paste a component in a given game object.
        /// </summary>
        /// <param name="sourceObj">The object that has the component you want to copy.</param>
        /// <param name="receivingObj">The object that you want to paste the component into.</param>
        /// <typeparam name="T">The type of component to copy/paste.</typeparam>
        public static void CopyAndPasteComponent<T>(GameObject sourceObj, GameObject receivingObj) where T : Component
        {
            var sourceComponent = sourceObj.GetComponent<T>();
            var receivingComponent = receivingObj.GetComponent<T>();

            receivingComponent.transform.SetParent(sourceObj.transform.parent);

            if (UnityEditorInternal.ComponentUtility.CopyComponent(sourceComponent))
                if (UnityEditorInternal.ComponentUtility.PasteComponentValues(receivingComponent))
                    Debug.Log("Component copied and pasted successfully!");
        }

        public static void AddComponent<T>(GameObject sourceObj, GameObject receivingObj) where T : Component
        {
            var srcComponent = sourceObj.GetComponent<T>();
            var receivingComponent = receivingObj.GetComponent<T>();

            if (receivingObj.GetComponent<T>()) { Object.DestroyImmediate(receivingComponent); }

            if (UnityEditorInternal.ComponentUtility.CopyComponent(srcComponent))
            {
                if (UnityEditorInternal.ComponentUtility.PasteComponentAsNew(receivingObj))
                {
                    Debug.Log(srcComponent.ToString() + " added successfully.");
                }
            }
        }
    }
}