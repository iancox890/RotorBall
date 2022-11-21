using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// Multiplier target requires the player to complete the level with a multipleir of at least x.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(MultiplierTarget).asset", menuName = "Psychedelic Games/Objectives/Multiplier Target")]
    public class MultiplierTarget : Objective
    {
        [SerializeField] private float multiplier;

        public override string Description { get => description.Value.Replace("<multiplier>", multiplier.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.MaxMultiplier >= multiplier) { return true; }
            return false;
        }
    }
}