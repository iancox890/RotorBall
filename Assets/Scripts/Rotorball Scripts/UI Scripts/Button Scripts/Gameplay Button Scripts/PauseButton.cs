using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Pauses the game on click.
    /// </summary>
    public class PauseButton : UIButton
    {
        [SerializeField] private GameObject pauseScreen;

        protected override void OnClicked()
        {
            TimeUtility.Pause();
            pauseScreen.Activate();
            FindObjectOfType<MenuTimer>()?.Pause();
        }
    }
}
