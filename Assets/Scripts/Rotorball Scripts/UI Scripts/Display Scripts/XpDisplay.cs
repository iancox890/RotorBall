using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Displays the xp info on the level details screen.abstract
    /// </summary>
    public class XpDisplay : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private Text currentXp;
        [Space]
        [SerializeField] private Slider xpSlider;
        [Space]
        [SerializeField] private ExperienceLevel experience;

        private void Start()
        {
            PlayerFile file = PlayerFile.GetFile();

            int xp = file.ExperiencePoints;
            int xpRequired = experience.GetXpRequired(file);

            xpSlider.value = (float)xp / xpRequired;

            levelText.text = file.ExperienceLevel.FormatValue();
            currentXp.text = xp.FormatValue() + " / " + xpRequired.FormatValue();
        }
    }
}
