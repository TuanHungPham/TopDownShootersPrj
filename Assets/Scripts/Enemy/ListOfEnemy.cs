using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfEnemy : MonoBehaviour
{
    #region public var
    public GameObject selectedEnemy;
    public List<GameObject> listOfEnemies = new List<GameObject>();
    #endregion

    #region private var
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
        InitializeListOfEnemy();
    }

    private void Update()
    {
        GetRandomObjFromList();
    }

    private void InitializeListOfEnemy()
    {
        foreach (Transform item in transform)
        {
            if (listOfEnemies.Contains(item.gameObject)) continue;

            listOfEnemies.Add(item.gameObject);
        }
    }

    private void GetRandomObjFromList()
    {
        int index = Random.Range(0, listOfEnemies.Count);
        selectedEnemy = listOfEnemies[index];
    }
}
