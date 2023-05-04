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
    public bool IsAchievementUpdated { get => isAchievementUpdated; private set => isAchievementUpdated = value; }
    #endregion

    #region private var
    [SerializeField] private bool isAchievementUpdated;
    #endregion

    private void Awake()
    {
        isAchievementUpdated = false;
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
}
