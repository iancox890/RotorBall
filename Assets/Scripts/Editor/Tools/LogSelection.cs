using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Logs the amount of objects currently being selected.
    /// </summary>
    public class LogSelection
    {
        [MenuItem("GameObject/Psychedelic Games/Log Selection", false, 1000)]
        private static void xAxisReflectionMenu() => Debug.Log(Selection.objects.Length + " object(s) selected.");
    }
}
