using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{

    using LevelManagement;
    using Files;

    /// <summary>
    /// Deletes all user level data, leaving experience, options, purchases and rotorpoints untouched.
    /// </summary>
    public class ClearDataButton : UIButton
    {
        protected override void OnClicked()
        {
            FileUtility.ClearFiles(false);
            PlayerPrefs.DeleteAll();
        }
    }
}
