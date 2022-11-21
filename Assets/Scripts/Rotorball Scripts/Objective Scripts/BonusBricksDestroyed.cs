using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// Objective which requires a ball hit a certain amount of bricks.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BonusBricksDestroyed).asset", menuName = "Psychedelic Games/Objectives/Bonus Bricks Destroyed")]
    public class BonusBricksDestroyed : Objective
    {
        [SerializeField] private int destroyed;

        public override string Description { get => description.Value.Replace("<destroyed>", destroyed.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.MaxBonusbrickDestruction >= destroyed) { return true; }

            return false;
        }
    }
}
