using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private static EnemyWaveManager instance;
    public static EnemyWaveManager Instance { get => instance; set => instance = value; }

    #region public var
    public int waveNumber;
    public int numberOfEnemy;
    public int restOfEnemy;
    public float nextWaveTimer;
    #endregion

    #region private var
    [SerializeField] private int addEnemyNumber;
    [SerializeField] private int addEnemyHP;
    [SerializeField] private int addEnemyDmg;
    [SerializeField] private ListOfEnemy listOfEnemy;
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
        listOfEnemy = transform.root.Find("ListOfEnemy").GetComponent<ListOfEnemy>();
        enemySpawner = transform.root.Find("EnemySpawner").GetComponent<EnemySpawner>();

        waveNumber = 1;
        numberOfEnemy = 10;
        nextWaveTimer = 60;
        addEnemyNumber = 5;
        addEnemyHP = 25;
        addEnemyDmg = 10;
        restOfEnemy = numberOfEnemy;
    }

    private void Update()
    {
        
    }

    private void CheckNextWaveTimer()
    {
        nextWaveTimer -= Time.deltaTime;
    }

    private void AddEnemyStatus()
    {
        foreach (GameObject item in listOfEnemy.listOfEnemies)
        {

        }
    }

    private void AddEnemyNumber()
    {

    }
}
