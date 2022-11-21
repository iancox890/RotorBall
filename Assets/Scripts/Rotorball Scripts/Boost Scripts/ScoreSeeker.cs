using UnityEngine;

namespace PsychedelicGames.RotorBall.Boosts
{

    using PsychedelicGames.RotorBall.Files;

    /// <summary>
    /// Boosts the score for a given ball by X%.
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreSeeker.asset", menuName = "Psychedelic Games/Boosts/Score Seeker")]
    public class ScoreSeeker : Boost
    {
        public override void Activate(Gameplay.Ball ball)
        {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.TotalBoostsUsed++;
            statisticsFile.ScoreBoostsUsed++;
            StatisticsFile.File = statisticsFile;
            
            ball.IsScoreSeeker = true;
            ball.ScoreBonus = boosts[GetLevel()];

            UpdateStock();
        }
    }
}
