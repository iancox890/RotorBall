using UnityEngine;

namespace PsychedelicGames.RotorBall.Boosts
{

    using PsychedelicGames.RotorBall.Files;

    /// <summary>
    /// Adjusts the ball size by a given percentage.
    /// </summary>
    [CreateAssetMenu(fileName = "Size.asset", menuName = "Psychedelic Games/Boosts/Size")]
    public class Size : Boost
    {
        [SerializeField] private Option option;

        private enum Option { Shrink, Enlarge }

        public override void Activate(Gameplay.Ball ball)
        {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.TotalBoostsUsed++;
            switch (option)
            {
                case Option.Shrink:
                    ball.SetScale(1 - boosts[GetLevel()], true);
                    statisticsFile.ShrinkBoostsUsed++;
                    break;
                case Option.Enlarge:
                    ball.SetScale(1 + boosts[GetLevel()], true);
                    statisticsFile.EnlargeBoostsUsed++;
                    break;
            }
            StatisticsFile.File = statisticsFile;

            UpdateStock();
        }
    }
}
