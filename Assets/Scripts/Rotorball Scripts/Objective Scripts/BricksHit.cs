using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// Objective which requires a ball hit a certain amount of bricks.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BricksHit).asset", menuName = "Psychedelic Games/Objectives/Bricks Hit")]
    public class BricksHit : Objective
    {
        [SerializeField] private int hits;

        public override string Description { get => description.Value.Replace("<hits>", hits.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.MaxBrickHits >= hits) { return true; }

            return false;
        }
    }
}
