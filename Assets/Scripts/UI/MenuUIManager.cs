using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    private static MenuUIManager instance;

    public static MenuUIManager Instance { get => instance; set => instance = value; }
    public CoinPanel CoinPanel { get => coinPanel; set => coinPanel = value; }
    public CharacterManagerUI CharacterManagerUI { get => characterManagerUI; set => characterManagerUI = value; }
    public LeaderboardButton LeaderboardButton { get => leaderboardButton; set => leaderboardButton = value; }
    public LeaderboardMangerUI LeaderboardMangerUI { get => leaderboardMangerUI; set => leaderboardMangerUI = value; }

    #region public var
    #endregion

    #region private var
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
        coinPanel = GetComponentInChildren<CoinPanel>();
        characterManagerUI = GetComponentInChildren<CharacterManagerUI>();
        leaderboardButton = GetComponentInChildren<LeaderboardButton>();
        leaderboardMangerUI = GetComponentInChildren<LeaderboardMangerUI>();
    }
}
