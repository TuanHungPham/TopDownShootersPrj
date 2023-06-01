using UnityEngine;

public class ItemDropSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private int coinDropQuantity;
    [SerializeField] private float coinDropRate;
    [SerializeField] private float magazineDropRate;
    [SerializeField] private float weaponDropRate;
    [SerializeField] private float potionDropRate;
    [SerializeField] private bool isDrop;

    [Space(20)]
    [SerializeField] private Transform dropPos;
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
        DropItem(coinDropRate, ItemSpawnerCtrl.Instance.CoinSpawner, coinDropQuantity);
        DropItem(magazineDropRate, ItemSpawnerCtrl.Instance.MagazineSpawner, 1);
        DropItem(weaponDropRate, ItemSpawnerCtrl.Instance.WeaponSpawner, 1);
        DropItem(potionDropRate, ItemSpawnerCtrl.Instance.ConsumpItemSpawner, 1);

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
        itemSpawner.MaxObj += itemDropQuantity;
        itemSpawner.CanDrop = true;
    }
}
