using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Grants a power boost to the ball.
    /// </summary>
    public class Surge : Powerup
    {
        [SerializeField] private float boost;

        protected override void Activate()
        {
            Ball ball = sourceBrick.SourceBall;
            ball.Velocity += ball.Velocity * boost;
        }
    }
}
