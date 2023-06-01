using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    #region public var
    public bool AmmoLeft { get => ammoLeft; set => ammoLeft = value; }
    public int RifleAmmo { get => rifleAmmo; set => rifleAmmo = value; }
    public int PistolAmmo { get => pistolAmmo; set => pistolAmmo = value; }
    public int CurrentWeaponAmmo { get => currentWeaponAmmo; set => currentWeaponAmmo = value; }
    public AmmoType CurrentUsingAmmoType { get => currentUsingAmmoType; set => currentUsingAmmoType = value; }
    #endregion

    #region private var
    [SerializeField] private int currentWeaponAmmo;
    [SerializeField] private int rifleAmmo;
    [SerializeField] private int pistolAmmo;
    [SerializeField] private AmmoType currentUsingAmmoType;
    [SerializeField] private bool ammoLeft;

    [Space(20)]
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private UIManager uIManager;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        CurrentWeaponAmmo = RifleAmmo;
    }

    private void LoadComponents()
    {
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        uIManager = GameObject.Find("------ UI ------").GetComponentInChildren<UIManager>();
    }

    private void Update()
    {
        SwapAmmoType();
        CheckAmmo();
    }

    private void SwapAmmoType()
    {
        if (!uIManager.WeaponInventoryPanel.IsWeaponSwitched) return;
        WeaponHolderUI holderSelected = uIManager.WeaponInventoryPanel.WeaponHolderSelected();

        if (holderSelected.HolderType == HolderType.PRIMARY_HOLDER)
        {
            CurrentUsingAmmoType = AmmoType.RIFLE_AMMO;
        }
        else if (holderSelected.HolderType == HolderType.SECONDARY_HOLDER)
        {
            CurrentUsingAmmoType = AmmoType.PISTOL_AMMO;
        }

        RenewAmmoCapacity();
    }

    public void ConsumpAmmo()
    {
        CurrentWeaponAmmo--;
        UseAmmo();
    }

    public void AddAmmo(AmmoType ammoType, int ammoQuantity)
    {
        if (ammoType == AmmoType.RIFLE_AMMO)
        {
            RifleAmmo += ammoQuantity;
        }
        else if (ammoType == AmmoType.PISTOL_AMMO)
        {
            PistolAmmo += ammoQuantity;
        }

        RenewAmmoCapacity();
    }

    private void RenewAmmoCapacity()
    {
        if (CurrentUsingAmmoType == AmmoType.RIFLE_AMMO)
        {
            CurrentWeaponAmmo = RifleAmmo;
        }
        else if (CurrentUsingAmmoType == AmmoType.PISTOL_AMMO)
        {
            CurrentWeaponAmmo = PistolAmmo;
        }
    }

    private void UseAmmo()
    {
        if (CurrentUsingAmmoType == AmmoType.RIFLE_AMMO)
        {
            RifleAmmo = CurrentWeaponAmmo;
        }
        else if (CurrentUsingAmmoType == AmmoType.PISTOL_AMMO)
        {
            PistolAmmo = CurrentWeaponAmmo;
        }
    }

    private void CheckAmmo()
    {
        if (CurrentWeaponAmmo <= 0)
        {
            ammoLeft = false;
            return;
        }

        ammoLeft = true;
    }
}
