using UnityEngine;

public class AchievementDataManager : MonoBehaviour
{
    #region public var
    public int HighestEnemiesKilled { get => highestEnemiesKilled; set => highestEnemiesKilled = value; }
    public float HighestSurvivalTime { get => highestSurvivalTime; set => highestSurvivalTime = value; }
    public int Coin { get => coin; set => coin = value; }
    #endregion

    #region private var
    [SerializeField] private int highestEnemiesKilled;
    [SerializeField] private float highestSurvivalTime;
    [SerializeField] private int coin;
    #endregion

    public void ConsumeCoin(int consumptionQuantity)
    {
        Coin -= consumptionQuantity;
    }

    public void AddCoin(int coinQuantity)
    {
        Coin += coinQuantity;
    }
}
