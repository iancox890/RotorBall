using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the display for the camera view options.
    /// </summary>
    public class CameraViewDisplay : MonoBehaviour
    {
        [SerializeField] private CameraViewButton[] buttons;

        private const int Count = 3;

        private void Awake() => UpdateDisplay();

        private void OnEnable() => CameraViewButton.OnViewChanged += UpdateDisplay;
        private void OnDisable() => CameraViewButton.OnViewChanged -= UpdateDisplay;

        private void UpdateDisplay()
        {
            int active = PlayerPrefs.GetInt(CameraViewButton.ViewSetting);

            for (int i = 0; i < Count; i++)
            {
                if (i == active)
                {
                    buttons[i].Enable();
                }
                else
                {
                    buttons[i].Disable();
                }
            }
        }
    }
}
