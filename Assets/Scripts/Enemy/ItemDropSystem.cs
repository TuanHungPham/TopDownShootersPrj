using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSystem : MonoBehaviour
{
    #region public var
    public int coinDropQuantity;
    public float coinDropRate;
    public float magazineDropRate;
    public float weaponDropRate;
    public float potionDropRate;
    public Transform dropPos;
    #endregion

    #region private var
    [SerializeField] private bool isDrop;
    private float dropChance;
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
        weaponDropRate = 0.1f;
        potionDropRate = 0.25f;
    }

    private void Update()
    {
        GetRandomItemDropQuantity();
    }

    private void GetRandomItemDropQuantity()
    {
        coinDropQuantity = Random.Range(1, 7);
    }

    public void DropItem()
    {
        DropItem(coinDropRate, ItemSpawnerCtrl.Instance.coinSpawner, coinDropQuantity);
        DropItem(magazineDropRate, ItemSpawnerCtrl.Instance.magazineSpawner, 1);
        DropItem(weaponDropRate, ItemSpawnerCtrl.Instance.weaponSpawner, 1);
        DropItem(potionDropRate, ItemSpawnerCtrl.Instance.consumpItemSpawner, 1);

        isDrop = true;
    }

    private void DropItem(float dropRate, ItemSpanwer itemSpawner, int itemDropQuantity)
    {
        dropPos = transform.parent;

        dropChance = Random.value;
        if (isDrop || dropChance > dropRate)
        {
            itemSpawner.CanDrop = false;
            return;
        }

        itemSpawner.SetSpawnPos(dropPos);
        itemSpawner.maxObj += itemDropQuantity;
        itemSpawner.CanDrop = true;
    }
}
