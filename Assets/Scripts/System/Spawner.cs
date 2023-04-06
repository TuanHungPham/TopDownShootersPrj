using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Spawner : MonoBehaviour
{
    #region public var
    public int maxObj;
    public float spawnDelay;
    public float spawnTimer;
    public List<GameObject> listOfActiveObj = new List<GameObject>();
    public List<GameObject> listOfInactiveObj = new List<GameObject>();
    #endregion

    #region private var
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
        gameObj = listOfObj.selectedObj;
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

        SetActiveObj();
        obj.transform.position = spawnPos.position;
        obj.transform.rotation = spawnPos.rotation;
        obj.transform.parent = parent;

        spawnTimer = spawnDelay;
    }

    protected virtual void SetActiveObj()
    {
        foreach (var item in listOfActiveObj)
        {
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
