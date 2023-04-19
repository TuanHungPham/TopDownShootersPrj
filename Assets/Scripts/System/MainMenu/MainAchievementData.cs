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
    }

    private void UpdateSurvivalTime()
    {
        survivalTime = DataManager.Instance.achievementDataManager.survivalTime;

        if (highestSurvivalTime >= DataManager.Instance.achievementDataManager.survivalTime) return;
        highestSurvivalTime = DataManager.Instance.achievementDataManager.survivalTime;
    }

    public void ConsumeCoin(int consumptionQuantity)
    {
        coin -= consumptionQuantity;
    }
}
