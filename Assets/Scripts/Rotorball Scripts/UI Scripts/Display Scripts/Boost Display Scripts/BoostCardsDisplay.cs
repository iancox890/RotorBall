using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays the boost cards for the player.
    /// </summary>
    public class BoostCardsDisplay : MonoBehaviour
    {
        [SerializeField] private CardButton[] cards;

        private int length;

        private void UpdateDisplay()
        {
            for (int i = 0; i < length; i++)
            {
                CardButton temp = cards[i];
                if (!temp.Boost.IsActivatable())
                {
                    temp.Disable();
                }
            }
        }

        private void Awake()
        {
            length = cards.Length;
            UpdateDisplay();
        }
    }
}
