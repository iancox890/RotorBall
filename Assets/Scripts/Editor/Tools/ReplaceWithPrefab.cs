using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Replaces all selected gameobjects to a given prefab.
    /// </summary>
    public class ReplaceWithPrefab : ScriptableWizard
    {
        [SerializeField] private GameObject prefab;

        [MenuItem("Psychedelic Games/Tools/Replace With Prefab")]
        private static void InitWizard() => DisplayWizard<ReplaceWithPrefab>("Replace With Prefab", "Replace");

        private void OnWizardCreate()
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                GameObject replacementGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                GameObject selectedGameObject = selectedGameObjects[i];

                //Ensure we copy and paste the right transform
                if (selectedGameObject.GetComponent<Transform>() != null && selectedGameObject.GetComponent<RectTransform>() == null)
                    ComponentUtility.CopyAndPasteComponent<Transform>(selectedGameObject, replacementGameObject);
                else
                    ComponentUtility.CopyAndPasteComponent<RectTransform>(selectedGameObject, replacementGameObject);

                DestroyImmediate(selectedGameObject);
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        private void OnWizardUpdate() => helpString = "This wizard will replace selected gameobjects in a scene with a prefab of your choice, while preserving their original position.";
    }
}
