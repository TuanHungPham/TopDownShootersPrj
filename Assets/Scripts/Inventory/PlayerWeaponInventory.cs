using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInventory : MonoBehaviour
{
    #region public var
    public List<WeaponData> weaponInventory = new List<WeaponData>();
    public WeaponData SwappedWeapon { get => swappedWeapon; set => swappedWeapon = value; }
    #endregion

    #region private var
    [SerializeField] private WeaponData swappedWeapon;
    [SerializeField] private PlayerCtrl playerCtrl;
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
    }

    public void AddWeaponToInventory(WeaponData weaponData)
    {
        AddNewWeapon(weaponData);
        playerCtrl.AmmoSystem.AddAmmo(weaponData.AmmoType, weaponData.Ammo);
    }

    private void AddNewWeapon(WeaponData weaponData)
    {
        if (!IsExistWeaponTypeInInventory(weaponData))
        {
            weaponInventory.Add(weaponData);
            return;
        }
        else if (weaponInventory.Contains(weaponData)) return;

        weaponInventory.Remove(SwappedWeapon);
        weaponInventory.Add(weaponData);
        return;
    }

    private bool IsExistWeaponTypeInInventory(WeaponData weaponData)
    {
        SwappedWeapon = weaponInventory.Find((x) => x.WeaponType == weaponData.WeaponType);

        if (SwappedWeapon == null) return false;
        return true;
    }

    public WeaponData SearchInventoryByWeaponType(WeaponType type)
    {
        WeaponData weapon;
        for (int i = 0; i < weaponInventory.Count; i++)
        {
            if (weaponInventory[i].WeaponType != type) continue;

            weapon = weaponInventory[i];
            return weapon;
        }
        return null;
    }
}
