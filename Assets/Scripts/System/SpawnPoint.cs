using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    #region public var
    public Transform spawnPointSelected;
    #endregion

    #region private var
    [SerializeField] private List<Transform> listOfSpawnPoint = new List<Transform>();
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
        InitializeList();
    }

    private void Update()
    {
        GetRandomSpawnPoint();
    }

    private void InitializeList()
    {
        foreach (Transform child in transform)
        {
            listOfSpawnPoint.Add(child);
        }
    }

    private void GetRandomSpawnPoint()
    {
        int index = Random.Range(0, listOfSpawnPoint.Count);

        spawnPointSelected = listOfSpawnPoint[index];
    }
}
