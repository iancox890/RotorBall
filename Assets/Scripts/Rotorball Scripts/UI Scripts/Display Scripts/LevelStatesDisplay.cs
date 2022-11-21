using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using LevelManagement;
    /// <summary>
    /// Updates the level states for the select screen.
    /// </summary>
    public class LevelStatesDisplay : MonoBehaviour
    {
        private void Awake() => TierData.Current.UpdateLevelStates();
    }
}
