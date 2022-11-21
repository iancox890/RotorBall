using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    using LevelManagement;

    /// <summary>
    /// Completes the current level when clicked.
    /// </summary>
    public class CompleteLevel
    {
        [MenuItem("Psychedelic Games/Tools/Complete Level")]
        private static void Complete() => GameObject.FindObjectOfType<LevelManager>().UpdateComplete();
    }
}
