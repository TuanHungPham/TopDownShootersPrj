using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInventory : MonoBehaviour
{
    #region public var
    public List<WeaponData> weaponInventory = new List<WeaponData>();
    #endregion

    #region private var
    [SerializeField] private WeaponData initialWeapon;
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
        // initialWeapon = Resources.Load<WeaponData>("WeaponData/AK47");

        InitializeWeaponInventory();
    }

    private void InitializeWeaponInventory()
    {
        if (weaponInventory.Contains(initialWeapon)) return;

        weaponInventory.Add(initialWeapon);
    }

    public WeaponData SearchByWeaponType(WeaponType type)
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
