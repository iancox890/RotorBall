using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// ScoreLimit requires the player to complete the level with at least the given score.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(ScoreTarget).asset", menuName = "Psychedelic Games/Objectives/Score Target")]
    public class ScoreTarget : Objective
    {
        [SerializeField] private int score;

        public override string Description { get => description.Value.Replace("<score>", score.FormatValue()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.Score >= score) { return true; }
            return false;
        }
    }
}