using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Spawner
{
    #region public var
    public bool canSpawn { get => _canSpawn; set => _canSpawn = value; }
    #endregion

    #region private var
    [SerializeField] private bool _canSpawn;

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
        parent = transform.parent;
    }

    private void Update()
    {
        GetObjFromList();
    }

    protected override void Spawn()
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
        base.UpdateListGameObj();
    }

    protected override bool CanSpawn()
    {
        return _canSpawn;
    }
}
