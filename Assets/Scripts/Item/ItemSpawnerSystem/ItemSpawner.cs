using UnityEngine;

public class ItemSpanwer : Spawner
{
    #region public var
    public bool CanDrop { get => canDrop; set => canDrop = value; }
    #endregion

    #region private var
    [SerializeField] protected bool canDrop;
    [SerializeField] private Vector3 checkPos;
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
        Spawn();
        UpdateListGameObj();
        ClearActiveList();
    }

    public override void Spawn()
    {
        SetRandomDropPos();
        base.Spawn();
    }

    public void SetRandomDropPos()
    {
        if (!CanSpawn()) return;

        Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        spawnPos.position = spawnPos.position + randomPos;
        checkPos = spawnPos.position;
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
        MaxObj = 0;
    }

    protected override bool CanSpawn()
    {
        if (canDrop && listOfActiveObj.Count < MaxObj) return true;

        return false;
    }
}
