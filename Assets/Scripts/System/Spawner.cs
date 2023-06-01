using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [Space(20)]
    [SerializeField] protected ListOfObj listOfObj;
    [SerializeField] protected GameObject gameObj;
    [SerializeField] protected Transform spawnPos;
    [SerializeField] protected Transform parent;

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
        gameObj = listOfObj.SelectedObj;
    }

    public virtual void Spawn()
    {
        if (!CanSpawn()) return;

        GameObject obj;
        if (listOfInactiveObj.Count > 0)
        {
            obj = RandomGameObj();
            listOfInactiveObj.Remove(obj);
            listOfActiveObj.Add(obj);
        }
        else
        {
            GetObjFromList();
            obj = NewGameObj(gameObj);
            listOfActiveObj.Add(obj);
        }

        obj.transform.position = spawnPos.position;
        obj.transform.rotation = spawnPos.rotation;
        obj.transform.parent = parent;
        obj.SetActive(true);

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

    protected virtual GameObject NewGameObj(GameObject obj)
    {
        GameObject newGameObj = Instantiate(obj);
        return newGameObj;
    }

    protected virtual GameObject RandomGameObj()
    {
        int index = Random.Range(0, listOfInactiveObj.Count);
        return listOfInactiveObj[index];
    }

    protected abstract bool CanSpawn();

    protected abstract void UpdateListGameObj();
}
