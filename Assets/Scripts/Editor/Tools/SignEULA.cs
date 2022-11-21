using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    using Files;
    using UI;

    /// <summary>
    /// Signs the EULA to allow the welcome screen to be skipped
    /// </summary>
    public class SignEULA
    {
        [MenuItem("Psychedelic Games/Tools/Sign EULA")]
        private static void Sign() {
            PlayerFile f = PlayerFile.GetFile();
            f.EULASigned = true;
            PlayerFile.file = f;
            PlayerFile.SaveFile();
            GameObject.FindObjectOfType<GotoLevel>()?.DoIt();
        }
    }
}
