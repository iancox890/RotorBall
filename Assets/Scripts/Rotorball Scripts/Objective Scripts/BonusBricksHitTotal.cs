using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// Objective which requires a ball hit a certain amount of bricks.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BonusBricksHitTotal).asset", menuName = "Psychedelic Games/Objectives/Bonus Bricks Hit Total")]
    public class BonusBricksHitTotal : Objective
    {
        [SerializeField] private int hits;

        public override string Description { get => description.Value.Replace("<hits>", hits.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.AllBonusBrickHits >= hits) { return true; }

            return false;
        }
    }
}
