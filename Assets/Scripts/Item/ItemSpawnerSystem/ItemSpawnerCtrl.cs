using UnityEngine;

public class ItemSpawnerCtrl : MonoBehaviour
{
    private static ItemSpawnerCtrl instance;
    [SerializeField] public static ItemSpawnerCtrl Instance { get => instance; }

    #region public var
    public ItemSpanwer CoinSpawner { get => coinSpawner; set => coinSpawner = value; }
    public MagazineSpawner MagazineSpawner { get => magazineSpawner; set => magazineSpawner = value; }
    public WeaponSpawner WeaponSpawner { get => weaponSpawner; set => weaponSpawner = value; }
    public ConsumpItemSpawner ConsumpItemSpawner { get => consumpItemSpawner; set => consumpItemSpawner = value; }
    #endregion

    #region private var
    [SerializeField] private ItemSpanwer coinSpawner;
    [SerializeField] private MagazineSpawner magazineSpawner;
    [SerializeField] private WeaponSpawner weaponSpawner;
    [SerializeField] private ConsumpItemSpawner consumpItemSpawner;
    #endregion

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
        CoinSpawner = GetComponentInChildren<ItemSpanwer>();
        MagazineSpawner = GetComponentInChildren<MagazineSpawner>();
        WeaponSpawner = GetComponentInChildren<WeaponSpawner>();
        ConsumpItemSpawner = GetComponentInChildren<ConsumpItemSpawner>();
    }
}