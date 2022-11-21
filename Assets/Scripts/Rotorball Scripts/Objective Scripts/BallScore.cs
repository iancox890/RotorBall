using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// BallScore requires the player to complete the level with at least a ball score high of x.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(BallScore).asset", menuName = "Psychedelic Games/Objectives/Ball Score")]
    public class BallScore : Objective
    {
        [SerializeField] private int score;

        public override string Description { get => description.Value.Replace("<score>", score.ToString()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.MaxBallScore >= score) { return true; }
            return false;
        }
    }
}
