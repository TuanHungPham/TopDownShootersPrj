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
    #endregion

    #region private var
    [SerializeField] protected GameObject gameObj;
    [SerializeField] protected SpawnPoint spawnPointScript;
    [SerializeField] protected Transform spawnPos;
    [SerializeField] protected Transform parent;
    [SerializeField] protected List<GameObject> listOfActiveObj = new List<GameObject>();
    [SerializeField] protected List<GameObject> listOfInactiveObj = new List<GameObject>();
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

    protected virtual void Spawn()
    {
        if (!CanSpawn()) return;

        GameObject obj;
        if (listOfInactiveObj.Count > 0)
        {
            obj = RandomGameObj();
            listOfInactiveObj.Remove(obj);
            obj.SetActive(true);
        }
        else
        {
            obj = NewGameObj(gameObj);
            obj.SetActive(true);
        }

        obj.transform.position = spawnPos.position;
        obj.transform.rotation = spawnPos.rotation;
        obj.transform.parent = parent;

        spawnTimer = spawnDelay;
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

    protected virtual void UpdateListGameObj()
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
}
