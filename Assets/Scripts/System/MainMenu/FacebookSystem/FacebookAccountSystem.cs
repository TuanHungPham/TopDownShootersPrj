using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;

public class FacebookAccountSystem : MonoBehaviour
{
    private static FacebookAccountSystem instance;
    public static FacebookAccountSystem Instance { get => instance; private set => instance = value; }

    #region public var
    #endregion

    #region private var
    private AccessToken accessToken;
    #endregion

    private void Awake()
    {
        instance = this;
        InitializeFacebookSDK();
    }

    private void Start()
    {
        DisplayUsernameAtStart();
    }

    private void InitializeFacebookSDK()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            return;
        }

        FB.Init(SetInit, OnHideUnity);
    }

    private void OnHideUnity(bool isUnityShown)
    {
        if (isUnityShown)
        {
            Time.timeScale = 1;
            return;
        }

        Time.timeScale = 0;
    }

    private void SetInit()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("FacebookSDK is initialized!");
            return;
        }

        Debug.Log("Failed to initialize FacebookSDK");
    }

    public void LoginFacebook()
    {
        if (FB.IsLoggedIn) return;

        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("user_friends");
        permissions.Add("email");

        FB.LogInWithReadPermissions(permissions, OnFacebookLogin);
    }

    private void OnFacebookLogin(ILoginResult result)
    {
        if (result.Error != null)
        {
            Debug.LogWarning(result.Error);
            return;
        }

        Debug.Log("Login Facebook successfully!");
        GetUserInformation();
    }

    private void GetUserInformation()
    {
        FB.API("/me?fields=name", HttpMethod.GET, DisplayUsername);

        accessToken = AccessToken.CurrentAccessToken;
    }

    private void DisplayUsername(IGraphResult result)
    {
        if (result.Error != null)
        {
            Debug.LogWarning(result.Error);
            return;
        }

        string username = result.ResultDictionary["name"].ToString();

        UserManager.Instance.UserName = username;
        DataManager.Instance.Username = username;
        MenuUIManager.Instance.CharacterManagerUI.ShowUsername();

        PlayfabSystemManager.Instance.PlayfabAccountSystem.LoginWithFacebook(accessToken.TokenString, username);
    }

    private static void DisplayUsernameAtStart()
    {
        if (!FB.IsLoggedIn) return;

        MenuUIManager.Instance.CharacterManagerUI.ShowUsername();
    }

    public void LogoutFacebook()
    {
        if (!FB.IsLoggedIn) return;

        FB.LogOut();
    }
}
