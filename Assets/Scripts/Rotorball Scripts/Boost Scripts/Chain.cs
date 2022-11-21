using UnityEngine;
using PsychedelicGames.RotorBall.Files;

namespace PsychedelicGames.RotorBall.Boosts
{

    using PsychedelicGames.RotorBall.Files;

    /// <summary>
    /// Damages a given amount of bricks within a radius.
    /// </summary>
    [CreateAssetMenu(fileName = "Chain.asset", menuName = "Psychedelic Games/Boosts/Chain")]
    public class Chain : Boost
    {
        public override void Activate(Gameplay.Ball ball)
        {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.TotalBoostsUsed++;
            statisticsFile.ChainBoostsUsed++;
            StatisticsFile.File = statisticsFile;

            ball.SetChain(boosts[GetLevel()]);
            UpdateStock();
        }
    }
}
