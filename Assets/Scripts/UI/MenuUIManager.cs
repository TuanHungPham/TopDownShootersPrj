using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    private static MenuUIManager instance;
    public static MenuUIManager Instance { get => instance; set => instance = value; }

    #region public var
    public CoinPanel CoinPanel { get => coinPanel; set => coinPanel = value; }
    public CharacterManagerUI CharacterManagerUI { get => characterManagerUI; set => characterManagerUI = value; }
    public LeaderboardButton LeaderboardButton { get => leaderboardButton; set => leaderboardButton = value; }
    public LeaderboardMangerUI LeaderboardMangerUI { get => leaderboardMangerUI; set => leaderboardMangerUI = value; }
    #endregion

    #region private var
    [SerializeField] private List<Button> buttonList;
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private CoinPanel coinPanel;
    [SerializeField] private CharacterManagerUI characterManagerUI;
    [SerializeField] private LeaderboardButton leaderboardButton;
    [SerializeField] private LeaderboardMangerUI leaderboardMangerUI;
    #endregion

    private void Awake()
    {
        instance = this;

        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        darkScreen = transform.Find("DarkScreen").gameObject;

        coinPanel = GetComponentInChildren<CoinPanel>();
        characterManagerUI = GetComponentInChildren<CharacterManagerUI>();
        leaderboardButton = GetComponentInChildren<LeaderboardButton>();
        leaderboardMangerUI = GetComponentInChildren<LeaderboardMangerUI>();
    }

    private void Start()
    {
        InitializeButtonList();
    }

    private void Update()
    {
        HandleMenuButton();
    }

    private void InitializeButtonList()
    {
        foreach (Transform child in transform)
        {
            Button button = child.GetComponent<Button>();

            if (button == null || buttonList.Contains(button)) continue;

            buttonList.Add(button);
        }
    }

    private void HandleMenuButton()
    {
        if (!darkScreen.activeSelf)
        {
            SetupButton(true);
            return;
        }

        SetupButton(false);
    }

    private void SetupButton(bool set)
    {
        foreach (Button button in buttonList)
        {
            if (button.interactable = set) continue;

            button.interactable = set;
        }
    }
}
