using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsReward : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    #region public var
    #endregion 

    #region private var
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private string androidAdUnitID;
    [SerializeField] private string iosAdUnitID;
    private string adUnitID;
    #endregion

    private void Awake()
    {
        LoadComponents();

        SetUnitID();
    }

    private void Start()
    {
        SetupRewardPanelRandomly();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        if (SceneManager.GetActiveScene().Equals("MainMenu"))
        {
            rewardPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("RewardPanel").gameObject;
            darkScreen = GameObject.Find("------ UI ------").transform.GetChild(0).Find("DarkScreen").gameObject;
        }

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
        Time.timeScale = 0;
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
        Reward();
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().Equals("MainMenu"))
        {
            rewardPanel.SetActive(false);
            darkScreen.SetActive(false);
        }
    }

    private void Reward()
    {
        if (SceneManager.GetActiveScene().Equals("MainMenu"))
        {
            UserManager.Instance.coin += 1500;
            return;
        }

        InGameManager.Instance.respawnManager.Respawn();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Ads Showed Failed: " + error.ToString() + " " + message);
        Time.timeScale = 1;
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    private void SetupRewardPanelRandomly()
    {
        if (!SceneManager.GetActiveScene().Equals("MainMenu")) return;

        float random = Random.Range(0f, 1f);
        Debug.Log(random);

        if (random > 0.65f) return;

        rewardPanel.SetActive(true);
        darkScreen.SetActive(true);
    }
}
