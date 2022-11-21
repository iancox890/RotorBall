using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Increases the balls scale by a given amount.
    /// </summary>
    public class Amplify : Powerup
    {
        [SerializeField] private float scaleMultiplier;

        protected override void Activate() => sourceBrick.SourceBall.SetScale(scaleMultiplier, false);
    }
}
