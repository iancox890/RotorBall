using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Gives a given amount of balls to the player when pressed.
    /// </summary>
    public class GiveBallsButton : RewardedAdButton
    {
        /*[SerializeField] */private LevelManager manager;

        override protected void Init() {
            base.Init();
            manager = manager ?? FindObjectOfType<LevelManager>();
        }

        override protected void Reward() {
            base.Reward();
            print(manager);
            manager.Retry();
        }
    }
}
