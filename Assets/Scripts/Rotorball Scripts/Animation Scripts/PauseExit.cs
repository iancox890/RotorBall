using UnityEngine;

namespace PsychedelicGames.RotorBall.Animations
{
    /// <summary>
    /// Controls the exit animation for the pause menu.
    /// </summary>
    public class PauseExit : MonoBehaviour
    {
        public void OnExit()
        {
            gameObject.Deactivate();
            TimeUtility.Resume();
            FindObjectOfType<MenuTimer>()?.Unpause();
        }
    }
}
