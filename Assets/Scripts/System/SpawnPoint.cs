using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    #region public var
    public Transform SpawnPointSelected { get => spawnPointSelected; set => spawnPointSelected = value; }
    #endregion

    #region private var
    [SerializeField] private Transform spawnPointSelected;
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
            if (listOfSpawnPoint.Contains(child)) continue;

            listOfSpawnPoint.Add(child);
        }
    }

    private void GetRandomSpawnPoint()
    {
        int index = Random.Range(0, listOfSpawnPoint.Count);

        SpawnPointSelected = listOfSpawnPoint[index];
    }
}
