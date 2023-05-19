using UnityEngine;
using PlayFab;
using Facebook.Unity;
using System;

public class UserManager : MonoBehaviour
{
    private static UserManager instance;
    public static UserManager Instance { get => instance; }

    #region public var
    public string userName;
    public int enemiesKilled;
    public int highestEnemiesKilled;
    public float survivalTime;
    public float highestSurvivalTime;
    public int coin;
    public bool IsAchievementUpdated { get => isAchievementUpdated; private set => isAchievementUpdated = value; }
    #endregion

    #region private var
    [SerializeField] private bool isAchievementUpdated;
    #endregion

    private void Awake()
    {
        instance = this;

        LoadData();
        UpdateData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("Highest Enemies Killed", highestEnemiesKilled);
        PlayerPrefs.SetFloat("Highest Survival Time", highestSurvivalTime);
    }

    public void LoadData()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
        highestEnemiesKilled = PlayerPrefs.GetInt("Highest Enemies Killed", 0);
        highestSurvivalTime = PlayerPrefs.GetFloat("Highest Survival Time", 0);
    }

    public void UpdateData()
    {
        UpdateUsername();
        UpdateCoin();
        UpdateEnemiesKilled();
        UpdateSurvivalTime();
    }

    private void UpdateUsername()
    {
        userName = DataManager.Instance.Username;
    }

    private void UpdateCoin()
    {
        coin += DataManager.Instance.achievementDataManager.coin;
    }

    private void UpdateEnemiesKilled()
    {
        enemiesKilled = DataManager.Instance.achievementDataManager.enemiesKilled;

        if (highestEnemiesKilled >= DataManager.Instance.achievementDataManager.enemiesKilled) return;

        highestEnemiesKilled = DataManager.Instance.achievementDataManager.enemiesKilled;
        isAchievementUpdated = true;
    }

    private void UpdateSurvivalTime()
    {
        survivalTime = DataManager.Instance.achievementDataManager.survivalTime;

        if (highestSurvivalTime >= DataManager.Instance.achievementDataManager.survivalTime) return;

        highestSurvivalTime = DataManager.Instance.achievementDataManager.survivalTime;
        isAchievementUpdated = true;
    }

    public void ConsumeCoin(int consumptionQuantity)
    {
        coin -= consumptionQuantity;
    }

    private void OnDisable()
    {
        SaveData();
    }
}
