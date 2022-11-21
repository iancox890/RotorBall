using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// Objective which requires a ball hit a certain amount of bricks.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BonusBricksHitTotal).asset", menuName = "Psychedelic Games/Objectives/Bonus Bricks Destroyed Total")]
    public class BonusBricksDestroyedTotal : Objective
    {
        [SerializeField] private int destroyed;

        public override string Description { get => description.Value.Replace("<destroyed>", destroyed.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.AllBonusBricksDestroyed >= destroyed) { return true; }

            return false;
        }
    }
}
