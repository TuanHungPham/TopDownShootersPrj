using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    private static UserManager instance;
    public static UserManager Instance { get => instance; set => instance = value; }

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

        LoadComponents();

        LoadData();
        UpdateData();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Username", userName);
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("Highest Enemies Killed", highestEnemiesKilled);
        PlayerPrefs.SetFloat("Highest Survival Time", highestSurvivalTime);
    }

    public void LoadData()
    {
        userName = PlayerPrefs.GetString("Username", "");
        coin = PlayerPrefs.GetInt("Coin", 0);
        highestEnemiesKilled = PlayerPrefs.GetInt("Highest Enemies Killed", 0);
        highestSurvivalTime = PlayerPrefs.GetFloat("Highest Survival Time", 0);
    }

    public void UpdateData()
    {
        UpdateCoin();
        UpdateEnemiesKilled();
        UpdateSurvivalTime();
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
