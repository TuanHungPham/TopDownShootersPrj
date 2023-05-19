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
    [SerializeField] private GameObject notiPanel;
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private TMP_Text errorPanelText;
    [SerializeField] private TMP_Text notiPanelText;
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
        notiPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("NotiPanel").gameObject;
        notiPanelText = notiPanel.transform.Find("Scroll View").Find("Viewport").GetChild(0).GetComponentInChildren<TMP_Text>();
        darkScreen = GameObject.Find("------ UI ------").transform.GetChild(0).Find("DarkScreen").gameObject;
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
        notiPanel.SetActive(true);
        notiPanelText.text = "Login successfully!";

        loginPanel.SetActive(false);
        darkScreen.SetActive(false);

        DisplayUserName();
    }

    private void OnLoginError(PlayFabError error)
    {
        errorPanel.gameObject.SetActive(true);
        errorPanelText.text = error.GenerateErrorReport();
    }

    private void DisplayUserName()
    {
        UserManager.Instance.userName = username;
        DataManager.Instance.Username = username;
        MenuUIManager.Instance.CharacterManagerUI.ShowUsername();
    }
}
