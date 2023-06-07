using UnityEngine;
using UnityEngine.UI;

public class LeaderboardButton : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private GameObject boardButtonPanel;
    [SerializeField] private bool isShowed;
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
        leaderboardButton = GetComponent<Button>();
        boardButtonPanel = transform.Find("Panel").gameObject;
    }

    private void Update()
    {
        HandleButton();
    }

    private void HandleButton()
    {
        if (!PlayFab.PlayFabClientAPI.IsClientLoggedIn())
        {
            leaderboardButton.interactable = false;
            return;
        }

        leaderboardButton.interactable = true;
    }

    public void ShowButtonPanel()
    {
        if (!isShowed)
        {
            boardButtonPanel.SetActive(true);
            isShowed = true;
            return;
        }

        boardButtonPanel.SetActive(false);
        isShowed = false;
    }
}
