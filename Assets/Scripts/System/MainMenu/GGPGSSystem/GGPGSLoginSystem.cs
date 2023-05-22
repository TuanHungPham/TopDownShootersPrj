using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GGPGSLoginSystem : MonoBehaviour
{
    #region public var
    public bool IsGGPGSLoggedIn { get => isGGPGSLoggedIn; private set => isGGPGSLoggedIn = value; }
    #endregion

    #region private var
    [SerializeField] private bool isGGPGSLoggedIn;

    #endregion

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(OnSignInResult);
    }

    private void OnSignInResult(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            Debug.Log("Login Successfully!!!");
            isGGPGSLoggedIn = true;
        }
        else
        {
            Debug.Log("Login Unsuccessfully!!!");
            isGGPGSLoggedIn = false;
        }

        Debug.Log(status);
    }
}
