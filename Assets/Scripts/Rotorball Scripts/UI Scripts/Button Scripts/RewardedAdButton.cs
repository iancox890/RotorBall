using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace PsychedelicGames.RotorBall.UI
{
    public class RewardedAdButton : UIButton, IUnityAdsListener
    {
        [SerializeField] private string placementId;
        [SerializeField] private UnityEvent onReward;

        protected virtual void Reward()
        {
            print("Rewarded");
            onReward?.Invoke();
        }

        override protected void OnEnabled()
        {
            Advertisement.AddListener(this);
            Interactable = Advertisement.IsReady(placementId);
        }

        override protected void OnDisabled()
        {
            // Debug.Log($"Destroyed Ad Button {gameObject.name}");

            Advertisement.RemoveListener(this);
        }

        override protected void OnClicked()
        {
            ShowAd();
        }

        void ShowAd()
        {
            // print($"Showing Ad: {placementId}");
            Advertisement.Show(placementId);
        }

        public void OnUnityAdsReady(string placementId)
        {
            print($"Ad {placementId} ready");
            if (this.placementId.Equals(placementId))
            {
                // print($"Ad {placementId} ready");
                Interactable = true;
            }
        }

        public void OnUnityAdsDidError(string message)
        {
            if (this.placementId.Equals(placementId))
            {
                print($"error with ad {placementId}");
                // TODO Change this!! !important
                throw new UnityException("Ad error "+message);
            }
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            if (this.placementId.Equals(placementId))
            {
                // print($"ad {placementId} started");
                // throw new System.NotImplementedException();
            }
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // print($"Finished ad {placementId} on {gameObject.name}");
            if (placementId.Equals(this.placementId))
            {
                if (showResult == ShowResult.Finished)
                {
                    print($"Watched ad {placementId} to the end, rewarding {gameObject.name}");
                    Reward();
                }
                else if (showResult == ShowResult.Skipped)
                {
                    // Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
                }
                else if (showResult == ShowResult.Failed)
                {
                    Debug.LogError("Video failed to show");
                }
            }
        }
    }
}
