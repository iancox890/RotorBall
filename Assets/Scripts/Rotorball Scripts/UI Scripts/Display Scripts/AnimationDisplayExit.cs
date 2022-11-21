using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Deactivates the display when it is exited.
    /// </summary>
    public class AnimationDisplayExit : MonoBehaviour
    {
        public void OnExit() => gameObject.Deactivate();
    }
}
