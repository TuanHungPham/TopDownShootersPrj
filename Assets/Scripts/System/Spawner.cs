using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    #region public var
    public int maxObj;
    public float spawnDelay;
    public float spawnTimer;
    #endregion

    #region private var
    [SerializeField] protected GameObject gameObj;
    [SerializeField] protected SpawnPoint spawnPointScript;
    [SerializeField] private List<GameObject> listOfActiveObj = new List<GameObject>();
    [SerializeField] private List<GameObject> listOfInactiveObj = new List<GameObject>();
    private bool isSpawnCooldown;
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
        gameObj = Resources.Load<GameObject>("Prefabs/Enemy1");

        spawnPointScript = GameObject.Find("------ OTHER ------").transform.Find("SpawnPoint").GetComponent<SpawnPoint>();

        spawnTimer = spawnDelay;
    }

    private void Update()
    {
        UpdateListOfInactiveObj();
        CheckSpawnTime();
        Spawn();
    }

    private void Spawn()
    {
        if (!CanSpawn()) return;

        GameObject enemy;
        if (listOfInactiveObj.Count > 0)
        {
            enemy = RandomGameObj();
            listOfInactiveObj.Remove(enemy);
            enemy.SetActive(true);
        }
        else
        {
            enemy = NewGameObj(gameObj);
        }

        enemy.transform.position = spawnPointScript.spawnPointSelected.position;
        enemy.transform.rotation = spawnPointScript.spawnPointSelected.rotation;
        enemy.transform.parent = GameObject.Find("EnemySpawner").transform;

        spawnTimer = spawnDelay;
    }

    private GameObject NewGameObj(GameObject obj)
    {
        GameObject newGameObj = Instantiate(obj);
        return newGameObj;
    }

    private GameObject RandomGameObj()
    {
        int index = Random.Range(0, listOfInactiveObj.Count);
        return listOfInactiveObj[index];
    }

    private void CheckSpawnTime()
    {
        if (spawnTimer <= 0)
        {
            isSpawnCooldown = false;
            return;
        }

        isSpawnCooldown = true;
        spawnTimer -= Time.deltaTime;
    }

    private bool CanSpawn()
    {
        if (!isSpawnCooldown && listOfActiveObj.Count < maxObj) return true;

        return false;
    }

    private void UpdateListOfInactiveObj()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf || listOfInactiveObj.Contains(child.gameObject)) continue;

            listOfInactiveObj.Add(child.gameObject);
        }
    }
}
