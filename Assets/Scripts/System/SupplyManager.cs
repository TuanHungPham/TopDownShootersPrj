using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    #region public var
    public List<GameObject> supplyBox = new List<GameObject>();
    #endregion

    #region private var
    [SerializeField] private ListOfObj listOfObj;
    [SerializeField] private Transform weaponSpawnerPool;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private bool canSupply;
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
        listOfObj = transform.root.Find("WeaponDropStorage").GetComponent<ListOfObj>();

        weaponSpawnerPool = transform.root.Find("ItemSpawner").Find("WeaponSpawner");
        pointA = transform.Find("PointA");
        pointB = transform.Find("PointB");
    }

    private void Update()
    {
        CheckSupplySpawn();
        SpawnSupply();
    }

    private void SpawnSupply()
    {
        if (!canSupply || supplyBox.Count >= 2) return;

        for (int i = 0; i < 2; i++)
        {
            int index = Random.Range(0, listOfObj.listOfObj.Count);

            GameObject weaponSupply = Instantiate(listOfObj.listOfObj[index]);
            weaponSupply.transform.SetParent(weaponSpawnerPool);
            weaponSupply.SetActive(true);

            supplyBox.Add(weaponSupply);

            if (i == 0)
            {
                weaponSupply.transform.position = pointA.position;
                continue;
            }

            weaponSupply.transform.position = pointB.position;
        }
    }

    private void CheckSupplySpawn()
    {
        if (EnemyWaveManager.Instance.waveNumber % 5 != 0 || EnemyWaveManager.Instance.nextWaveTimer <= 0 || !EnemyWaveManager.Instance.IsEndWave)
        {
            supplyBox.Clear();
            canSupply = false;
            return;
        }

        canSupply = true;
    }
}
