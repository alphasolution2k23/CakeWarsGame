using System.Collections;
using UnityEngine;
using System;
using GoogleMobileAds.Api.Mediation;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    #region Singletons
    public static AdManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Android Settings")]
    [SerializeField] string bannerAdUnitAnd;
    [SerializeField] string interstitialAdUnitAnd;
    [SerializeField] string rewardAdUnitAnd;

    [Header("IOS Settings")]
    [SerializeField] string bannerAdUnitIOS;
    [SerializeField] string interstitialAdUnitIOS;
    [SerializeField] string rewardAdUnitIOS;
    [SerializeField] bool testAds;

    string bannerAdUnit;
    string interstitialAdUnit;
    string rewardAdUnit;

    private RewardedAd rewardedAd;

    private void Start()
    {
        InitIds();
        InitAPI();
    }

    #region Initialization

    private void InitIds()
    {
#if UNITY_ANDROID
        bannerAdUnit = bannerAdUnitAnd;
        interstitialAdUnit = interstitialAdUnitAnd;
        rewardAdUnit = rewardAdUnitAnd;

        rewardAdUnit = testAds ? "ca-app-pub-3940256099942544/5224354917" : rewardAdUnit;
#elif UNITY_IOS
        //appId = appIdIOS;
        bannerAdUnit = bannerAdUnitIOS;
        interstitialAdUnit = interstitialAdUnitIOS;
        rewardAdUnit = rewardAdUnitIOS;
        rewardAdUnit = testAds ? "ca-app-pub-3940256099942544/1712485313" : rewardAdUnit;
#endif
    }
    private void InitAPI()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("Ads Init Complete");
        });
    }

    #endregion

    Action onRewardedVideoComplete;
    public void ShowRewardedVideo(Action _onRewardedVideoComplete)
    {
        onRewardedVideoComplete = _onRewardedVideoComplete;
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");


        AdRequest adRequest = null;
        // send the request to load the ad.
        RewardedAd.Load(rewardAdUnit, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;

                ShowRewardedAd();
            });
    }
    private void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            Time.timeScale = 0;
            rewardedAd.Show((Reward reward) =>
            {
                Time.timeScale = 1;
                onRewardedVideoComplete?.Invoke();
            });
        }
    }
}