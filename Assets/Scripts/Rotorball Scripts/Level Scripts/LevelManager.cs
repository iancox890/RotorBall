using UnityEngine;
using System;
using PsychedelicGames.RotorBall.Gameplay;
using PsychedelicGames.RotorBall.Files;

namespace PsychedelicGames.RotorBall.LevelManagement
{
    /// <summary>
    /// Manages a given level.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;
        [SerializeField] private TierData tierData;
        [Space]
        [SerializeField] public GameObject completed;
        [SerializeField] public GameObject failed;
        [Space]
        [SerializeField] private Animator levelAnimator;
        // [Space]
        // [SerializeField] private GameScene _giftScene;
        [Space]
        [SerializeField] private float timeTillMultiplierReset;
        [Space]
        [SerializeField] private ExperienceLevel experience;
        [Space]
        [SerializeField] [Range(0, 1)] private float xpEarnedFromScore;
        [SerializeField] [Range(0, 1)] private float rpEarnedFromScore;
        [SerializeField] private float rpEarnedFromObjectives = 1000;
        [SerializeField] private float firstTimeBonus = 1000;
        [SerializeField] [Range(0, 1)] private float retryPenalty;

        private const int MultiplierMin = 1;

        private GameObject gameplay;
        private Slingshot slingshot;

        public float levelTime;

        private int maxBrickHits;
        private int maxBrickDestruction;
        private int maxBonusBrickHits;
        private int allBonusBrickHits;
        private int allBonusBricksDestroyed;

        private int allBricksHit;
        private int allBricksDestroyed;
        private int allStandardBricksDestroyed;
        private int allDurableBricksHit;
        private int allDurableBricksDestroyed;
        private int allSwapperBricksDestroyed;
        private int allShifterBricksDestroyed;
        private int allHazardBricksHit;
        private int allPowerBricksHit;

        private int maxBonusBrickDestruction;
        private int maxBallScore;
        private int maxBallsInPlay;
        private int triggerHash = Animator.StringToHash("Exit");
        private int ballsInPlay;
        private int finalBalls;
        private int startingBalls;
        private int score;
        private int totalScore;
        private int scoreFromTime;
        private int scoreFromBalls;
        private int rpFromObjectives;
        private int rpFromScore;

        private float maxMultiplier;
        private float multiplier = MultiplierMin;
        private float timer;
        private float finishTime;

        private bool hazardHit;
        private bool isCompleted;
        private bool multiplierActive;
        private bool retried;

        public int ScoreFromTime { get => scoreFromTime; }
        public int ScoreFromBalls { get => scoreFromBalls; }

        public int RpFromObjectives { get => rpFromObjectives; }
        public int RpFromScore { get => rpFromScore; }
        public float FirstTimeBonus { get => firstTimeBonus; }

        public float FinishTime { get => finishTime; }

        public int BallsInPlay
        {
            get => ballsInPlay;
            set
            {
                ballsInPlay = value;
                if (ballsInPlay > maxBallsInPlay)
                {
                    maxBallsInPlay = ballsInPlay;
                }
                if (ballsInPlay <= 0)
                {
                    UpdateFail();
                }
            }
        }

        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnScoreUpdated?.Invoke(score);
            }
        }

        public int TotalScore { get => totalScore; }

        public float Multiplier
        {
            get => multiplier;
            set
            {
                if (multiplier < value)
                {
                    timer = Time.timeSinceLevelLoad + timeTillMultiplierReset;
                }

                multiplier = value;

                if (multiplier < 1)
                {
                    multiplier = 1;
                }

                if (multiplier > maxMultiplier)
                {
                    maxMultiplier = multiplier;
                }

                multiplierActive = true;
                OnMultiplierUpdated?.Invoke(multiplier);
            }
        }

        public int RotorPointsEarned { get; private set; }
        public int ExperiencePointsEarned { get; private set; }

        public event Action<int> OnScoreUpdated;
        public event Action<float> OnMultiplierUpdated;

        public void Retry()
        {
            print("Retrying level");
            enabled = true;
            retried = true;

            slingshot.GiveBalls();

            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.LevelsFailed--;
            
            statisticsFile.TotalBricksHit -= allBricksHit;
            statisticsFile.TotalBricksDestroyed -= allBricksDestroyed;
            statisticsFile.StandardBricksDestroyed -= allStandardBricksDestroyed;
            statisticsFile.DurableBricksHit -= allDurableBricksHit;
            statisticsFile.DurableBricksDestroyed -= allDurableBricksDestroyed;
            statisticsFile.SwapperBricksDestroyed -= allSwapperBricksDestroyed;
            statisticsFile.ShifterBricksDestroyed -= allShifterBricksDestroyed;
            statisticsFile.HazardBricksHit -= allHazardBricksHit;
            statisticsFile.BonusBricksHit -= allBonusBrickHits;
            statisticsFile.BonusBricksDestroyed -= allBonusBricksDestroyed;
            statisticsFile.PowerBricksHit -= allPowerBricksHit;

            statisticsFile.timePlayedGame -= levelTime;
            statisticsFile.timePlayedTotal -= levelTime;

            StatisticsFile.File = statisticsFile;
        }

        public void UpdateReport(Ball ball)
        {
            int bricksHit = ball.BricksHit;
            int bonusBricksHit = ball.BonusBricksHit;
            
            allBricksHit += ball.BricksHit;
            allBricksDestroyed += ball.BricksDestroyed;
            allStandardBricksDestroyed += ball.StandardBricksDestroyed;
            allDurableBricksHit += ball.DurableBricksHit;
            allDurableBricksDestroyed += ball.DurableBricksDestroyed;
            allSwapperBricksDestroyed += ball.SwapperBricksDestroyed;
            allShifterBricksDestroyed += ball.ShifterBricksDestroyed;
            allHazardBricksHit += ball.HazardBricksHit;
            allPowerBricksHit += ball.PowerBricksHit;
            allBonusBrickHits += bonusBricksHit;
            allBonusBricksDestroyed += ball.BonusBricksDestroyed;

            int bricksDestroyed = ball.BricksDestroyed;
            int bonusBricksDestroyed = ball.BonusBricksDestroyed;
            int ballScore = ball.BallScore;

            if (bricksHit > maxBrickHits)
            {
                maxBrickHits = bricksHit;
            }
            if (bricksDestroyed > maxBrickDestruction)
            {
                maxBrickDestruction = bricksDestroyed;
            }
            if (bonusBricksHit > maxBonusBrickHits)
            {
                maxBonusBrickHits = bonusBricksHit;
            }
            if (bonusBricksDestroyed > maxBonusBrickDestruction)
            {
                maxBonusBrickDestruction = bonusBricksDestroyed;
            }
            if (ballScore > maxBallScore)
            {
                maxBallScore = ballScore;
            }

            //print("bricksDestroyed:" + bricksDestroyed);
            //print("maxBricksDestroyed:" + maxBrickDestruction);
            //print("standardBricksDestroyed:" + allStandardBricksDestroyed);
        }

        public void UpdateHazardStatus() => hazardHit = true;

        public void UpdateComplete()
        {
            //Apply score bonuses
            finishTime = FindObjectOfType<UI.TimerDisplay>().Gametime;
            totalScore = score;

            if (!levelData.File.IsCompleted) { RotorPointsEarned += Mathf.RoundToInt(firstTimeBonus); }
            else { firstTimeBonus = 0; }

            if (!retried)
            {
                float target = levelData.TargetTime;
                finalBalls = slingshot.Balls;

                scoreFromBalls = finalBalls * 10000;
                totalScore += scoreFromBalls;

                if (finishTime < target)
                {
                    scoreFromTime = Mathf.RoundToInt((target - finishTime) / 0.001f);
                    totalScore += scoreFromTime;
                }
            }
            //Get the level report, update level data, and set the tier as dirty.
            LevelReport report = GetReport();

            int completed = levelData.UpdateData(report);
            tierData.SetDirty();

            //Get our file data
            PlayerFile playerFile = PlayerFile.GetFile();
            StatisticsFile statisticsFile = FileUtility.GetFile<StatisticsFile>(StatisticsFile.FileName);

            //statisticsFile.timePlayedGame += finishTime;
            //statisticsFile.timePlayedTotal += finishTime;

            //Add to our rotorpoints based off the score and the completed objectives

            if (retried)
            {
                rpFromObjectives = Mathf.RoundToInt((completed * rpEarnedFromObjectives) * retryPenalty);
                rpFromScore = Mathf.RoundToInt((totalScore * rpEarnedFromScore) * retryPenalty);
            }
            else
            {
                rpFromObjectives = Mathf.RoundToInt(completed * rpEarnedFromObjectives);
                rpFromScore = Mathf.RoundToInt(totalScore * rpEarnedFromScore);
            }

            RotorPointsEarned += rpFromScore + rpFromObjectives;

            float xp = totalScore * xpEarnedFromScore;
            ExperiencePointsEarned += Mathf.RoundToInt(xp);

            playerFile.RotorPoints += RotorPointsEarned;
            playerFile.ExperiencePoints += ExperiencePointsEarned;
            statisticsFile.RPEarned += RotorPointsEarned;

            statisticsFile.LevelsCompleted++;
            statisticsFile.TotalScore += totalScore;

            FileUtility.OverwriteFile(playerFile, PlayerFile.FileName);
            StatisticsFile.File = statisticsFile;

            experience.UpdateLevel(playerFile);

            levelAnimator.SetTrigger(triggerHash);

            isCompleted = true;
            enabled = false;
        }

        private void Awake()
        {
            LevelData.Current = levelData;
            TierData.Current = tierData;

            slingshot = FindObjectOfType<Slingshot>();
            gameplay = GameObject.FindGameObjectWithTag("Gameplay UI");
            startingBalls = levelData.Modifiers.BallCount;

            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.LevelsPlayed++;
            StatisticsFile.File = statisticsFile;
        }

        private void Update()
        {
            if (multiplierActive)
            {
                if (Time.timeSinceLevelLoad > timer)
                {
                    ResetMultiplier();
                }
            }
        }

        private void OnEnable() => DestructibleBrick.OnAllBricksDestroyed += UpdateComplete;

        private void OnDisable()
        {
            DestructibleBrick.OnAllBricksDestroyed -= UpdateComplete;
            gameplay.Deactivate();

            StatisticsFile statisticsFile = FileUtility.GetFile<StatisticsFile>(StatisticsFile.FileName);
            
            statisticsFile.TotalBricksHit += allBricksHit;
            statisticsFile.TotalBricksDestroyed += allBricksDestroyed;
            statisticsFile.StandardBricksDestroyed += allStandardBricksDestroyed;
            statisticsFile.DurableBricksHit += allDurableBricksHit;
            statisticsFile.DurableBricksDestroyed += allDurableBricksDestroyed;
            statisticsFile.SwapperBricksDestroyed += allSwapperBricksDestroyed;
            statisticsFile.ShifterBricksDestroyed += allShifterBricksDestroyed;
            statisticsFile.HazardBricksHit += allHazardBricksHit;
            statisticsFile.BonusBricksHit += allBonusBrickHits;
            statisticsFile.BonusBricksDestroyed += allBonusBricksDestroyed;
            statisticsFile.PowerBricksHit += allPowerBricksHit;

            statisticsFile.BallsLost += startingBalls - finalBalls;
            statisticsFile.BallsLeftover += finalBalls;

            //finishTime = FindObjectOfType<UI.TimerDisplay>().Gametime;
            statisticsFile.timePlayedGame += levelTime;
            statisticsFile.timePlayedTotal += levelTime;

            StatisticsFile.File = statisticsFile;
        }

        private void OnExit()
        {
            if (isCompleted)
            {
                completed.Activate();
            }
            else
            {
                failed.Activate();
            }
        }

        private void UpdateFail()
        {
            if (!slingshot.HasBalls && !isCompleted)
            {
                StatisticsFile statisticsFile = StatisticsFile.File;
                statisticsFile.LevelsFailed++;
                StatisticsFile.File = statisticsFile;
                
                levelData.FailedAttempts++;

                levelAnimator.SetTrigger(triggerHash);

                isCompleted = false;
                enabled = false;
            }
        }

        private LevelReport GetReport() => new LevelReport(
            finishTime, maxMultiplier, score, totalScore,
            finalBalls, maxBallScore, maxBallsInPlay,
            maxBrickHits, maxBrickDestruction,
            allStandardBricksDestroyed,
            allDurableBricksHit,allDurableBricksDestroyed,
            maxBonusBrickHits, allBonusBrickHits, allBonusBricksDestroyed, maxBonusBrickDestruction,
            allPowerBricksHit,allHazardBricksHit,
            allShifterBricksDestroyed,allSwapperBricksDestroyed,
            hazardHit, retried
        );

        private void ResetMultiplier() => Multiplier = MultiplierMin;

        public LevelData GetLevel() { return levelData; }
    }
}
