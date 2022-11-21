using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Switches a display via animation.
    /// </summary>
    public class SwitchAnimationDisplayButton : UIButton
    {
        [SerializeField] private Animator currentAniamtor;
        [SerializeField] private Animator newAnimator;
        [SerializeField] private string currentTrigger;
        [SerializeField] private string newTrigger;

        private int currentHash;
        private int newHash;

        protected override void Init()
        {
            currentHash = Animator.StringToHash(currentTrigger);
            newHash = Animator.StringToHash(newTrigger);
        }

        protected override void OnClicked()
        {
            currentAniamtor.SetTrigger(currentHash);

            newAnimator.gameObject.Activate();
            newAnimator.SetTrigger(newHash);

            //FindObjectOfType<AppStartup>().LoadScheme();
        }
    }
}
