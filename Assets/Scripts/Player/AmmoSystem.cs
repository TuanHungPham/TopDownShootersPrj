using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    #region public var
    public int currentWeaponAmmo;
    public int rifleAmmo;
    public int pistolAmmo;
    public AmmoType currentUsingAmmoType;
    public bool AmmoLeft { get => ammoLeft; set => ammoLeft = value; }
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private bool ammoLeft;
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
        currentWeaponAmmo = rifleAmmo;
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
        if (!uIManager.weaponInventoryPanel.IsWeaponSwitched) return;
        WeaponHolderUI holderSelected = uIManager.weaponInventoryPanel.WeaponHolderSelected();

        if (holderSelected.holderType == HolderType.PRIMARY_HOLDER)
        {
            currentUsingAmmoType = AmmoType.RIFLE_AMMO;
        }
        else if (holderSelected.holderType == HolderType.SECONDARY_HOLDER)
        {
            currentUsingAmmoType = AmmoType.PISTOL_AMMO;
        }

        RenewAmmoCapacity();
    }

    public void ConsumpAmmo()
    {
        currentWeaponAmmo--;
        UseAmmo();
    }

    public void AddAmmo(AmmoType ammoType, int ammoQuantity)
    {
        if (ammoType == AmmoType.RIFLE_AMMO)
        {
            rifleAmmo += ammoQuantity;
        }
        else if (ammoType == AmmoType.PISTOL_AMMO)
        {
            pistolAmmo += ammoQuantity;
        }

        RenewAmmoCapacity();
    }

    private void RenewAmmoCapacity()
    {
        if (currentUsingAmmoType == AmmoType.RIFLE_AMMO)
        {
            currentWeaponAmmo = rifleAmmo;
        }
        else if (currentUsingAmmoType == AmmoType.PISTOL_AMMO)
        {
            currentWeaponAmmo = pistolAmmo;
        }
    }

    private void UseAmmo()
    {
        if (currentUsingAmmoType == AmmoType.RIFLE_AMMO)
        {
            rifleAmmo = currentWeaponAmmo;
        }
        else if (currentUsingAmmoType == AmmoType.PISTOL_AMMO)
        {
            pistolAmmo = currentWeaponAmmo;
        }
    }

    private void CheckAmmo()
    {
        if (currentWeaponAmmo <= 0)
        {
            ammoLeft = false;
            return;
        }

        ammoLeft = true;
    }
}
