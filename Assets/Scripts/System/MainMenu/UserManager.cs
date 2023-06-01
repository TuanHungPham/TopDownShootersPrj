using UnityEngine;

public class UserManager : MonoBehaviour
{
    private static UserManager instance;
    public static UserManager Instance { get => instance; }

    #region public var
    public bool IsAchievementUpdated { get => isAchievementUpdated; private set => isAchievementUpdated = value; }
    public string UserName { get => userName; set => userName = value; }
    public int EnemiesKilled { get => enemiesKilled; set => enemiesKilled = value; }
    public int HighestEnemiesKilled { get => highestEnemiesKilled; set => highestEnemiesKilled = value; }
    public float SurvivalTime { get => survivalTime; set => survivalTime = value; }
    public float HighestSurvivalTime { get => highestSurvivalTime; set => highestSurvivalTime = value; }
    public int Coin { get => coin; set => coin = value; }
    #endregion

    #region private var
    [SerializeField] private string userName;
    [SerializeField] private int enemiesKilled;
    [SerializeField] private int highestEnemiesKilled;
    [SerializeField] private float survivalTime;
    [SerializeField] private float highestSurvivalTime;
    [SerializeField] private int coin;
    [SerializeField] private bool isAchievementUpdated;
    #endregion

    private void Awake()
    {
        instance = this;

        LoadData();
        UpdateData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Coin", Coin);
        PlayerPrefs.SetInt("Highest Enemies Killed", HighestEnemiesKilled);
        PlayerPrefs.SetFloat("Highest Survival Time", HighestSurvivalTime);
    }

    public void LoadData()
    {
        Coin = PlayerPrefs.GetInt("Coin", 0);
        HighestEnemiesKilled = PlayerPrefs.GetInt("Highest Enemies Killed", 0);
        HighestSurvivalTime = PlayerPrefs.GetFloat("Highest Survival Time", 0);
    }

    public void UpdateData()
    {
        UpdateUsername();
        UpdateCoin();
        UpdateEnemiesKilled();
        UpdateSurvivalTime();
    }

    private void UpdateUsername()
    {
        UserName = DataManager.Instance.Username;
    }

    private void UpdateCoin()
    {
        Coin += DataManager.Instance.AchievementDataManager.coin;
    }

    private void UpdateEnemiesKilled()
    {
        EnemiesKilled = DataManager.Instance.AchievementDataManager.enemiesKilled;

        if (HighestEnemiesKilled >= DataManager.Instance.AchievementDataManager.enemiesKilled) return;

        HighestEnemiesKilled = DataManager.Instance.AchievementDataManager.enemiesKilled;
        isAchievementUpdated = true;
    }

    private void UpdateSurvivalTime()
    {
        SurvivalTime = DataManager.Instance.AchievementDataManager.survivalTime;

        if (HighestSurvivalTime >= DataManager.Instance.AchievementDataManager.survivalTime) return;

        HighestSurvivalTime = DataManager.Instance.AchievementDataManager.survivalTime;
        isAchievementUpdated = true;
    }

    public void ConsumeCoin(int consumptionQuantity)
    {
        Coin -= consumptionQuantity;
    }

    private void OnDisable()
    {
        SaveData();
    }
}
