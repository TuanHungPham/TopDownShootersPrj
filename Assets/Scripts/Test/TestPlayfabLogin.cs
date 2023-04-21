using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class TestPlayfabLogin : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
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

    }

    public void Login()
    {
        LoginWithPlayFabRequest loginWithPlayFabRequest = new LoginWithPlayFabRequest
        {
            Username = TestUIManager.Instance.TestLoginUI.UsernameInput,
            Password = TestUIManager.Instance.TestLoginUI.PasswordInput
        };

        PlayFabClientAPI.LoginWithPlayFab(loginWithPlayFabRequest, LoginSuccess, LoginError);
    }

    private void LoginSuccess(LoginResult loginResult)
    {
        TestUIManager.Instance.TestNotificationUI.EnablePanel();
        TestUIManager.Instance.TestNotificationUI.NotificationText.text = "Log In successfull!";
    }

    private void LoginError(PlayFabError error)
    {
        string textError = error.GenerateErrorReport();
        Debug.Log(textError);

        TestUIManager.Instance.TestErrorUI.EnablePanel();
        TestUIManager.Instance.TestErrorUI.ErrorText.text = textError;
    }
}
