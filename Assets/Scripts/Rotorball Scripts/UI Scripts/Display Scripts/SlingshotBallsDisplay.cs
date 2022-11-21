using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays the amount of balls the slingshot has.
    /// </summary>
    public class SlingshotBallsDisplay : MonoBehaviour
    {
        [SerializeField] private Text slingshotBallsText;

        private Slingshot slingshot;

        private void Awake()
        {
            slingshot = FindObjectOfType<Slingshot>();
            slingshot.OnBallsUpdated += SetText;

            slingshotBallsText.text = slingshot.Balls + "x";
        }

        private void SetText(int balls) => slingshotBallsText.text = balls + "x";
    }
}
