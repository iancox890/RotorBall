using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// True if the player has hit a hazard brick.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(HazardHit).asset", menuName = "Psychedelic Games/Objectives/Hazard Hit")]
    public class HazardHit : Objective
    {
        public override string Description { get => description.Value; }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.HazardHit) { return true; }
            return false;
        }
    }
}
