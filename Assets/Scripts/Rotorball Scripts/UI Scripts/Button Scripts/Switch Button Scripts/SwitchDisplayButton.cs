using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Swaps the active setting of two given screens on click.
    /// </summary>
    public class SwitchDisplayButton : UIButton
    {
        [SerializeField] [UnityEngine.Serialization.FormerlySerializedAs("screensToSwitchOff")] private GameObject[] displaysToSwitchOff;

        [SerializeField] [UnityEngine.Serialization.FormerlySerializedAs("screensToSwitchOn")] private GameObject[] displaysToSwitchOn;

        protected override void OnClicked()
        {
            for (int i = 0; i < displaysToSwitchOff.Length; i++) { displaysToSwitchOff[i].Deactivate(); }
            for (int i = 0; i < displaysToSwitchOn.Length; i++) { displaysToSwitchOn[i].Activate(); }
        }
    }
}
