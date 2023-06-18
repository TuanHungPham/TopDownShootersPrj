using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using TigerForge;

public class PlayfabAccountSystem : MonoBehaviour
{
    #region public var
    public string Email { get => email; set => email = value; }
    #endregion

    #region private var
    [SerializeField] private string email;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private GameObject notiPanel;
    [SerializeField] private TMP_Text errorPanelText;
    [SerializeField] private TMP_Text notiPanelText;
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
        errorPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("ErrorPanel").gameObject;
        errorPanelText = errorPanel.transform.Find("Scroll View").Find("Viewport").GetChild(0).GetComponentInChildren<TMP_Text>();
        notiPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("NotiPanel").gameObject;
        notiPanelText = notiPanel.transform.Find("Scroll View").Find("Viewport").GetChild(0).GetComponentInChildren<TMP_Text>();
    }

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
            notiPanel.gameObject.SetActive(true);
            notiPanelText.text = "Login Facebook account successfully!";

            UpdateUserDisplayName(username);

            EventManager.EmitEvent(EventID.PLAYFAB_LOGGINGIN.ToString());
        }, OnLoginFailed);
    }

    private void OnLoginFailed(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
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

    public void RecoveryPassword()
    {
        SendAccountRecoveryEmailRequest request = new SendAccountRecoveryEmailRequest
        {
            TitleId = "60E5E",
            Email = email
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoveryCallBack, OnRecoveryFailed);
    }

    private void OnRecoveryCallBack(SendAccountRecoveryEmailResult result)
    {
        notiPanel.gameObject.SetActive(true);
        notiPanelText.text = "EMAIL SENT";
    }

    private void OnRecoveryFailed(PlayFabError error)
    {
        errorPanel.gameObject.SetActive(true);
        errorPanelText.text = error.GenerateErrorReport();
    }

    public void LogOut()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        UserManager.Instance.UserName = "";
        DataManager.Instance.Username = "";

        CharacterManagerUI.Instance.ShowUsername();

        FacebookAccountSystem.Instance.LogoutFacebook();
        PlayFabClientAPI.ForgetAllCredentials();
    }
}
