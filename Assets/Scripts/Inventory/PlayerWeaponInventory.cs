using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInventory : MonoBehaviour
{
    #region public var
    public List<WeaponData> weaponInventory = new List<WeaponData>();
    public bool IsUpdateInventory { get => isUpdateInventory; set => isUpdateInventory = value; }
    public WeaponData swappedWeapon;
    #endregion

    #region private var
    [SerializeField] private WeaponData initialWeapon;
    [SerializeField] private bool isUpdateInventory;
    #endregion

    private void Awake()
    {
        LoadComponents();
        InitializeWeaponInventory();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        initialWeapon = Resources.Load<WeaponData>("WeaponData/AK47");
    }

    private void InitializeWeaponInventory()
    {
        if (weaponInventory.Contains(initialWeapon)) return;

        weaponInventory.Add(initialWeapon);
        isUpdateInventory = true;
    }

    public void AddWeaponToInventory(WeaponData weaponData)
    {
        if (!IsExistWeaponInInventory(weaponData))
        {
            weaponInventory.Add(weaponData);
            return;
        }

        weaponInventory.Remove(swappedWeapon);
        weaponInventory.Add(weaponData);
    }

    private bool IsExistWeaponInInventory(WeaponData weaponData)
    {
        swappedWeapon = weaponInventory.Find((x) => x.WeaponType == weaponData.WeaponType);

        if (swappedWeapon == null) return false;
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
