using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
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
        boardButtonPanel = transform.Find("Panel").gameObject;
    }

    public void ShowButtonPanel()
    {
        if (!PlayFab.PlayFabClientAPI.IsClientLoggedIn()) return;

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
