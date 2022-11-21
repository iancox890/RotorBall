using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Handles the ball trail.
    /// </summary>
    public class BallTrail : MonoBehaviour
    {
        public event System.Action<float> OnWidthAdjusted;
        public Transform ball { get; set; }

        public void SetState(bool state)
        {
            if (state)
            {
                gameObject.Activate();
            }
            else
            {
                gameObject.Deactivate();
            }
        }

        public void SetWidth(float width) => OnWidthAdjusted?.Invoke(width);
    }
}
