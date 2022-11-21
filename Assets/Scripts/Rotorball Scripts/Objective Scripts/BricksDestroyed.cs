using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// True when a certain number of bricks is destroyed by one ball.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BricksDestroyed).asset", menuName = "Psychedelic Games/Objectives/Bricks Destroyed")]
    public class BricksDestroyed : Objective
    {
        [SerializeField] private int destroyed;

        public override string Description { get => description.Value.Replace("<destroyed>", destroyed.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.MaxBrickDestruction >= destroyed) { return true; }
            return false;
        }
    }
}
