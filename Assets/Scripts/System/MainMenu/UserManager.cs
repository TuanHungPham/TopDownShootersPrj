using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    private static UserManager instance;
    public static UserManager Instance { get => instance; set => instance = value; }

    #region public var
    public MainAchievementData mainAchievementData;
    public string userName;
    #endregion

    #region private var
    #endregion

    private void Awake()
    {
        instance = this;

        LoadComponents();

        LoadData();
        mainAchievementData.UpdateData();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        mainAchievementData = GetComponent<MainAchievementData>();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Username", userName);
        PlayerPrefs.SetInt("Coin", mainAchievementData.coin);
        PlayerPrefs.SetInt("Highest Enemies Killed", mainAchievementData.highestEnemiesKilled);
        PlayerPrefs.SetFloat("Highest Survival Time", mainAchievementData.highestSurvivalTime);
    }

    public void LoadData()
    {
        userName = PlayerPrefs.GetString("Username", "");
        mainAchievementData.coin = PlayerPrefs.GetInt("Coin", 0);
        mainAchievementData.highestEnemiesKilled = PlayerPrefs.GetInt("Highest Enemies Killed", 0);
        mainAchievementData.highestSurvivalTime = PlayerPrefs.GetFloat("Highest Survival Time", 0);
    }

    private void OnDisable()
    {
        SaveData();
    }
}
