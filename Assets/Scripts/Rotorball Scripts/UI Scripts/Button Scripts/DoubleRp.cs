using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using PsychedelicGames.RotorBall.Files;
    /// <summary>
    /// Doubles the players RP. 
    /// </summary>
    public class DoubleRp : RewardedAdButton
    {
        [SerializeField] private GameObject holder;
        [SerializeField] private Text rpDouble;
        [SerializeField] private Text rpEarned;
        [SerializeField] private Text rpTotal;
        // [SerializeField] private Text

        private LevelManagement.LevelManager manager;

        protected override void Init() => manager = FindObjectOfType<LevelManagement.LevelManager>();

        protected override void Reward()
        {
            PlayerFile file = PlayerFile.GetFile();

            int rp = manager.RotorPointsEarned;
            file.RotorPoints += rp;

            holder.Activate();
            // rpDouble.text = rp.FormatValue();
            rpDouble.GetComponent<CountUp>().Count(0,rp);
            // rpEarned.text = (rp + rp).FormatValue();
            rpEarned.GetComponent<CountUp>().Count(rp,rp*2);
            // rpTotal.text = file.RotorPoints.FormatValue();
            rpTotal.GetComponent<CountUp>().Count(file.RotorPoints-rp,file.RotorPoints);

            Interactable = false;
        }
    }
}
