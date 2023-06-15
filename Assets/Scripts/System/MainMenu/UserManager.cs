using UnityEngine;
using TigerForge;

public class UserManager : MonoBehaviour
{
    private static UserManager instance;
    public static UserManager Instance { get => instance; }

    #region public var
    public string UserName { get => userName; set => userName = value; }
    public int HighestEnemiesKilled { get => highestEnemiesKilled; set => highestEnemiesKilled = value; }
    public float HighestSurvivalTime { get => highestSurvivalTime; set => highestSurvivalTime = value; }
    #endregion

    #region private var
    [SerializeField] private string userName;
    [SerializeField] private int highestEnemiesKilled;
    [SerializeField] private float highestSurvivalTime;
    #endregion

    private void Awake()
    {
        instance = this;

        UpdateData();
    }

    public void UpdateData()
    {
        UpdateUsername();
        UpdateEnemiesKilled();
        UpdateSurvivalTime();
    }

    private void UpdateUsername()
    {
        UserName = DataManager.Instance.Username;
    }

    private void UpdateEnemiesKilled()
    {
        highestEnemiesKilled = DataManager.Instance.AchievementDataManager.HighestEnemiesKilled;
    }

    private void UpdateSurvivalTime()
    {
        highestSurvivalTime = DataManager.Instance.AchievementDataManager.HighestSurvivalTime;
    }

    public void AddCoinToDataManager(int coinQuantity)
    {
        DataManager.Instance.AchievementDataManager.AddCoin(coinQuantity);
        EventManager.EmitEvent(EventID.CHANGING_COIN_QUANTITY.ToString());
    }
}
