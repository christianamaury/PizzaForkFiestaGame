using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Importing Google Ads SDK;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdsManager : MonoBehaviour
{

    public static AdsManager Instance { get; set; }
    //Test Ads Banner: ca-app-pub-3940256099942544/2934735716
    //Test Ads Interstitial: ca-app-pub-3940256099942544/4411468910

    private string appId;
    private string bannerId = "ca-app-pub-3187572158588519/8522389862";
    private string interstitialId = "ca-app-pub-3187572158588519/5896226520";

    private BannerView bannerView;
    private InterstitialAd interstitialAds;

    private void Awake()
    {
        Instance = this;
  
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initializing the Google Ads SDK;
        MobileAds.Initialize(initStatus => { });

        //Checking if bought the game to remove the Ads below here:
        //Getting Reference from the PlayerPreSaved Data to see if the user has bought the game before;
        int noAdsReference = PlayerPrefs.GetInt("NoAds", 0);

        //If is equal to false, show ads to the user;
        if (IAPurchase.Instance.removeAllAds_IAP == false || noAdsReference == 0)
        {
            //Requesting Banners and Video Ads;
            this.requestBanner();
            this.requestVideoAds();
        }

    }

    public void requestBanner()
    {
        //Ads position bottom of the screen;
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);

        //Creating an empty Ads Request;
        AdRequest request = new AdRequest.Builder().Build();

        //Loading Banner with Request;
        bannerView.LoadAd(request);
    }

    public void requestVideoAds()
    {
        this.interstitialAds = new InterstitialAd(interstitialId);

        //Creating an empty Interstitial Ads Request;
        AdRequest request = new AdRequest.Builder().Build();

        //Loading Interstital Ads;
        this.interstitialAds.LoadAd(request);
    }

    public void showingInterstitialAds()
    {
        //If the interstitial is ready; show it..
        if (this.interstitialAds.IsLoaded())
        {
            //Display the Ads;
            this.interstitialAds.Show();
        }

        else
        {
            Debug.Log("Ads aren't ready yet");
            this.interstitialAds.OnAdOpening += HandleOnAdOpened;
            this.interstitialAds.OnAdClosed += HandleOnAdClosed;

        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {

    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {

    }
}
