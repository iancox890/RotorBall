using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Manages the background displays.
    /// </summary>
    public class StartMenuBackgroundLevels : MonoBehaviour
    {
        [SerializeField] private bool _immediate = true;
        [SerializeField] private Animator[] backgrounds;
        [SerializeField] private float switchTime;

        private int length;
        private int current;
        private int fadeIn = Animator.StringToHash("Fade In");
        private int fadeOut = Animator.StringToHash("Fade Out");


        private void Awake() {
            length = backgrounds.Length;
            // current = GetRandomIndex();
            current = 0;

            for (int i=0; i<length; i++) {
                // backgrounds[i].SetTrigger(fadeOut);
                if (i == current) {
                    // backgrounds[current].gameObject.Activate();
                    // backgrounds[current].SetTrigger(fadeIn);
                    // backgrounds[i].Play(fadeOut,0,1f);
                } else {
                    backgrounds[i].gameObject.Deactivate();
                    // backgrounds[i].SetTrigger(fadeOut);
                }
            }
        }

        private void Start()
        {
            if (_immediate) {
                RandomLevel();
            }
        }

        public void RandomLevel() {
            for (int i=0; i<length; i++) {
                if (i == current) {
                    backgrounds[current].gameObject.Activate();
                    // backgrounds[current].ResetTrigger(fadeOut);
                    backgrounds[current].SetTrigger(fadeIn);
                    // backgrounds[current].Play(fadeIn,0,0f);
                } else {
                    
                    // backgrounds[current].gameObject.Deactivate();
                    // backgrounds[current].SetTrigger(fadeIn);
                }
            }
        }

        private int GetRandomIndex()
        {
            int index;

            do
            {
                index = Random.Range(0, length);
            }
            while (index == current);

            return index;
        }
    }
}