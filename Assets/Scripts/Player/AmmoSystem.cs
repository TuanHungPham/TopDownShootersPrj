using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    #region public var
    public int currentWeaponAmmo;
    public AmmoType currentUsingAmmoType;
    public bool AmmoLeft { get => ammoLeft; set => ammoLeft = value; }
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private WeaponInventoryPanel weaponInventoryPanel;
    [SerializeField] private bool ammoLeft;
    [SerializeField] private int rifleAmmo;
    [SerializeField] private int pistolAmmo;
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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        weaponInventoryPanel = GameObject.Find("------ UI ------").transform.GetChild(0).GetComponentInChildren<WeaponInventoryPanel>();

        rifleAmmo = playerCtrl.playerWeaponSystem.playerShootingSystem.weapon.weaponData.Ammo;
        currentWeaponAmmo = rifleAmmo;
        pistolAmmo = 200;
    }

    private void Update()
    {
        SwapAmmoType();
        CheckAmmo();
    }

    private void SwapAmmoType()
    {
        if (!weaponInventoryPanel.IsWeaponSwitched) return;
        WeaponHolderUI holderSelected = weaponInventoryPanel.WeaponHolderSelected();

        if (holderSelected.holderType == HolderType.PRIMARY_HOLDER)
        {
            currentUsingAmmoType = AmmoType.RIFLE_AMMO;
        }
        else if (holderSelected.holderType == HolderType.SECONDARY_HOLDER)
        {
            currentUsingAmmoType = AmmoType.PISTOL_AMMO;
        }

        RenewAmmoCapacity();
        weaponInventoryPanel.IsWeaponSwitched = false;
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
