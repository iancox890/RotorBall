using UnityEngine;

namespace PsychedelicGames.RotorBall.Boosts
{

    using PsychedelicGames.RotorBall.Files;

    /// <summary>
    /// Increases ball speed by a given percentage.
    /// </summary>
    [CreateAssetMenu(fileName = "Supercharge.asset", menuName = "Psychedelic Games/Boosts/Supercharge")]
    public class Supercharge : Boost
    {
        public override void Activate(Gameplay.Ball ball)
        {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.TotalBoostsUsed++;
            statisticsFile.SuperchargeBoostsUsed++;
            StatisticsFile.File = statisticsFile;

            ball.Charge = boosts[GetLevel()];
            UpdateStock();
        }
    }
}
