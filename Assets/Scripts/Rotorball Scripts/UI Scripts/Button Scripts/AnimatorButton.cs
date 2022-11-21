using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Triggers an animation when pressed.
    /// </summary>
    public class AnimatorButton : UIButton
    {
        [SerializeField] private string parameterName;
        [SerializeField] private bool parameterBool;
        [SerializeField] private Parameter parameterType;
        [SerializeField] private Animator animator;

        public enum Parameter { Trigger, Bool }

        private int hash;

        protected override void Init() => hash = Animator.StringToHash(parameterName);
        protected override void OnClicked()
        {
            switch (parameterType)
            {
                case Parameter.Trigger:
                    animator.SetTrigger(hash);
                    break;
                case Parameter.Bool:
                    animator.SetBool(hash, parameterBool);
                    break;
            }
        }
    }
}
