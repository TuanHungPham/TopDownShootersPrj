using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsernameInput : MonoBehaviour
{
    #region public var
    public string NameInput { get => nameInput; set => nameInput = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private GameObject leaderBoardManager;
    [SerializeField] private GameObject usernameInputPanel;
    [SerializeField] private CharacterManagerUI characterManagerUI;
    private string nameInput;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        CheckIsUserHasName();
    }

    private void LoadComponents()
    {
        darkScreen = transform.parent.Find("DarkScreen").gameObject;
        leaderBoardManager = transform.parent.Find("LeaderboardManagerUI").gameObject;
        characterManagerUI = transform.parent.Find("CharacterManagerUI").GetComponent<CharacterManagerUI>();
        usernameInputPanel = transform.Find("UsernameInputPanel").gameObject;
    }

    public void ReadInput(string input)
    {
        nameInput = input;
    }

    private void CheckIsUserHasName()
    {
        if (!UserManager.Instance.userName.Equals(""))
        {
            SetPanelStatus(false);
            leaderBoardManager.SetActive(true);
            return;
        }

        SetPanelStatus(true);
    }

    public void Confirm()
    {
        UserManager.Instance.userName = nameInput;
        SetPanelStatus(false);

        characterManagerUI.ShowUsername();
        leaderBoardManager.SetActive(true);
    }

    private void SetPanelStatus(bool check)
    {
        usernameInputPanel.SetActive(check);
        darkScreen.gameObject.SetActive(check);
    }
}
