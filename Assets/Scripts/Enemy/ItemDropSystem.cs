using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSystem : MonoBehaviour
{
    #region public var
    public int coinDropQuantity;
    public float coinDropRate;
    public float magazineDropRate;
    public Transform dropPos;
    #endregion

    #region private var
    [SerializeField] private bool isDrop;
    #endregion

    private void OnEnable()
    {
        isDrop = false;
    }

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
        coinDropRate = 0.8f;
        magazineDropRate = 0.3f;
    }

    private void Update()
    {
        GetRandomItemDropQuantity();
    }

    private void GetRandomDropPosition()
    {
        Vector3 randomPosAround = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        dropPos.position = transform.parent.position + randomPosAround;
    }

    private void GetRandomItemDropQuantity()
    {
        coinDropQuantity = Random.Range(1, 7);
    }

    public void DropItem()
    {
        DropItem(coinDropRate, ItemSpawnerCtrl.Instance.coinSpawner, coinDropQuantity);
        DropItem(magazineDropRate, ItemSpawnerCtrl.Instance.magazineSpawner, 1);

        isDrop = true;
    }

    private void DropCoin()
    {
        if (isDrop || Random.value > coinDropRate)
        {
            ItemSpawnerCtrl.Instance.coinSpawner.CanDrop = false;
            return;
        }

        ItemSpawnerCtrl.Instance.coinSpawner.GetSpawnPos(dropPos);
        ItemSpawnerCtrl.Instance.coinSpawner.maxObj += coinDropQuantity;
        ItemSpawnerCtrl.Instance.coinSpawner.CanDrop = true;
        isDrop = true;
    }

    private void DropMagazine()
    {
        if (isDrop || Random.value > magazineDropRate)
        {
            ItemSpawnerCtrl.Instance.magazineSpawner.CanDrop = false;
            return;
        }

        ItemSpawnerCtrl.Instance.magazineSpawner.GetSpawnPos(dropPos);
        ItemSpawnerCtrl.Instance.magazineSpawner.maxObj += 1;
        ItemSpawnerCtrl.Instance.magazineSpawner.CanDrop = true;
        isDrop = true;
    }

    private void DropItem(float dropRate, ItemSpanwer itemSpawner, int itemDropQuantity)
    {
        float dropChance = Random.value;
        Debug.Log(dropChance);
        if (isDrop || dropChance > dropRate)
        {
            itemSpawner.CanDrop = false;
            return;
        }

        itemSpawner.GetSpawnPos(dropPos);
        itemSpawner.maxObj += itemDropQuantity;
        itemSpawner.CanDrop = true;
    }
}
