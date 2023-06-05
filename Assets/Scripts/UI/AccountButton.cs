using UnityEngine;
using PlayFab;

public class AccountButton : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private GameObject logOutButton;
    [SerializeField] private GameObject accountRegisterPanel;
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
        darkScreen = transform.parent.parent.Find("DarkScreen").gameObject;
        accountRegisterPanel = transform.parent.parent.Find("AccountRegisterPanel").gameObject;
        logOutButton = transform.Find("LogOutButton").gameObject;
    }

    public void ShowAccountSystemPanel()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            logOutButton.gameObject.SetActive(true);
            return;
        }

        accountRegisterPanel.gameObject.SetActive(true);
        darkScreen.gameObject.SetActive(true);
    }
}
