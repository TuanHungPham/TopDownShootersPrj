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
        coin += DataManager.Instance.AchievementDataManager.Coin;
    }

    private void UpdateEnemiesKilled()
    {
        enemiesKilled = DataManager.Instance.AchievementDataManager.HighestEnemiesKilled;

        if (highestEnemiesKilled >= DataManager.Instance.AchievementDataManager.HighestEnemiesKilled) return;
        highestEnemiesKilled = DataManager.Instance.AchievementDataManager.HighestEnemiesKilled;
    }

    private void UpdateSurvivalTime()
    {
        survivalTime = DataManager.Instance.AchievementDataManager.HighestSurvivalTime;

        if (highestSurvivalTime >= DataManager.Instance.AchievementDataManager.HighestSurvivalTime) return;
        highestSurvivalTime = DataManager.Instance.AchievementDataManager.HighestSurvivalTime;
    }

    public void ConsumeCoin(int consumptionQuantity)
    {
        coin -= consumptionQuantity;
    }
}
