using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAchievementData : MonoBehaviour
{
    #region public var
    public int enemiesKilled;
    public int highestEnemiesKilled;
    public float survivalTime;
    public float highestSurvivalTime;
    public int coin;
    #endregion

    #region private var
    #endregion

    private void OnEnable()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        if (DataManager.Instance == null) return;

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

        if (highestEnemiesKilled <= enemiesKilled) return;
        highestEnemiesKilled = enemiesKilled;
    }

    private void UpdateSurvivalTime()
    {
        survivalTime = DataManager.Instance.achievementDataManager.survivalTime;

        if (highestSurvivalTime <= survivalTime) return;
        highestSurvivalTime = survivalTime;
    }

    public void ConsumeCoin(int consumptionQuantity)
    {
        coin -= consumptionQuantity;
    }
}
