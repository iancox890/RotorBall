using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the camera view setting via dropdown.
    /// </summary>
    public class CameraViewButton : UIButton
    {
        [SerializeField] private ViewOption viewOption;
        [Space]
        [SerializeField] private GameObject buttonIcon;
        [SerializeField] private GameObject tickIcon;

        public static event System.Action OnViewChanged;
        public enum ViewOption { Close, Medium, Far }

        public const string ViewSetting = "View Setting";

        public void Enable()
        {
            buttonIcon.Deactivate();
            tickIcon.Activate();
        }

        public void Disable()
        {
            tickIcon.Deactivate();
            buttonIcon.Activate();
        }

        protected override void OnClicked()
        {
            PlayerPrefs.SetInt(ViewSetting, (int)viewOption);
            OnViewChanged?.Invoke();
        }
    }
}