using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// True when a certain number of bricks is destroyed by one ball.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BallsInPlay).asset", menuName = "Psychedelic Games/Objectives/Ball In Play")]
    public class BallsInPlay : Objective
    {
        [SerializeField] private int balls;

        public override string Description { get => description.Value.Replace("<balls>", balls.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.MaxBallsInPlay >= balls) { return true; }
            return false;
        }
    }
}
