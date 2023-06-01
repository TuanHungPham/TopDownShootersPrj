using UnityEngine;

public class Achievement : MonoBehaviour
{
    private static Achievement instance;
    public static Achievement Instance { get => instance; }

    #region public var
    public int EnemiesKilled { get => enemiesKilled; set => enemiesKilled = value; }
    public float SurvivalTime { get => survivalTime; set => survivalTime = value; }
    public int TotalDmg { get => totalDmg; set => totalDmg = value; }
    public int TotalMoney { get => totalMoney; set => totalMoney = value; }
    #endregion

    #region private var
    [SerializeField] private int enemiesKilled;
    [SerializeField] private float survivalTime;
    [SerializeField] private int totalDmg;
    [SerializeField] private int totalMoney;
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
        UpdateToMainData();
    }

    private void TimeCounter()
    {
        SurvivalTime += Time.deltaTime;
    }

    public void UpdateToMainData()
    {
        DataManager.Instance.AchievementDataManager.enemiesKilled = EnemiesKilled;
        SurvivalTime = DataManager.Instance.AchievementDataManager.survivalTime = SurvivalTime;
        TotalMoney = DataManager.Instance.AchievementDataManager.coin = TotalMoney;
    }

    public void SaveDataWhenRetry()
    {
        int highestEnemiesKilled = PlayerPrefs.GetInt("Highest Enemies Killed", 0);
        float highestSurvivalTime = PlayerPrefs.GetFloat("Highest Survival Time", 0);
        int coin = PlayerPrefs.GetInt("Coin", 0);

        if (highestEnemiesKilled < EnemiesKilled)
        {
            highestEnemiesKilled = EnemiesKilled;
        }

        if (highestSurvivalTime < SurvivalTime)
        {
            highestSurvivalTime = SurvivalTime;
        }

        coin += TotalMoney;

        SaveDataIngame(highestEnemiesKilled, highestSurvivalTime, coin);
    }

    private static void SaveDataIngame(int highestEnemiesKilled, float highestSurvivalTime, int coin)
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("Highest Enemies Killed", highestEnemiesKilled);
        PlayerPrefs.SetFloat("Highest Survival Time", highestSurvivalTime);
    }
}
