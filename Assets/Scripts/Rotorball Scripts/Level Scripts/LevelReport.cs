namespace PsychedelicGames.RotorBall.LevelManagement
{
    /// <summary>
    /// Represents a report for when a level is finished.
    /// </summary>
    public struct LevelReport
    {
        private float finalTime;
        public float FinalTime { get => finalTime; }

        private int score;
        public int Score { get => score; }

        private int totalScore;
        public int TotalScore { get => totalScore; }

        private int ballsRemaining;
        public int BallsRemaning { get => ballsRemaining; }

        private int maxBrickHits;
        public int MaxBrickHits { get => maxBrickHits; }

        private int maxBrickDestruction;
        public int MaxBrickDestruction { get => maxBrickDestruction; }

        private int maxBonusBrickHits;
        public int MaxBonusBrickHits { get => maxBonusBrickHits; }

        private int allBonusBrickHits;
        public int AllBonusBrickHits { get => allBonusBrickHits; }

        private int allBonusBricksDestroyed;
        public int AllBonusBricksDestroyed { get => allBonusBricksDestroyed; }

        private int maxBonusBrickDestruction;
        public int MaxBonusbrickDestruction { get => maxBonusBrickDestruction; }

        private int maxBallScore;
        public int MaxBallScore { get => maxBallScore; }

        private int maxBallsInPlay;
        public int MaxBallsInPlay { get => maxBallsInPlay; }

        public int allPowerBricksHit { get; }
        public int allHazardBricksHit { get; }
        public int allDurableBricksHit { get; }
        public int allDurableBricksDestroyed { get; }
        public int allShifterBricksDestroyed { get; }
        public int allStandardBricksDestroyed { get; }
        public int allSwapperBricksDestroyed { get; }

        private float maxMultiplier;
        public float MaxMultiplier { get => maxMultiplier; }

        private bool hazardHit;
        public bool HazardHit { get => hazardHit; }

        private bool retried;
        public bool Retried { get => retried; }

        public LevelReport(
            float time,
            float maxMultiplier, 
            int score,
            int totalScore,
            int balls,int maxBallScore, int maxBallsInPlay,
            int maxbrickHits, int maxbrickDestruction,
            int allStandardBricksDestroyed,
            int allDurableBricksHit, int allDurableBricksDestroyed,
            int maxBonusbrickHits, int allBonusbrickHits, int allBonusBricksDestroyed, int maxBonusbrickDestruction,
            int allPowerBricksHit, int allHazardBricksHit,
            int allShifterBricksDestroyed, int allSwapperBricksDestroyed,
            bool hazardHit, bool retried)
        {
            this.finalTime = time;
            this.score = score;
            this.totalScore = totalScore;
            this.ballsRemaining = balls;
            this.maxBrickHits = maxbrickHits;
            this.maxBrickDestruction = maxbrickDestruction;
            this.maxBonusBrickHits = maxBonusbrickHits;
            this.allBonusBrickHits = allBonusbrickHits;
            this.allBonusBricksDestroyed = allBonusBricksDestroyed;
            this.maxBonusBrickDestruction = maxBonusbrickDestruction;
            this.maxBallScore = maxBallScore;
            this.maxBallsInPlay = maxBallsInPlay;

            this.allPowerBricksHit = allPowerBricksHit;
            this.allHazardBricksHit = allHazardBricksHit;
            this.allDurableBricksHit = allDurableBricksHit;
            this.allDurableBricksDestroyed = allDurableBricksDestroyed;
            this.allShifterBricksDestroyed = allShifterBricksDestroyed;
            this.allStandardBricksDestroyed = allStandardBricksDestroyed;
            this.allSwapperBricksDestroyed = allSwapperBricksDestroyed;

            this.maxMultiplier = maxMultiplier;
            this.hazardHit = hazardHit;
            this.retried = retried;
        }

        //public static LevelReport BlankReport = new LevelReport();
    }
}
