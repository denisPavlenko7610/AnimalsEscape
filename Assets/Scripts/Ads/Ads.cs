using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace AnimalsEscape
{
    public class Ads : MonoBehaviour
    {
        public event Action onAdClosed;
        
        private InterstitialAd _interstitialAd;
        
#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test
        //private string _adUnitId = "ca-app-pub-7173647303121367/4138065601";
#elif UNITY_IPHONE
  //private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#endif
        
        public void Start()
        {
            MobileAds.Initialize(initStatus =>
            {
                LoadInterstitialAd();
            });
        }
        
       

        public void ShowInterstitialAd()
        {
            RegisterEventHandlers(_interstitialAd);
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }
        
        void LoadInterstitialAd()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");
            
            var adRequest = new AdRequest();
            
            InterstitialAd.Load(_adUnitId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                            "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

                    _interstitialAd = ad;
                });
        }
        
        void RegisterEventHandlers(InterstitialAd interstitialAd)
        {
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                onAdClosed?.Invoke();
                LoadInterstitialAd();

                Debug.Log("Interstitial ad full screen content closed.");
            };

            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                    "with error : " + error);
            };
        }
    }
}