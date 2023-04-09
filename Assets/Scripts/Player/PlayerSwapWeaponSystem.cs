using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapWeaponSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private WeaponInventoryPanel weaponInventoryPanel;
    [SerializeField] private Transform weaponStorage;

    [SerializeField] private Transform primaryWeaponHolder;
    [SerializeField] private Transform secondaryWeaponHolder;
    [SerializeField] private Transform meleeWeaponHolder;
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
        playerCtrl = GetComponentInParent<PlayerCtrl>();

        weaponInventoryPanel = GameObject.Find("------ UI ------").transform.GetChild(0).GetComponentInChildren<WeaponInventoryPanel>();
        weaponStorage = GameObject.Find("------ ITEM ------").transform.Find("WeaponStorage");
        primaryWeaponHolder = transform.Find("PrimaryWeapon").Find("Holder");
        secondaryWeaponHolder = transform.Find("SecondaryWeapon").Find("Holder");
    }

    private void Update()
    {
        GetWeaponFromStorage();
        StoreWeapon();
        SwitchWeapon();
    }

    private void GetWeaponFromStorage()
    {
        if (!playerCtrl.playerWeaponInventory.IsUpdateInventory) return;

        for (int i = 0; i < playerCtrl.playerWeaponInventory.weaponInventory.Count; i++)
        {
            Transform weapon = GetWeaponInStorageByName(playerCtrl.playerWeaponInventory.weaponInventory[i]);

            if (weapon == null) continue;

            PutWeaponToHolder(weapon);
            weapon.localPosition = Vector3.zero;
            weapon.gameObject.SetActive(true);
        }
    }

    private void PutWeaponToHolder(Transform obj)
    {
        Weapon weapon = obj.GetComponent<Weapon>();

        if (weapon.weaponData.WeaponType == WeaponType.PRIMARY_WEAPON)
        {
            obj.SetParent(primaryWeaponHolder);
        }
        else if (weapon.weaponData.WeaponType == WeaponType.SECONDARY_WEAPON)
        {
            obj.SetParent(secondaryWeaponHolder);
        }
    }

    private void StoreWeapon()
    {
        if (!playerCtrl.playerWeaponInventory.IsUpdateInventory || playerCtrl.playerWeaponInventory.swappedWeapon == null) return;

        Transform holder = null;

        if (playerCtrl.playerWeaponInventory.swappedWeapon.WeaponType == WeaponType.PRIMARY_WEAPON)
        {
            holder = primaryWeaponHolder;
        }
        else if (playerCtrl.playerWeaponInventory.swappedWeapon.WeaponType == WeaponType.SECONDARY_WEAPON)
        {
            holder = secondaryWeaponHolder;
        }

        foreach (Transform item in holder)
        {
            Weapon weapon = item.GetComponent<Weapon>();

            if (weapon.weaponData != playerCtrl.playerWeaponInventory.swappedWeapon) continue;

            item.gameObject.SetActive(false);
            PutWeaponToStorage(item);
        }

        playerCtrl.playerWeaponInventory.IsUpdateInventory = false;
    }

    private void PutWeaponToStorage(Transform obj)
    {
        obj.SetParent(weaponStorage);
    }

    private Transform GetWeaponInStorageByName(WeaponData weaponData)
    {
        foreach (Transform item in weaponStorage)
        {
            Weapon weapon = item.GetComponent<Weapon>();

            if (!weapon.weaponData.WeaponName.Equals(weaponData.WeaponName)) continue;

            return item;
        }
        return null;
    }

    private void SwitchWeapon()
    {
        if (!weaponInventoryPanel.IsWeaponSwitched) return;

        foreach (WeaponHolderUI weaponHolderUI in weaponInventoryPanel.listOfWeaponHolderUI)
        {
            if (!weaponHolderUI.IsSelected) continue;

            GetHolder(weaponHolderUI);
            return;
        }
    }

    private void GetHolder(WeaponHolderUI weaponHolderUI)
    {
        primaryWeaponHolder.gameObject.SetActive(false);
        secondaryWeaponHolder.gameObject.SetActive(false);

        if (weaponHolderUI.holderType == HolderType.PRIMARY_HOLDER)
        {
            primaryWeaponHolder.gameObject.SetActive(true);
        }
        else if (weaponHolderUI.holderType == HolderType.SECONDARY_HOLDER)
        {
            secondaryWeaponHolder.gameObject.SetActive(true);
        }
    }
}
