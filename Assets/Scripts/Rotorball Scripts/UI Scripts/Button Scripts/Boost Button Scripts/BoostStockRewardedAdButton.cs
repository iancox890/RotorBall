using UnityEngine;

namespace PsychedelicGames.RotorBall.UI {
    using Boosts;
    class BoostStockRewardedAdButton : RewardedAdButton {
        [SerializeField] private Boost boost;

        override protected void Init() => boost.OnMaxed+=()=>Interactable=false;
        override protected void Reward()
        {
            boost.AddPrize();
        }
    }
}