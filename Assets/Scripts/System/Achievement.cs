using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    private static Achievement instance;
    public static Achievement Instance { get => instance; }

    #region public var
    public int enemiesKilled;
    public float survivalTime;
    public int totalDmg;
    public int totalMoney;
    #endregion

    #region private var
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
        instance = this;
    }

    private void Update()
    {
        TimeCounter();
        UpdateToMainData();
    }

    private void TimeCounter()
    {
        survivalTime += Time.deltaTime;
    }

    public void UpdateToMainData()
    {
        DataManager.Instance.achievementDataManager.enemiesKilled = enemiesKilled;
        survivalTime = DataManager.Instance.achievementDataManager.survivalTime = survivalTime;
        totalMoney = DataManager.Instance.achievementDataManager.coin = totalMoney;
    }

    public void SaveDataWhenRetry()
    {
        int highestEnemiesKilled = PlayerPrefs.GetInt("Highest Enemies Killed", 0);
        float highestSurvivalTime = PlayerPrefs.GetFloat("Highest Survival Time", 0);
        int coin = PlayerPrefs.GetInt("Coin", 0);

        if (highestEnemiesKilled < enemiesKilled)
        {
            highestEnemiesKilled = enemiesKilled;
        }

        if (highestSurvivalTime < survivalTime)
        {
            highestSurvivalTime = survivalTime;
        }

        coin += totalMoney;

        SaveDataIngame(highestEnemiesKilled, highestSurvivalTime, coin);
    }

    private static void SaveDataIngame(int highestEnemiesKilled, float highestSurvivalTime, int coin)
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("Highest Enemies Killed", highestEnemiesKilled);
        PlayerPrefs.SetFloat("Highest Survival Time", highestSurvivalTime);
    }
}
