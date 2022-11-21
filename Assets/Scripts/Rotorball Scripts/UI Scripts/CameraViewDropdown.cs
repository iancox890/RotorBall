using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the camera view setting via dropdown.
    /// </summary>
    public class CameraViewDropdown : MonoBehaviour
    {
        private string viewSetting = "View Setting";

        private void Start()
        {
            Dropdown dropdown = GetComponent<Dropdown>();
            dropdown.value = PlayerPrefs.GetInt(viewSetting, 1);
            dropdown.onValueChanged.AddListener(UpdateViewSetting);
        }

        private void UpdateViewSetting(int index) => PlayerPrefs.SetInt(viewSetting, index);
    }
}