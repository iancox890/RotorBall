using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class BannerAd : MonoBehaviour
{
    [SerializeField] private bool _destroy;
    [SerializeField] private string placementId;

    void Start()
    {
        StartCoroutine(ShowBannerWhenReady());
    }

    private void OnEnable() {
        print("Adding hidebanner callback thing");
        SceneManager.sceneUnloaded += HideBanner;
    }

    private void OnDisable() {
        print("Removing hidebanner callback thing");
        // SceneManager.sceneUnloaded -= HideBanner;
    }

    private void HideBanner(Scene scene) {
        print($"Attempting to hide ad, loaded: {Advertisement.Banner.isLoaded}");
        if (Advertisement.Banner.isLoaded) {
            print("Hiding");
            Advertisement.Banner.Hide(_destroy);
        }
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId) && !Advertisement.Banner.isLoaded)
        {
            print($"Waiting for {placementId}\nready: {Advertisement.IsReady(placementId)}, loaded: {Advertisement.Banner.isLoaded}");
            yield return new WaitForSeconds(0.5f);
        }
        // BannerPosition bannerPos = new BannerPosition();
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementId);
    }
}
