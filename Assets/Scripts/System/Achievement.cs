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
    }

    private void TimeCounter()
    {
        survivalTime += Time.deltaTime;
    }

    public void UpdateToMainData()
    {
        enemiesKilled = DataManager.Instance.achievementDataManager.enemiesKilled;
        survivalTime = DataManager.Instance.achievementDataManager.survivalTime;
        totalMoney = DataManager.Instance.achievementDataManager.coin;
    }
}
