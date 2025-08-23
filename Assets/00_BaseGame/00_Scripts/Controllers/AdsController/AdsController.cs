using UnityEngine;
using GoogleMobileAds.Api;
using System;
using EventDispatcher;

public class AdsController : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private AppOpenAd appOpenAd;

    // Ad Unit IDs của bạn
    private string bannerId = "ca-app-pub-4739017290334481/4383883316"; // Bieu ngu
    private string interstitialId = "ca-app-pub-4739017290334481/6239906374"; // Trung gian
    private string rewardedId = "ca-app-pub-4739017290334481/2604716024"; // Nhan thuong
    private string appOpenId = "ca-app-pub-4739017290334481/8079601655";

    public void Init()
    {
        // Khởi tạo SDK
        MobileAds.Initialize(initStatus => { 
            Debug.Log("AdMob SDK initialized");
        });

        // KHÔNG load banner ở đây nữa
        RequestInterstitial();
        RequestRewardedAd();
        RequestAppOpenAd();
    }

    #region BANNER ADS
    public void RequestBanner()
    {
        this.bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest();
        this.bannerView.LoadAd(request);
        Debug.Log("Banner requested");
    }

    public void ShowBanner()
    {
        if (bannerView != null)
            bannerView.Show();
    }

    public void HideBanner()
    {
        if (bannerView != null)
            bannerView.Hide();
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }
    #endregion

    #region INTERSTITIAL ADS
    public void RequestInterstitial()
    {
        InterstitialAd.Load(interstitialId, new AdRequest(), (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogError("Interstitial failed to load: " + error.GetMessage());
                return;
            }

            interstitialAd = ad;
            Debug.Log("Interstitial loaded");

            interstitialAd.OnAdFullScreenContentClosed += () => {
                Debug.Log("Interstitial closed");
                RequestInterstitial();
            };

            interstitialAd.OnAdFullScreenContentFailed += (AdError error) => {
                Debug.LogError("Interstitial failed to show: " + error.GetMessage());
                RequestInterstitial();
            };
        });
    }

    public void ShowInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Show();
            Debug.Log("Showing Interstitial");
        }
        else
        {
            Debug.Log("Interstitial not ready");
        }
    }
    #endregion

    #region REWARDED ADS
    public void RequestRewardedAd()
    {
        RewardedAd.Load(rewardedId, new AdRequest(), (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error.GetMessage());
                return;
            }

            rewardedAd = ad;
            Debug.Log("Rewarded ad loaded");

            rewardedAd.OnAdFullScreenContentClosed += () => {
                Debug.Log("Rewarded ad closed");
                RequestRewardedAd();
            };

            rewardedAd.OnAdFullScreenContentFailed += (AdError error) => {
                Debug.LogError("Rewarded ad failed to show: " + error.GetMessage());
                RequestRewardedAd();
            };
        });
    }

    private void HandleUserEarnedReward(RewardedAd sender, Reward args)
    {
        Debug.Log("User earned reward: " + args.Amount + " " + args.Type);
        
        // Gửi event thông báo nhận thưởng thành công
        this.PostEvent(EventID.REWARDED_AD_COMPLETED, args);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show((Reward reward) =>
            {
                HandleUserEarnedReward(rewardedAd, reward);
            });
            Debug.Log("Showing Rewarded Ad");
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }
    #endregion

    #region APP OPEN ADS
    public void RequestAppOpenAd()
    {
        AppOpenAd.Load(appOpenId, new AdRequest(), (AppOpenAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogError("AppOpenAd failed to load: " + error.GetMessage());
                return;
            }

            appOpenAd = ad;
            Debug.Log("AppOpen ad loaded");

            appOpenAd.OnAdFullScreenContentClosed += () => {
                Debug.Log("AppOpen ad closed");
                RequestAppOpenAd();
            };

            appOpenAd.OnAdFullScreenContentFailed += (AdError error) => {
                Debug.LogError("AppOpen ad failed to show: " + error.GetMessage());
                RequestAppOpenAd();
            };
        });
    }

    public void ShowAppOpenAd()
    {
        if (appOpenAd != null)
        {
            appOpenAd.Show();
            Debug.Log("Showing AppOpen Ad");
        }
        else
        {
            Debug.Log("AppOpen ad not ready");
        }
    }
    #endregion
}