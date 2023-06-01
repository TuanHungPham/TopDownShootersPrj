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
        coin += DataManager.Instance.AchievementDataManager.coin;
    }

    private void UpdateEnemiesKilled()
    {
        enemiesKilled = DataManager.Instance.AchievementDataManager.enemiesKilled;

        if (highestEnemiesKilled >= DataManager.Instance.AchievementDataManager.enemiesKilled) return;
        highestEnemiesKilled = DataManager.Instance.AchievementDataManager.enemiesKilled;
    }

    private void UpdateSurvivalTime()
    {
        survivalTime = DataManager.Instance.AchievementDataManager.survivalTime;

        if (highestSurvivalTime >= DataManager.Instance.AchievementDataManager.survivalTime) return;
        highestSurvivalTime = DataManager.Instance.AchievementDataManager.survivalTime;
    }

    public void ConsumeCoin(int consumptionQuantity)
    {
        coin -= consumptionQuantity;
    }
}
