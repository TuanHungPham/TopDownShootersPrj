using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class GGPlay : MonoBehaviour
{

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
        }
        else
        {
            Debug.Log("Login Unsuccessfully!!!");
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }

        Debug.Log(status);
    }
}
