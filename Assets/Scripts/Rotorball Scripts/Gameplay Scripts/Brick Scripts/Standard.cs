using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Represents a standard brick. 
    /// Destroyed when hit.
    /// </summary>
    public class Standard : DestructibleBrick
    {
        protected override void OnBallCollision(Transform other)
        {
            Ball ball = other.GetComponent<Ball>();
            ball.StandardBricksDestroyed++;
            Destroy(ball);
        }
    }
}
