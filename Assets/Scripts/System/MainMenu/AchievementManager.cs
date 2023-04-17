using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private static AchievementManager instance;
    public static AchievementManager Instance { get => instance; set => instance = value; }

    #region public var
    public MainAchievementData mainAchievementData;
    public string userName;
    #endregion

    #region private var
    #endregion

    private void Awake()
    {
        instance = this;

        LoadData();
        LoadComponents();
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
        PlayerPrefs.SetInt("Coin", mainAchievementData.coin);
    }

    public void LoadData()
    {
        mainAchievementData.coin = PlayerPrefs.GetInt("Coin", 0);
    }

    private void OnDisable()
    {
        SaveData();
    }
}
