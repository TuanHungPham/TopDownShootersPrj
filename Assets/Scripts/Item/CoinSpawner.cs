using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Spawner
{
    #region public var
    public bool CanDrop { get => canDrop; set => canDrop = value; }
    #endregion

    #region private var
    [SerializeField] private bool canDrop;

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
        listOfObj = transform.root.Find("CoinStorage").GetComponent<ListOfObj>();
        parent = transform;
    }

    private void Update()
    {
        Spawn();
        UpdateListGameObj();
        ClearActiveList();
    }

    public override void Spawn()
    {
        base.Spawn();
    }

    public void GetSpawnPos(Transform position)
    {
        spawnPos = position;
    }

    protected override void GetObjFromList()
    {
        base.GetObjFromList();
    }

    protected override GameObject RandomGameObj()
    {
        return base.RandomGameObj();
    }

    protected override GameObject NewGameObj(GameObject obj)
    {
        return base.NewGameObj(obj);
    }

    protected override void SetActiveObj()
    {
        base.SetActiveObj();
    }

    protected override void UpdateListGameObj()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf && !listOfInactiveObj.Contains(child.gameObject))
            {
                listOfInactiveObj.Add(child.gameObject);
            }
        }
    }

    private void ClearActiveList()
    {
        if (!EnemyWaveManager.Instance.UpdateWave) return;

        listOfActiveObj.Clear();
        maxObj = 0;
    }

    protected override bool CanSpawn()
    {
        if (canDrop && listOfActiveObj.Count < maxObj) return true;

        return false;
    }
}
