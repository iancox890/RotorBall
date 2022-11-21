using UnityEngine;

namespace PsychedelicGames.RotorBall.Boosts
{

    using PsychedelicGames.RotorBall.Files;

    /// <summary>
    /// Makes the ball this boost is applied to last for X% longer.
    /// </summary>
    [CreateAssetMenu(fileName = "Endurance.asset", menuName = "Psychedelic Games/Boosts/Endurance")]
    public class Endurance : Boost
    {
        public override void Activate(Gameplay.Ball ball)
        {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.TotalBoostsUsed++;
            statisticsFile.EnduranceBoostsUsed++;
            StatisticsFile.File = statisticsFile;

            ball.SelfDestructTime += ball.SelfDestructTime * boosts[GetLevel()];
            UpdateStock();
        }
    }
}
