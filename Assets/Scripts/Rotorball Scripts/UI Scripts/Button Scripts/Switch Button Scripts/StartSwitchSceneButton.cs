using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    
    using Files;

    /// <summary>
    /// Loads a given menu when clicked.
    /// </summary>
    public class StartSwitchSceneButton : UIButton
    {
        [SerializeField] private GameScene _menuScene;
        [SerializeField] private GameScene _welcomeScene;

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
            PlayerFile plr = PlayerFile.GetFile();
            loader.Scene = plr.EULASigned ? _menuScene : _welcomeScene;
            animator.SetTrigger(trigger);
            Interactable = false;

            //FindObjectOfType<AppStartup>().LoadScheme();
        }
    }
}
