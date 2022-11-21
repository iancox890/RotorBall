using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.Audio;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Base class for any UI button.
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] private bool backButtonActivates;
        [SerializeField] private AudioClip clip;

        private AudioSource source;

        protected Button button;
        public Button Button { get { button = button ?? GetComponent<Button>(); return button; } }
        private CanvasGroup _cg;
        public CanvasGroup CG { get { _cg = _cg ?? GetComponent<CanvasGroup>(); return _cg; } }

        public bool Interactable {
            get => Button.interactable;
            set { Button.interactable = value; if (CG != null) { CG.alpha = value?1f:0.5f; } else {throw new System.Exception($"Add a Canvas group to {gameObject.name}");} }
        }

        protected virtual void Init() { }
        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
        protected abstract void OnClicked();

        private void OnClick()
        {
            if (clip)
            {
                source?.PlayOneShot(clip);
            }

            OnClicked();
        }

        private void Awake()
        {
            button = GetComponent<Button>();
            if (AudioManager.Instance)
            {
                source = AudioManager.Instance.AudioUI;
            }
            Init();
        }

        protected void OnEnable()
        {
            button.onClick.AddListener(OnClick);
            OnEnabled();
        }

        protected void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
            OnDisabled();
        }

        virtual protected void Update()
        {
            if (backButtonActivates && enabled)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    foreach (UIButton b in GetComponents<UIButton>())
                    {
                        if (b.enabled && b.backButtonActivates)
                        {
                            b.button.onClick.Invoke();
                            b.OnClick();
                        }
                    }
                }
            }
        }
    }
}
