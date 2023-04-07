using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpanwer : Spawner
{
    #region public var
    public bool CanDrop { get => canDrop; set => canDrop = value; }
    #endregion

    #region private var
    [SerializeField] protected bool canDrop;
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
        parent = transform;
    }

    private void Update()
    {
        SetRandomDropPos();
        Spawn();
        UpdateListGameObj();
        ClearActiveList();
    }

    public override void Spawn()
    {
        base.Spawn();
    }

    private void SetRandomDropPos()
    {
        if (spawnPos == null) return;

        Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        this.spawnPos.position = this.spawnPos.position + randomPos;
    }

    public virtual void SetSpawnPos(Transform position)
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

    protected virtual void ClearActiveList()
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
