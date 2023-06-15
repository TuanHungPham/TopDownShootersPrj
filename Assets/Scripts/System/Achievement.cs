using TigerForge;
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

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.ENEMY_DEATH.ToString(), PlusEnemyKilledNumber);
        EventManager.StartListening(EventID.GAME_OVER.ToString(), UpdateToMainData);
    }

    private void Update()
    {
        TimeCounter();
    }

    private void PlusEnemyKilledNumber()
    {
        enemiesKilled++;
    }

    private void TimeCounter()
    {
        survivalTime += Time.deltaTime;
    }

    public void UpdateToMainData()
    {
        UpdateCoinToMainData();
        UpdateKilledScoreToMainData();
        UpdateTimeScoreToMainData();
    }

    private void UpdateCoinToMainData()
    {
        DataManager.Instance.AchievementDataManager.AddCoin(totalMoney);
    }

    private void UpdateKilledScoreToMainData()
    {
        if (enemiesKilled <= DataManager.Instance.AchievementDataManager.HighestEnemiesKilled) return;

        DataManager.Instance.AchievementDataManager.HighestEnemiesKilled = enemiesKilled;
    }

    private void UpdateTimeScoreToMainData()
    {
        if (survivalTime <= DataManager.Instance.AchievementDataManager.HighestSurvivalTime) return;

        DataManager.Instance.AchievementDataManager.HighestSurvivalTime = survivalTime;
    }
}
