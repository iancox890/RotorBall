using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// BallLimit requires the player to complete the level with at least the given amount of balls left.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BallLimit).asset", menuName = "Psychedelic Games/Objectives/Ball Limit")]
    public class BallLimit : Objective
    {
        [SerializeField] private int balls;

        public override string Description { get => description.Value.Replace("<balls>", balls.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.BallsRemaning >= balls) { return true; }
            return false;
        }
    }
}
