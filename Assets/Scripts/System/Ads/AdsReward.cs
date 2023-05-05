using UnityEngine;
using UnityEngine.Advertisements;

public class AdsReward : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    #region public var
    #endregion 

    #region private var
    [SerializeField] private string androidAdUnitID;
    [SerializeField] private string iosAdUnitID;
    private string adUnitID;
    #endregion

    private void Awake()
    {
        LoadComponents();

        SetUnitID();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        androidAdUnitID = "Rewarded_Android";
        iosAdUnitID = "Rewarded_iOS";
    }

    private void SetUnitID()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            adUnitID = androidAdUnitID;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            adUnitID = iosAdUnitID;
        }
    }
    public void LoadAds()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void ShowAds()
    {
        Advertisement.Show(adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ads loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Ads loaded Failed: " + error.ToString() + " " + message);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (!placementId.Equals(adUnitID) || !showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) return;

        Debug.Log("Unity Ads Rewarded Ad Completed");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Ads Showed Failed: " + error.ToString() + " " + message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }
}
