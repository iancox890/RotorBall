using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Manages the background displays.
    /// </summary>
    public class BackgroundDisplayManager : MonoBehaviour
    {
        [SerializeField] private Animator[] backgrounds;
        [SerializeField] private float switchTime;

        private int length;
        private int current;
        private int fadeIn = Animator.StringToHash("Fade In");
        private int fadeOut = Animator.StringToHash("Fade Out");

        private bool deactivated;

        private void Start()
        {
            deactivated = false;
            length = backgrounds.Length;
            current = GetRandomIndex();

            backgrounds[current].gameObject.Activate();
            InvokeRepeating("Switch", switchTime, switchTime);
        }

        private void Switch()
        {
            backgrounds[current].SetTrigger(fadeOut);
            current = GetRandomIndex();

            Animator background = backgrounds[current];

            background.gameObject.Activate();
            background.SetTrigger(fadeIn);
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

        private void OnEnable()
        {
            if (deactivated)
            {
                foreach (Animator bg in backgrounds)
                {
                    if (bg == backgrounds[current])
                    {
                        bg.gameObject.Activate();
                        bg.SetTrigger(fadeIn);
                    }
                    else
                    {
                        bg.gameObject.Deactivate();
                    }
                }
                deactivated = false;
                FindObjectOfType<AppStartup>().LoadScheme();
            }
        }

        private void OnDisable()
        {
            deactivated = true;
            backgrounds[current].gameObject.Deactivate();
        }
    }
}
