using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private static EnemyWaveManager instance;
    public static EnemyWaveManager Instance { get => instance; set => instance = value; }
    public bool UpdateWave { get => updateWave; set => updateWave = value; }

    #region public var
    public int waveNumber;
    public int numberOfEnemy;
    public int restOfEnemy;
    public float nextWaveTimer;
    #endregion

    #region private var
    [SerializeField] private int addEnemyNumber;
    [SerializeField] private int addEnemyHP;
    [SerializeField] private ListOfObj listOfEnemy;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private bool isEndWave;
    [SerializeField] private bool updateWave;
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

        waveNumber = 1;
        numberOfEnemy = 10;
        nextWaveTimer = 15;
        addEnemyNumber = 5;
        addEnemyHP = 25;
        restOfEnemy = numberOfEnemy;
    }

    private void Update()
    {
        CheckNumberOfAliveEnemy();
        CheckWave();
        SetUpForNextWave();
    }

    private void SetUpForNextWave()
    {
        if (!isEndWave)
        {
            UpdateWave = false;
            nextWaveTimer = 15;
            return;
        }

        UpdateWave = true;

        if (UpdateWave)
        {
            CheckNextWaveTimer();
            StopSpawnEnemy();
            if (nextWaveTimer > 0) return;

            AddEnemyNumber();
            AddEnemyHP(listOfEnemy.listOfObj);
            AddEnemyHP(enemySpawner.listOfInactiveObj);
            waveNumber++;
            UpdateWave = false;
            isEndWave = false;
        }
    }

    private void CheckNextWaveTimer()
    {
        nextWaveTimer -= Time.deltaTime;
    }

    private void AddEnemyHP(List<GameObject> list)
    {
        foreach (GameObject enemy in list)
        {
            EnemyCtrl enemyCtrl = enemy.GetComponent<EnemyCtrl>();
            enemyCtrl.enemyStatus.maxHP += addEnemyHP;
        }
    }

    private void CheckNumberOfAliveEnemy()
    {
        enemySpawner.maxObj = restOfEnemy;
    }

    private void AddEnemyNumber()
    {
        numberOfEnemy += addEnemyNumber;
        restOfEnemy = numberOfEnemy;
    }

    private void StopSpawnEnemy()
    {
        enemySpawner.maxObj = 0;
    }

    private void CheckWave()
    {
        if (restOfEnemy <= 0)
        {
            isEndWave = true;
            return;
        }

        isEndWave = false;
    }
}
