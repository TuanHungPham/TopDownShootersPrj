using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabRegistrySystem : MonoBehaviour
{
    #region public var
    public string Username { get => username; set => username = value; }
    public string Email { get => email; set => email = value; }
    public string Password { get => password; set => password = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject accountRegisterPanel;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private TMP_Text errorPanelText;
    [SerializeField] private string username;
    [SerializeField] private string email;
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
        accountRegisterPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("AccountRegisterPanel").gameObject;
        errorPanel = GameObject.Find("------ UI ------").transform.GetChild(0).Find("ErrorPanel").gameObject;
        errorPanelText = errorPanel.transform.Find("Scroll View").Find("Viewport").GetChild(0).GetComponentInChildren<TMP_Text>();
    }

    public void DisplayRegistryPanel()
    {
        accountRegisterPanel.gameObject.SetActive(true);
    }

    public void RegisterPlayfabAccount()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            Username = username,
            Email = email,
            Password = password,
            DisplayName = username
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegister, OnRegisterError);
    }

    private void OnRegister(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register successfully!");
    }

    private void OnRegisterError(PlayFabError error)
    {
        errorPanel.gameObject.SetActive(true);
        errorPanelText.text = error.GenerateErrorReport();
    }
}
