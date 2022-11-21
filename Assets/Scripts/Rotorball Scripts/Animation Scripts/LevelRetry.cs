using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Triggers the re-entry animation.
    /// </summary>
    public class LevelRetry : MonoBehaviour
    {
        [SerializeField] private Animator levelController;
        [SerializeField] private Animator failureController;

        private int levelHashTrigger = Animator.StringToHash("Re-entry");
        private int failureHashBool = Animator.StringToHash("Retry");

        private bool returned;

        public void OnReturned()
        {
            returned = true;

            levelController.SetTrigger(levelHashTrigger);
            gameObject.Deactivate();
        }

        private void OnEnable()
        {
            if (returned)
            {
                failureController.SetBool(failureHashBool, true);
            }
        }
    }
}
