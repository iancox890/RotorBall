using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    using LevelManagement;
    using Files;

    /// <summary>
    /// Multiply enables a given amount of balls when a brick is destroyed.
    /// </summary>
    public class Multiply : Powerup
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private int amount = 2;

        private Ball[] balls;
        private float activationSpeed;

        protected override void Init()
        {
            balls = new Ball[amount];
            activationSpeed = LevelData.Current.Modifiers.BallSpeed;

            for (int i = 0; i < amount; i++)
            {
                Ball ball = (Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject).GetComponentInChildren<Ball>();

                ball.gameObject.Deactivate();
                balls[i] = ball;
            }
        }

        protected override void Activate()
        {
            for (int i = 0; i < amount; i++)
            {
                Ball ball = balls[i];

                ball.transform.position = transform.position;
                ball.gameObject.Activate();
                ball.EnablePlay(new Vector2(activationSpeed, activationSpeed));
            }

            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.BallsSpawned += amount;
            StatisticsFile.File = statisticsFile;
        }
    }
}
