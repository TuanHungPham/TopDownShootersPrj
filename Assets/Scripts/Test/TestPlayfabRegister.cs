using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class TestPlayfabRegister : MonoBehaviour
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

    public void Register()
    {
        RegisterPlayFabUserRequest registerPlayFabUserRequest = new RegisterPlayFabUserRequest
        {
            Username = TestUIManager.Instance.TestRegisterUI.UsernameInput,
            Password = TestUIManager.Instance.TestRegisterUI.PasswordInput,
            Email = TestUIManager.Instance.TestRegisterUI.EmailInput,
            DisplayName = TestUIManager.Instance.TestRegisterUI.DisplayNameInput,
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerPlayFabUserRequest, RegisterSuccess, RegisterError);
    }

    public void RegisterSuccess(RegisterPlayFabUserResult registerPlayFabUserResult)
    {
        TestUIManager.Instance.TestNotificationUI.EnablePanel();
        TestUIManager.Instance.TestNotificationUI.NotificationText.text = "Register successfull";

        TestUIManager.Instance.TestRegisterUI.DisablePanel();
        TestUIManager.Instance.TestLoginUI.EnablePanel();
    }

    public void RegisterError(PlayFabError error)
    {
        string textError = error.GenerateErrorReport();
        Debug.Log(textError);

        TestUIManager.Instance.TestErrorUI.EnablePanel();
        TestUIManager.Instance.TestErrorUI.ErrorText.text = textError;
    }
}
