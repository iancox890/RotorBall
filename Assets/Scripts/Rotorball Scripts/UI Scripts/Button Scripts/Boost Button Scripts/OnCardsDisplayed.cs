using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Gameplay;

    /// <summary>
    /// Runs when boost cards are displayed in game.
    /// </summary>
    public class OnCardsDisplayed : MonoBehaviour
    {
        private Slingshot sling;
        private Rotor rotor;

        public void OnShow() { sling.enabled = false; rotor.IsRotatable = false; }
        public void OnHide() { sling.enabled = true;  rotor.IsRotatable = true;  }

        private void Start()
        {
            sling = FindObjectOfType<Slingshot>();
            rotor = FindObjectOfType<Rotor>();
        }
    }
}
