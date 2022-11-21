using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.Files;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays player data in text form.
    /// </summary>
    public class PlayerDataText : MonoBehaviour
    {
        [SerializeField] private InfoCode code;
        //[SerializeField] private ExperienceLevel experience;

        private enum InfoCode { RP, XP, CurrentXP, Level }

        private PlayerFile file;
        
        public event System.Action setText;

        private void Awake()
        {
            Text infoText = GetComponent<Text>();
            file = PlayerFile.GetFile();

            switch (code)
            {
                case (InfoCode.RP):
                    setText = () => infoText.text = file.RotorPoints.FormatValue();
                    break;

                    // case (InfoCode.XP):
                    //     setText = () => infoText.text = file.ExperiencePoints.FormatValue();
                    //     break;

                    // case (InfoCode.CurrentXP):
                    //     setText = () => infoText.text = file.ExperiencePoints.FormatValue() + " / " + experience.XpRequiredForNextLevel.FormatValue();
                    //     break;

                    // case (InfoCode.Level):
                    //     setText = () => infoText.text = file.ExperienceLevel.ToString();
                    //     break;
            }

            setText?.Invoke();
        }
    }
}
