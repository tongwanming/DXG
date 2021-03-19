using System;
using Common.Utils;
using Utils;

namespace Ads
{
    public class AdManager : SingletonComp<AdManager>
    {
        string rewardedVideoTag = "";

        // Ad manager event for tracking all events of ad status. 
        public static event Action OnSdkInitialisedEvent;
        public static event Action OnBannerLoadedEvent;
        public static event Action<string> OnBannerLoadFailedEvent;

        public static event Action OnInterstitialLoadedEvent;
        public static event Action<string> OnInterstitialLoadFailedEvent;
        public static event Action OnInterstitialShownEvent;
        public static event Action OnInterstitialClosedEvent;

        public static event Action OnRewardedLoadedEvent;

        public static event Action<string> OnRewardedLoadFailedEvent;

        public static event Action OnRewardedShownEvent;
        public static event Action OnRewardedClosedEvent;
        public static event Action<string> OnRewardedAdRewardedEvent;

        private void Start()
        {
            Invoke(nameof(InitAd), 2f);
        }

        private void InitAd()
        {
#if UNITY_ANDROID
            string appKey = "ec7e5079";
#elif UNITY_IPHONE
            string appKey = "ec7e5079";
#else
            string appKey = "ec7e5079";
#endif


            LogUtils.Log("unity-script: IronSource.Agent.validateIntegration");
            IronSource.Agent.validateIntegration();

            LogUtils.Log("unity-script: unity version" + IronSource.unityVersion());

            // SDK init
            LogUtils.Log("unity-script: IronSource.Agent.init");
            IronSource.Agent.init(appKey);

            StartLoadingAds();
        }

        /// <summary>
        /// Loads ads after initialization.
        /// </summary>
        public void StartLoadingAds()
        {
            //Add Rewarded Video Events
            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

            // Add Interstitial Events
            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

            // Add Banner Events
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
            IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
            IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
            IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

            RequestBannerAds();
            RequestInterstitial();
            RequestRewarded();
        }

        private void OnApplicationPause(bool isPaused)
        {
            LogUtils.Log("unity-script: OnApplicationPause = " + isPaused);
            IronSource.Agent.onApplicationPause(isPaused);
        }

        /// Requests banner ad.        
        public void RequestBannerAds()
        {
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        }

        /// Requests intestitial ad.
        public void RequestInterstitial()
        {
            IronSource.Agent.loadInterstitial();
        }

        /// Requests rewarded ad. Rewarded ad loading and caching is done automatically with ironsource sdk.
        public void RequestRewarded()
        {
        }

        /// Shows banner ad.
        public void ShowBanner()
        {
            IronSource.Agent.displayBanner();
        }

        /// Hides banner ad.
        public void HideBanner()
        {
            IronSource.Agent.hideBanner();
        }

        /// Check if interstial ad ready to show.
        public bool IsInterstitialAvailable()
        {
            return IronSource.Agent.isInterstitialReady();
        }

        /// Shows interstitial ad if available.
        public void ShowInterstitial()
        {
            if (IsInterstitialAvailable())
            {
                IronSource.Agent.showInterstitial();
            }
            else
            {
                RequestInterstitial();
            }
        }

        /// Checks if rewarded ad ready to show.
        public bool IsRewardedAvailable()
        {
            return IronSource.Agent.isRewardedVideoAvailable();
        }

        /// Shows rewarded ad if loaded.
        public void ShowRewarded()
        {
            if (IsRewardedAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
        }

        /// <summary>
        /// Show rewarded ad if available. tag is to identify from which location rewarded ad is called.
        /// </summary>
        public void ShowRewardedWithTag(string _rewardedVideoTag = "VideoTag")
        {
            rewardedVideoTag = _rewardedVideoTag;

#if UNITY_EDITOR
            OnRewardedAdRewarded();
#else
            ShowRewarded();
#endif
        }

        #region Callback Integration

        /// Invokes event callback of varient ad status.
        public void OnSdkInitialised()
        {
            if (OnSdkInitialisedEvent != null)
            {
                OnSdkInitialisedEvent.Invoke();
            }
        }

        public void OnBannerLoaded()
        {
            if (OnBannerLoadedEvent != null)
            {
                OnBannerLoadedEvent.Invoke();
            }
        }

        public void OnBannerLoadFailed(string reason)
        {
            if (OnBannerLoadFailedEvent != null)
            {
                OnBannerLoadFailedEvent.Invoke(reason);
            }
        }

        public void OnInterstitialLoaded()
        {
            if (OnInterstitialLoadedEvent != null)
            {
                OnInterstitialLoadedEvent.Invoke();
            }
        }

        public void OnInterstitialLoadFailed(string reason)
        {
            if (OnInterstitialLoadFailedEvent != null)
            {
                OnInterstitialLoadFailedEvent.Invoke(reason);
            }
        }

        public void OnInterstitialShown()
        {
            if (OnInterstitialShownEvent != null)
            {
                OnInterstitialShownEvent.Invoke();
            }
        }

        public void OnInterstitialClosed()
        {
            if (OnInterstitialClosedEvent != null)
            {
                OnInterstitialClosedEvent.Invoke();
            }
        }

        public void OnRewardedLoaded()
        {
            if (OnRewardedLoadedEvent != null)
            {
                OnRewardedLoadedEvent.Invoke();
            }
        }

        public void OnRewardedLoadFailed(string reason)
        {
            {
                if (OnRewardedLoadFailedEvent != null)
                {
                    OnRewardedLoadFailedEvent.Invoke(reason);
                }
            }
        }

        public void OnRewardedShown()
        {
            if (OnRewardedShownEvent != null)
            {
                OnRewardedShownEvent.Invoke();
            }
        }

        public void OnRewardedClosed()
        {
            if (OnRewardedClosedEvent != null)
            {
                OnRewardedClosedEvent.Invoke();
            }
        }

        public void OnRewardedAdRewarded()
        {
            if (OnRewardedAdRewardedEvent != null)
            {
                OnRewardedAdRewardedEvent.Invoke(rewardedVideoTag);
            }
        }

        #endregion

        #region RewardedAd callback handlers

        void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
        {
            LogUtils.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
            if (canShowAd)
            {
                OnRewardedLoaded();
            }
            else
            {
                OnRewardedLoadFailed("fail");
            }
        }

        void RewardedVideoAdOpenedEvent()
        {
            LogUtils.Log("unity-script: I got RewardedVideoAdOpenedEvent");
            OnRewardedShown();
        }

        void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
        {
            LogUtils.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() +
                         " name = " + ssp.getRewardName());
            OnRewardedAdRewarded();
        }

        void RewardedVideoAdClosedEvent()
        {
            LogUtils.Log("unity-script: I got RewardedVideoAdClosedEvent");
            OnRewardedClosed();
        }

        void RewardedVideoAdStartedEvent()
        {
            LogUtils.Log("unity-script: I got RewardedVideoAdStartedEvent");
        }

        void RewardedVideoAdEndedEvent()
        {
            LogUtils.Log("unity-script: I got RewardedVideoAdEndedEvent");
        }

        void RewardedVideoAdShowFailedEvent(IronSourceError error)
        {
            LogUtils.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() +
                         ", description : " + error.getDescription());
        }

        void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
        {
            LogUtils.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
        }

        #endregion

        #region Interstitial callback handlers

        void InterstitialAdReadyEvent()
        {
            LogUtils.Log("unity-script: I got InterstitialAdReadyEvent");
            OnInterstitialLoaded();
        }

        void InterstitialAdLoadFailedEvent(IronSourceError error)
        {
            LogUtils.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() +
                         ", description : " + error.getDescription());
            OnInterstitialLoadFailed(error.getDescription());
        }

        void InterstitialAdShowSucceededEvent()
        {
            LogUtils.Log("unity-script: I got InterstitialAdShowSucceededEvent");
            OnInterstitialShown();
        }

        void InterstitialAdShowFailedEvent(IronSourceError error)
        {
            LogUtils.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() +
                         ", description : " + error.getDescription());
        }

        void InterstitialAdClickedEvent()
        {
            LogUtils.Log("unity-script: I got InterstitialAdClickedEvent");
        }

        void InterstitialAdOpenedEvent()
        {
            LogUtils.Log("unity-script: I got InterstitialAdOpenedEvent");
        }

        void InterstitialAdClosedEvent()
        {
            LogUtils.Log("unity-script: I got InterstitialAdClosedEvent");
            OnInterstitialClosed();

            //缓存插屏
            RequestInterstitial();
        }

        #endregion

        #region Banner callback handlers

        void BannerAdLoadedEvent()
        {
            LogUtils.Log("unity-script: I got BannerAdLoadedEvent");
            OnBannerLoaded();
        }

        void BannerAdLoadFailedEvent(IronSourceError error)
        {
            LogUtils.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " +
                         error.getDescription());
            OnBannerLoadFailed(error.getDescription());
        }

        void BannerAdClickedEvent()
        {
            LogUtils.Log("unity-script: I got BannerAdClickedEvent");
        }

        void BannerAdScreenPresentedEvent()
        {
            LogUtils.Log("unity-script: I got BannerAdScreenPresentedEvent");
        }

        void BannerAdScreenDismissedEvent()
        {
            LogUtils.Log("unity-script: I got BannerAdScreenDismissedEvent");
        }

        void BannerAdLeftApplicationEvent()
        {
            LogUtils.Log("unity-script: I got BannerAdLeftApplicationEvent");
        }

        #endregion
    }
}