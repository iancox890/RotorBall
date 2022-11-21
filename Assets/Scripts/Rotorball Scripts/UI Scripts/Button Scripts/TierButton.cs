using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the currently active tier when selected.
    /// </summary>
    public class TierButton : UIButton
    {
        [SerializeField] private TierData data;

        protected override void OnClicked() => TierData.Current = data;
    }
}
