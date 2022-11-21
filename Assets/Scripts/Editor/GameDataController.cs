using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using PsychedelicGames.RotorBall.Files;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Controls elements of game data/file management from the editor.
    /// </summary>
    public static class GameDataController
    {
        private const string menuName = "Psychedelic Games/Tools/Data";

        [MenuItem(menuName + "/Clear")]
        private static void ClearData()
        {
            FileUtility.ClearFiles(false);
            PlayerPrefs.DeleteAll();
        }

        [MenuItem(menuName + "/Delete")]
        private static void DeleteData()
        {
            FileUtility.ClearFiles(true);
            PlayerPrefs.DeleteAll();
        }
    }
}
