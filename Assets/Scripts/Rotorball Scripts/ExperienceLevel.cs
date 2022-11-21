using UnityEngine;
using PsychedelicGames.RotorBall.Files;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Handles the experience levels the player may reach.
    /// </summary>
    [CreateAssetMenu(fileName = "ExperienceLevel", menuName = "Psychedelic Games/Experience Level")]
    public class ExperienceLevel : ScriptableObject
    {
        //[SerializeField] private int xpRequiredForLevelUp;
        //[SerializeField] [Range(0, 1)] private float xpPercentageIncrease;
        //[SerializeField] private int rpRewardForLevelUp;

        [SerializeField] private int xpRequiredBase;
        [SerializeField] private int xpRequiredPerLevel;

        [SerializeField] private int rpRewardBase;
        [SerializeField] private int rpRewardPerLevel;

        public int GetXpRequired(PlayerFile file) => xpRequiredBase + file.ExperienceLevel * xpRequiredPerLevel;

        public void UpdateLevel(PlayerFile file)
        {
            while (file.ExperiencePoints > GetXpRequired(file))
            {
                file.ExperiencePoints -= GetXpRequired(file);
                file.ExperienceLevel++;
                file.RotorPoints += rpRewardBase + rpRewardPerLevel*file.ExperienceLevel;
            }

            PlayerFile.SaveFile();
        }
    }
}
