using UnityEngine;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall.Animations
{
    /// <summary>
    /// Plays the hollow shape animation when a Brick is destroyed.
    /// </summary>
    public class HollowShapeAnimation : MonoBehaviour
    {
        [SerializeField] private DestructibleBrick brickBody;

        private Animator animator;
        private int trigger = Animator.StringToHash("Destroyed");

        private void Start()
        {
            animator = GetComponent<Animator>();
            brickBody.OnBrickDestroyed += Trigger;
        }

        private void OnDestroy() => brickBody.OnBrickDestroyed -= Trigger;

        private void Trigger()
        {
            animator.SetTrigger(trigger);
            OnDestroy();
        }
    }
}
