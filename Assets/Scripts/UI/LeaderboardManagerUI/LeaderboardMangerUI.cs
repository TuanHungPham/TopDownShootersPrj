using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMangerUI : MonoBehaviour
{
    #region public var
    public List<UserManager> listOfUser = new List<UserManager>();
    #endregion

    #region private var
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private GameObject enemiesKilledBoard;
    [SerializeField] private GameObject survivalTimeBoard;
    [SerializeField] private EnemiesKilledPanel enemiesKilledPanel;
    [SerializeField] private SurvivalTimePanel survivalTimePanel;
    #endregion

    private void OnEnable()
    {
        ResetUserList();
    }

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
        InitializeUserList();
    }

    private void InitializeUserList()
    {
        for (int i = 0; i < 9; i++)
        {
            listOfUser.Add(new UserManager
            {
                UserName = "",
                HighestEnemiesKilled = 0,
                HighestSurvivalTime = 0,
            });
        }
    }

    private void ResetUserList()
    {
        foreach (var user in listOfUser)
        {
            user.UserName = "";
            user.HighestEnemiesKilled = 0;
            user.HighestSurvivalTime = 0;
        }
    }

    private void LoadComponents()
    {
        darkScreen = transform.parent.Find("DarkScreen").gameObject;
        enemiesKilledBoard = transform.Find("EnemiesKilledPanel").gameObject;
        survivalTimeBoard = transform.Find("SurvivalTimePanel").gameObject;
        enemiesKilledPanel = enemiesKilledBoard.GetComponent<EnemiesKilledPanel>();
        survivalTimePanel = survivalTimeBoard.GetComponent<SurvivalTimePanel>();
    }

    public void DisplayEnemiesKilledBoard()
    {
        StartCoroutine(SetEnemiesKilledBoard());
    }

    public void DisplaySurvivalTimeBoard()
    {
        StartCoroutine(SetSurvivalTimeBoard());
    }

    private IEnumerator SetEnemiesKilledBoard()
    {
        enemiesKilledBoard.SetActive(true);
        darkScreen.SetActive(true);

        PlayfabSystemManager.Instance.PlayfabLeaderboardSystem.GetGlobalLeaderboard("EnemiesKilled", listOfUser);
        yield return new WaitForSeconds(1);
        enemiesKilledPanel.ShowUserAchievement(listOfUser);
    }

    private IEnumerator SetSurvivalTimeBoard()
    {
        darkScreen.SetActive(true);
        survivalTimeBoard.SetActive(true);

        PlayfabSystemManager.Instance.PlayfabLeaderboardSystem.GetGlobalLeaderboard("SurvivalTime", listOfUser);
        yield return new WaitForSeconds(1);
        survivalTimePanel.ShowUserAchievement(listOfUser);
    }
}
