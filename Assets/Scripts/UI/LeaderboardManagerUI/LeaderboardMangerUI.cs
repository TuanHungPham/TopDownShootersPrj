using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMangerUI : MonoBehaviour
{
    #region public var
    public List<UserManager> listOfUser = new List<UserManager>();
    public List<UserManager> listNewUser = new List<UserManager>();
    #endregion

    #region private var
    [SerializeField] private GameObject enemiesKilledBoard;
    [SerializeField] private GameObject survivalTimeBoard;
    [SerializeField] private EnemiesKilledPanel enemiesKilledPanel;
    [SerializeField] private SurvivalTimePanel survivalTimePanel;
    #endregion

    private void OnEnable()
    {
        foreach (UserManager user in listNewUser)
        {
            AddUser(user);
        }
    }

    private void Awake()
    {
        LoadComponents();

        foreach (UserManager user in listNewUser)
        {
            AddUser(user);
        }
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        enemiesKilledBoard = transform.Find("EnemiesKilledPanel").gameObject;
        survivalTimeBoard = transform.Find("SurvivalTimePanel").gameObject;
        enemiesKilledPanel = enemiesKilledBoard.GetComponent<EnemiesKilledPanel>();
        survivalTimePanel = survivalTimeBoard.GetComponent<SurvivalTimePanel>();
    }

    private void Update()
    {

    }

    public void DisplayEnemiesKilledBoard()
    {
        enemiesKilledBoard.SetActive(true);
        enemiesKilledPanel.ShowUserAchievement(listOfUser);
    }

    public void DisplaySurvivalTimeBoard()
    {
        survivalTimeBoard.SetActive(true);
        survivalTimePanel.ShowUserAchievement(listOfUser);
    }

    public void AddUser(UserManager user)
    {
        if (listOfUser.Contains(user)) return;

        UserManager user1 = listOfUser.Find((x) => x.userName == user.userName);
        if (user1 == null)
        {
            AddNewUser(user);
            return;
        }

        EditExistedUser(user1, user);
    }

    private void AddNewUser(UserManager user)
    {
        listOfUser.Add(user);
    }

    private void EditExistedUser(UserManager oldUserStatus, UserManager newUserStatus)
    {
        int index = listOfUser.IndexOf(oldUserStatus);

        if (listOfUser[index].mainAchievementData.highestEnemiesKilled < newUserStatus.mainAchievementData.highestEnemiesKilled)
        {
            listOfUser[index].mainAchievementData.highestEnemiesKilled = newUserStatus.mainAchievementData.highestEnemiesKilled;
        }

        if (listOfUser[index].mainAchievementData.highestSurvivalTime < newUserStatus.mainAchievementData.highestSurvivalTime)
        {
            listOfUser[index].mainAchievementData.highestSurvivalTime = newUserStatus.mainAchievementData.highestSurvivalTime;
        }
    }
}
