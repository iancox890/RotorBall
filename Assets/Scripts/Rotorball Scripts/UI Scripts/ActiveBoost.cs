using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Gameplay;

    /// <summary>
    /// Handles the icon for the currently active boost.
    /// </summary>
    public class ActiveBoost : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string trigger;

        private Slingshot slingshot;
        private int hash;

        private void Awake()
        {
            slingshot = FindObjectOfType<Slingshot>();
            hash = Animator.StringToHash(trigger);
        }

        private void OnEnable() => slingshot.OnLaunched += Trigger;
        private void OnDisable() => slingshot.OnLaunched -= Trigger;

        private void Trigger() => animator.SetTrigger(hash);
    }
}
