using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerCtrl : MonoBehaviour
{
    private static ItemSpawnerCtrl instance; 
    public static ItemSpawnerCtrl Instance { get => instance; }

    public CoinSpawner coinSpawner;

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();   
    }

    private void LoadComponents()
    {
        coinSpawner = GetComponentInChildren<CoinSpawner>();
    }
}
