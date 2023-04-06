using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private SpawnPoint spawnPointScript;
    [SerializeField] private bool isSpawnCooldown;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void LoadComponents()
    {
        listOfObj = transform.root.Find("ListOfEnemy").GetComponent<ListOfObj>();

        spawnPointScript = GameObject.Find("------ OTHER ------").transform.Find("SpawnPoint").GetComponent<SpawnPoint>();

        parent = transform;
        spawnTimer = spawnDelay;
        maxObj = EnemyWaveManager.Instance.numberOfEnemy;
    }

    protected void Update()
    {
        CheckSpawnTime();
        GetSpawnPosition();
        Spawn();
        SetActiveObj();
        UpdateListGameObj();
    }

    protected override void GetObjFromList()
    {
        base.GetObjFromList();
    }

    public override void Spawn()
    {
        base.Spawn();
    }

    protected override void SetActiveObj()
    {
        base.SetActiveObj();
    }

    protected override GameObject NewGameObj(GameObject obj)
    {
        return base.NewGameObj(obj);
    }

    protected override GameObject RandomGameObj()
    {
        return base.RandomGameObj();
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

    protected override bool CanSpawn()
    {
        if (!isSpawnCooldown && listOfActiveObj.Count < maxObj) return true;

        return false;
    }

    protected override void UpdateListGameObj()
    {
        foreach (Transform child in transform)
        {
            Status status = child.GetComponent<Status>();

            if (child.gameObject.activeSelf && !listOfActiveObj.Contains(child.gameObject) && !status.IsDeath)
            {
                listOfActiveObj.Add(child.gameObject);
            }
            else if (listOfActiveObj.Contains(child.gameObject) && status.IsDeath)
            {
                listOfActiveObj.Remove(child.gameObject);
            }
            else if (!child.gameObject.activeSelf && !listOfInactiveObj.Contains(child.gameObject))
            {
                listOfInactiveObj.Add(child.gameObject);
            }
        }
    }

    private void GetSpawnPosition()
    {
        spawnPos = spawnPointScript.spawnPointSelected;
    }
}
