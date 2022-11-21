using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    
    using Files;

    /// <summary>
    /// Loads a given menu when clicked.
    /// </summary>
    public class SwitchSceneButton : UIButton
    {
        [SerializeField] [UnityEngine.Serialization.FormerlySerializedAs("menu")] private GameScene scene;

        private GameObject background;
        private Animator animator;
        private LoadOnFadeOut loader;

        private int trigger = Animator.StringToHash("FadeOut");

        protected override void Init()
        {
            background = GameObject.FindGameObjectWithTag("Fade Background");
            animator = background.GetComponent<Animator>();
            loader = background.GetComponent<LoadOnFadeOut>();
        }

        protected override void OnClicked()
        {
            loader.Scene = scene;
            animator.SetTrigger(trigger);
            Interactable = false;

            //FindObjectOfType<AppStartup>().LoadScheme();
        }
    }
}
