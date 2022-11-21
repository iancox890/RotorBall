using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Adds an extra ball to the slingshot.
    /// </summary>
    public  class ExtraBall : Powerup
    {
        private Slingshot slingshot;

        protected override void Init() => slingshot = FindObjectOfType<Slingshot>();
        protected override void Activate() { } //slingshot.SlingshotBalls++;
    }
}
