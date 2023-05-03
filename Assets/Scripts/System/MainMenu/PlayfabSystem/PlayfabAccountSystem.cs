using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabAccountSystem : MonoBehaviour
{
    public void LoginWithFacebook(string accessToken, string username)
    {
        LoginWithFacebookRequest request = new LoginWithFacebookRequest
        {
            AccessToken = accessToken,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithFacebook(request,
        result =>
        {
            Debug.Log("Login Playfab with Facebook account successfully!");
            UpdateUserDisplayName(username);
        }, OnLoginFailed);
    }

    private void OnLoginFailed(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Playfab with Facebook account successfully!");
    }

    private void UpdateUserDisplayName(string username)
    {
        UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = username
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateSuccess, OnUpdateFailed);
    }

    private void OnUpdateFailed(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
    }

    private void OnUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Update Display Name successfully!");
    }
}
