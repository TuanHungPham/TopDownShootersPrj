using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabLoginSystem : MonoBehaviour
{
    #region public var
    public string Username { get => username; set => username = value; }
    public string Password { get => password; set => password = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private TMP_Text errorPanelText;
    [SerializeField] private string username;
    [SerializeField] private string password;
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
        loginPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("LoginPanel").gameObject;
        errorPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("ErrorPanel").gameObject;
        errorPanelText = errorPanel.transform.Find("Scroll View").Find("Viewport").GetChild(0).GetComponentInChildren<TMP_Text>();
    }

    public void LoginPlayfabAccount()
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginCallBack, OnLoginError);
    }

    private void OnLoginCallBack(LoginResult result)
    {
        Debug.Log("Login successfully!");
    }

    private void OnLoginError(PlayFabError error)
    {
        errorPanel.gameObject.SetActive(true);
        errorPanelText.text = error.GenerateErrorReport();
    }
}
