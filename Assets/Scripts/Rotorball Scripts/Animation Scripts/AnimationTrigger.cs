using UnityEngine;

namespace PsychedelicGames.RotorBall.Animations
{
    /// <summary>
    /// Triggers an animation when enabled.
    /// </summary>
    public class AnimationTrigger : MonoBehaviour
    {
        [SerializeField] private string trigger;
        [SerializeField] private Animator animator;

        private int triggerHash;

        private void Awake() => triggerHash = Animator.StringToHash(trigger);
        private void OnEnable() => animator.SetTrigger(triggerHash);
    }
}
