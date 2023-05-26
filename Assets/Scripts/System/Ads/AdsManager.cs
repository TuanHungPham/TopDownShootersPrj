using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    #region public var
    public bool IsAdsLoaded { get => isAdsLoaded; private set => isAdsLoaded = value; }
    #endregion

    #region private var
    [SerializeField] private AdsReward adsReward;
    [SerializeField] private string androidGameID;
    [SerializeField] private string iosGameID;
    [SerializeField] private bool testMode;
    [SerializeField] private bool isAdsLoaded;
    private string gameID;


    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        adsReward = GetComponentInChildren<AdsReward>();

        androidGameID = "5268666";
        iosGameID = "5268667";
        testMode = true;
    }

    private void Start()
    {
        InitializeADS();
    }

    private void Update()
    {
        CheckAdsLoaded();
    }

    private void InitializeADS()
    {
        SetGameID();

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, testMode, this);
        }

        adsReward.LoadAds();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("ADS is initialized successfully!");

        adsReward.LoadAds();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Intiliaze Error " + error.ToString() + " " + message);
    }

    private void SetGameID()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            gameID = androidGameID;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameID = iosGameID;
        }
    }

    private void CheckAdsLoaded()
    {
    }
}
