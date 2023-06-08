using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public abstract class Spawner : MonoBehaviour
{
    #region public var
    public List<GameObject> listOfActiveObj = new List<GameObject>();
    public List<GameObject> listOfInactiveObj = new List<GameObject>();
    public float SpawnTimer { get => spawnTimer; set => spawnTimer = value; }
    public float SpawnDelay { get => spawnDelay; set => spawnDelay = value; }
    public int MaxObj { get => maxObj; set => maxObj = value; }
    #endregion

    #region private var
    [SerializeField] private int maxObj;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnTimer;
    [SerializeField] protected string poolName;

    [Space(20)]
    [SerializeField] protected ListOfObj listOfObj;
    [SerializeField] protected GameObject gameObj;
    [SerializeField] protected Transform spawnPos;
    #endregion

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected abstract void LoadComponents();

    protected virtual void GetObjFromList()
    {
        if (listOfObj.SelectedObj == null) return;

        gameObj = listOfObj.SelectedObj;
        poolName = gameObj.name;
    }

    public virtual void Spawn()
    {
        if (!CanSpawn()) return;

        GameObject obj = EasyObjectPool.instance.GetObjectFromPool(poolName, spawnPos.position, spawnPos.rotation);
        listOfActiveObj.Add(obj);

        SpawnTimer = SpawnDelay;
    }

    protected virtual void SetActiveObj()
    {
        foreach (var item in listOfActiveObj)
        {
            if (item.activeSelf) continue;

            item.SetActive(true);
        }
    }

    protected abstract bool CanSpawn();

    protected abstract void UpdateListGameObj();
}
