using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Sets the current tier and level to its arguments then loads the specified scene. Also signs the EULA because we're near the end of development so fuck it xd
    /// </summary>
    public class GotoLevel : MonoBehaviour
    {
        [SerializeField] private TierData _tier;
        [SerializeField] private LevelData _level;

        [SerializeField] private GameScene _scene;
        [SerializeField] private bool _signEULA = true;

        private GameObject background;
        private Animator animator;
        private LoadOnFadeOut loader;

        private int trigger = Animator.StringToHash("FadeOut");

        protected void Start()
        {
            background = GameObject.FindGameObjectWithTag("Fade Background");
            animator = background.GetComponent<Animator>();
            loader = background.GetComponent<LoadOnFadeOut>();
        }

        public void DoIt()
        {
            if (_signEULA)
            {
                PlayerFile f = PlayerFile.GetFile();
                f.EULASigned = true;
                PlayerFile.file = f;
                PlayerFile.SaveFile();
            }

            TierData.Current = _tier;
            LevelData.Current = _level;
            
            loader.Scene = _scene;
            animator.SetTrigger(trigger);
        }
    }
}
