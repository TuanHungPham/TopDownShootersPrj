using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
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

    private void Start()
    {
        SetPlayerData();
    }

    private void LoadComponents()
    {
        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }

    private void SetPlayerData()
    {
        SetHP();
        SetCharacterSkin();
        SetInitialWeapons();
        SetInitialAmmo();
    }

    private void SetHP()
    {
        playerCtrl.playerStatus.maxHP = DataManager.Instance.characterDataManager.characterHP;
    }

    private void SetCharacterSkin()
    {
        CharacterSkinManager.Instance.SetSkin(DataManager.Instance.characterDataManager.characterSkinIndex);
    }

    private void SetInitialWeapons()
    {
        playerCtrl.playerWeaponInventory.weaponInventory.Add(DataManager.Instance.characterDataManager.primaryWeaponData);
        playerCtrl.playerWeaponInventory.weaponInventory.Add(DataManager.Instance.characterDataManager.secondaryWeaponData);
        playerCtrl.playerWeaponInventory.IsUpdateInventory = true;
    }

    private void SetInitialAmmo()
    {
        playerCtrl.ammoSystem.rifleAmmo = DataManager.Instance.characterDataManager.primaryWeaponData.InitialAmmo;
        playerCtrl.ammoSystem.pistolAmmo = DataManager.Instance.characterDataManager.secondaryWeaponData.InitialAmmo;
    }
}
