using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using System;

public class EnemyWaveManager : MonoBehaviour
{
    private static EnemyWaveManager instance;
    public static EnemyWaveManager Instance { get => instance; set => instance = value; }

    #region public var
    public bool UpdateWave { get => updateWave; set => updateWave = value; }
    public bool IsEndWave { get => isEndWave; set => isEndWave = value; }
    public int NumberOfEnemy { get => numberOfEnemy; set => numberOfEnemy = value; }
    public int RestOfEnemy { get => RestOfEnemy1; set => RestOfEnemy1 = value; }
    public int WaveNumber { get => WaveNumber1; set => WaveNumber1 = value; }
    public float NextWaveTimer { get => nextWaveTimer; set => nextWaveTimer = value; }
    public int RestOfEnemy1 { get => restOfEnemy; set => restOfEnemy = value; }
    public int WaveNumber1 { get => waveNumber; set => waveNumber = value; }
    #endregion

    #region private var
    [SerializeField] private int waveNumber;
    [SerializeField] private int numberOfEnemy;
    [SerializeField] private int restOfEnemy;
    [SerializeField] private int addEnemyNumber;
    [SerializeField] private int addEnemyHP;
    [SerializeField] private float nextWaveTimer;
    [SerializeField] private bool isEndWave;
    [SerializeField] private bool updateWave;

    [Space(20)]
    [SerializeField] private ListOfObj listOfEnemy;
    [SerializeField] private EnemySpawner enemySpawner;
    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        listOfEnemy = transform.root.Find("ListOfEnemy").GetComponent<ListOfObj>();
        enemySpawner = transform.root.Find("EnemySpawner").GetComponent<EnemySpawner>();

        WaveNumber = 1;
        NumberOfEnemy = 10;
        NextWaveTimer = 15;
        addEnemyNumber = 5;
        addEnemyHP = 5;
        RestOfEnemy = NumberOfEnemy;
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.ENEMY_DEATH.ToString(), DecreaseRestOfEnemyNumber);
    }

    private void DecreaseRestOfEnemyNumber()
    {
        restOfEnemy--;
    }

    private void Update()
    {
        CheckNumberOfAliveEnemy();
        CheckWave();
        SetUpForNextWave();
    }

    private void SetUpForNextWave()
    {
        if (!IsEndWave)
        {
            UpdateWave = false;
            NextWaveTimer = 15;
            return;
        }

        UpdateWave = true;

        if (UpdateWave)
        {
            CheckNextWaveTimer();
            StopSpawnEnemy();
            if (NextWaveTimer > 0) return;

            AddEnemyNumber();
            AddEnemyHP(listOfEnemy.listOfObj);
            AddEnemyHP(enemySpawner.listOfInactiveObj);
            WaveNumber++;
            UpdateWave = false;
            IsEndWave = false;
        }
    }

    private void CheckNextWaveTimer()
    {
        NextWaveTimer -= Time.deltaTime;
    }

    private void AddEnemyHP(List<GameObject> list)
    {
        foreach (GameObject enemy in list)
        {
            EnemyCtrl enemyCtrl = enemy.GetComponent<EnemyCtrl>();
            enemyCtrl.EnemyStatus.MaxHP += addEnemyHP;
        }
    }

    private void CheckNumberOfAliveEnemy()
    {
        enemySpawner.MaxObj = RestOfEnemy;
    }

    private void AddEnemyNumber()
    {
        NumberOfEnemy += addEnemyNumber;
        RestOfEnemy = NumberOfEnemy;
    }

    private void StopSpawnEnemy()
    {
        enemySpawner.MaxObj = 0;
    }

    private void CheckWave()
    {
        if (RestOfEnemy <= 0)
        {
            RestOfEnemy = 0;
            IsEndWave = true;
            return;
        }

        IsEndWave = false;
    }
}
