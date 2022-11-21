using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the current level info to that of a specific level.
    /// </summary>
    public class LevelButton : UIButton
    {
        [SerializeField] private LevelNumber levelNumber;
        [Space]
        [SerializeField] private GameObject numberObj;
        [SerializeField] private GameObject lockObj;

        private LevelData data;

        protected override void Init()
        {
            data = TierData.Current.GetData((int)levelNumber);

            if (data.IsLocked)
            {
                numberObj.Deactivate();
                lockObj.Activate();
                Interactable = false;
            } else {
                numberObj.Activate();
                lockObj.Deactivate();
                Interactable = true;
            }
        }

        protected override void OnClicked() => LevelData.Current = data;
    }
}
