using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{

    using LevelManagement;
    using Files;

    /// <summary>
    /// Deletes all user level data, leaving experience, options, purchases and rotorpoints untouched.
    /// </summary>
    public class ClearLevelDataButton : UIButton
    {
        [SerializeField] private TierData[] data;

        protected override void OnClicked()
        {
            int length = data.Length;
            for (int i = 0; i < length; i++)
            {
                data[i].ResetData();
            }
            //FileUtility.ClearFiles(false);
        }
    }
}
