using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    using Files;

    /// <summary>
    /// Gives the player a given amount of rp.
    /// </summary>
    public class GiveRotorPoints : ScriptableWizard
    {
        [SerializeField] private int rp = 10000;

        [MenuItem("Psychedelic Games/Tools/Give Rotor Points")]
        private static void InitWizard() => DisplayWizard<GiveRotorPoints>("Give Rotor Points", "Give");

        private void OnWizardCreate()
        {
            PlayerFile file = PlayerFile.GetFile();
            file.RotorPoints += rp;
            PlayerFile.SaveFile();
        }
    }
}
