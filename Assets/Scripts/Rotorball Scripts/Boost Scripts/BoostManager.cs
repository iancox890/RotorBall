using UnityEngine;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall.Boosts
{
    /// <summary>
    /// Manages boosts for the current ball.
    /// </summary>
    public class BoostManager : MonoBehaviour
    {
        private Slingshot slingshot;

        public void Apply(Boost boost)
        {
            if (boost.IsActivatable())
            {
                Ball current = slingshot.CurrentBall;
                current.SetAsBoost();
                boost.Activate(current);
            }
        }

        private void Start() => slingshot = FindObjectOfType<Slingshot>();
    }
}
