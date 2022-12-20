using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstialAds : MonoBehaviour
{
    private InterstitialAd interstitial;
    public string android_ID;
    public string iOS_ID;

    //public GameObject music;

    private void Start()
    {
#if UNITY_ANDROID
        string adUnitId = android_ID;
#elif UNITY_IPHONE
        string adUnitId = iOS_ID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        interstitial.Destroy();
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message");
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        AudioListener.volume = 1;
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            AudioListener.volume = 0;
            //music.SetActive(false);
            this.interstitial.Show();
        }
    }

    //IEnumerator WaitingToShowAd()
    //{
    //    if (this.interstitial.IsLoaded())
    //    {
    //        yield return new WaitForSeconds(2);
    //        AudioListener.volume = 0;
    //        this.interstitial.Show();
    //    }
    //}
}
