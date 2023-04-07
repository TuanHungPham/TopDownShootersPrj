using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : PickUpSystem
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private int coinQuantity;
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
    }

    private void Update()
    {
        GetRandomCoinQuantity();
    }

    protected override void AddItemToPlayerInventory()
    {
        Achievement.Instance.totalMoney += coinQuantity;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    private void GetRandomCoinQuantity()
    {
        coinQuantity = Random.Range(10, 30);
    }
}