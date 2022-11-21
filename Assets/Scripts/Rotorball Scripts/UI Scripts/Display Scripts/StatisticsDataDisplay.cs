using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Displays statistical data.
    /// </summary>
    public class StatisticsDataDisplay : MonoBehaviour
    {
        [SerializeField] private Text timePlayedTotal;
        [SerializeField] private Text timePlayedMenu;
        [SerializeField] private Text timePlayedGame;
        [Space]
        [SerializeField] private Text levelsCompleted;
        [SerializeField] private Text levelsFailed;
        [SerializeField] private Text levelsRestarted;
        [SerializeField] private Text levelsPlayed;
        [Space]
        [SerializeField] private Text totalScore;
        [Space]
        [SerializeField] private Text ballsLaunched;
        [SerializeField] private Text ballsSpawned;
        [SerializeField] private Text ballsLost;
        [SerializeField] private Text ballsLeftover;
        [Space]
        [SerializeField] private Text totalBricksHit;
        [SerializeField] private Text totalBricksDestroyed;
        [SerializeField] private Text standardBricksDestroyed;
        [SerializeField] private Text durableBricksHit;
        [SerializeField] private Text durableBricksDestroyed;
        [SerializeField] private Text swapperBricksDestroyed;
        [SerializeField] private Text shifterBricksDestroyed;
        [SerializeField] private Text hazardBricksHit;
        [SerializeField] private Text bonusBricksHit;
        [SerializeField] private Text bonusBricksDestroyed;
        [SerializeField] private Text powerBricksHit;
        [Space]
        [SerializeField] private Text totalGiftsReceived;
        [Space]
        [SerializeField] private Text totalBoostsUsed;
        [SerializeField] private Text chainBoostsUsed;
        [SerializeField] private Text enduranceBoostsUsed;
        [SerializeField] private Text enlargeBoostsUsed;
        [SerializeField] private Text scoreBoostsUsed;
        [SerializeField] private Text shrinkBoostsUsed;
        [SerializeField] private Text superchargeBoostsUsed;
        [Space]
        [SerializeField] private Text RPEarned;
        [SerializeField] private Text RPSpent;

        private void Start()
        {
            StatisticsFile data = StatisticsFile.File;

            timePlayedTotal.text = data.timePlayedTotal.ToFormattedTime();
            timePlayedMenu.text = data.timePlayedMenu.ToFormattedTime();
            timePlayedGame.text = data.timePlayedGame.ToFormattedTime();

            levelsCompleted.text = data.LevelsCompleted.FormatValue();
            levelsFailed.text = data.LevelsFailed.FormatValue();
            levelsRestarted.text = data.LevelsRestarted.FormatValue();
            levelsPlayed.text = data.LevelsPlayed.FormatValue();

            totalScore.text = data.TotalScore.FormatValue();

            ballsLaunched.text = data.BallsLaunched.FormatValue();
            ballsSpawned.text = data.BallsSpawned.FormatValue();
            ballsLost.text = data.BallsLost.FormatValue();
            ballsLeftover.text = data.BallsLeftover.FormatValue();

            totalBricksHit.text             = data.TotalBricksHit.FormatValue();
            totalBricksDestroyed.text       = data.TotalBricksDestroyed.FormatValue();
            standardBricksDestroyed.text    = data.StandardBricksDestroyed.FormatValue();
            durableBricksHit.text           = data.DurableBricksHit.FormatValue();
            durableBricksDestroyed.text     = data.DurableBricksDestroyed.FormatValue();
            swapperBricksDestroyed.text     = data.SwapperBricksDestroyed.FormatValue();
            shifterBricksDestroyed.text     = data.ShifterBricksDestroyed.FormatValue();
            hazardBricksHit.text            = data.HazardBricksHit.FormatValue();
            bonusBricksHit.text             = data.BonusBricksHit.FormatValue();
            bonusBricksDestroyed.text       = data.BonusBricksDestroyed.FormatValue();
            powerBricksHit.text             = data.PowerBricksHit.FormatValue();

            if (totalGiftsReceived != null)
            {
                totalGiftsReceived.text = data.giftsOpened.ToString();
            }

            totalBoostsUsed.text        = data.TotalBoostsUsed.FormatValue();
            chainBoostsUsed.text        = data.ChainBoostsUsed.FormatValue();
            enduranceBoostsUsed.text    = data.EnduranceBoostsUsed.FormatValue();
            enlargeBoostsUsed.text      = data.EnlargeBoostsUsed.FormatValue();
            scoreBoostsUsed.text        = data.ScoreBoostsUsed.FormatValue();
            shrinkBoostsUsed.text       = data.ShrinkBoostsUsed.FormatValue();
            superchargeBoostsUsed.text  = data.SuperchargeBoostsUsed.FormatValue();

            RPEarned.text = data.RPEarned.FormatValue();
            RPSpent.text = data.RPSpent.FormatValue();
        }

        public void UpdateTimers(float time)
        {
            StatisticsFile data = FileUtility.GetFile<StatisticsFile>(StatisticsFile.FileName);
            timePlayedTotal.text = (data.timePlayedTotal+time).ToFormattedTime();
            timePlayedMenu.text = (data.timePlayedMenu+time).ToFormattedTime();
        }
    }
}
