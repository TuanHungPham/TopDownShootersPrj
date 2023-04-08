using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryPanel : MonoBehaviour
{
    #region public var
    public List<WeaponHolderUI> listOfWeaponHolderUI = new List<WeaponHolderUI>();
    public bool IsWeaponSwitched { get => isWeaponSwitched; set => isWeaponSwitched = value; }
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private GameObject primaryHolder;
    [SerializeField] private GameObject secondaryHolder;
    [SerializeField] private GameObject meleeHolder;
    [SerializeField] private bool isWeaponSwitched;
    [SerializeField] WeaponHolderUI primaryHolderUI;
    [SerializeField] WeaponHolderUI secondaryHolderUI;
    [SerializeField] WeaponHolderUI meleeHolderUI;

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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        primaryHolder = Resources.Load<GameObject>("Prefabs/PrimaryWeaponHolderUI");
        secondaryHolder = Resources.Load<GameObject>("Prefabs/SecondaryWeaponHolderUI");
        meleeHolder = Resources.Load<GameObject>("Prefabs/MeleeWeaponHolderUI");
    }

    private void Update()
    {
        SetData(primaryHolderUI, WeaponType.PRIMARY_WEAPON);
        SetData(secondaryHolderUI, WeaponType.SECONDARY_WEAPON);
        SetData(meleeHolderUI, WeaponType.MELEE_WEAPON);
    }

    private void InitializeWeaponInventory()
    {
        GameObject primary = NewWeaponHolder(primaryHolder);
        GameObject secondary = NewWeaponHolder(secondaryHolder);
        GameObject melee = NewWeaponHolder(meleeHolder);

        primaryHolderUI = primary.GetComponent<WeaponHolderUI>();
        secondaryHolderUI = secondary.GetComponent<WeaponHolderUI>();
        meleeHolderUI = melee.GetComponent<WeaponHolderUI>();

        listOfWeaponHolderUI.Add(primaryHolderUI);
        listOfWeaponHolderUI.Add(secondaryHolderUI);
        listOfWeaponHolderUI.Add(meleeHolderUI);

        SetUpHandleMethod(primaryHolderUI);
        SetUpHandleMethod(secondaryHolderUI);
        SetUpHandleMethod(meleeHolderUI);

        primaryHolderUI.IsSelected = true;
    }

    private GameObject NewWeaponHolder(GameObject obj)
    {
        GameObject newObj = Instantiate(obj);
        newObj.transform.SetParent(transform);
        newObj.transform.localScale = Vector3.one;

        return newObj;
    }

    private void SetUpHandleMethod(WeaponHolderUI weaponHolderUI)
    {
        weaponHolderUI.OnItemClicked += HandleItemSelection;
    }

    private void HandleItemSelection(WeaponHolderUI weaponHolderUI)
    {
        if (weaponHolderUI.IsEmpty) return;

        DeselecteAllItems();
        weaponHolderUI.Select();
        IsWeaponSwitched = true;
    }

    private void DeselecteAllItems()
    {
        foreach (WeaponHolderUI item in listOfWeaponHolderUI)
        {
            item.Deselect();
        }
    }

    private void SetData(WeaponHolderUI weaponHolderUI, WeaponType weaponType)
    {
        WeaponData weapon = playerCtrl.playerWeaponInventory.SearchInventoryByWeaponType(weaponType);
        if (weapon == null) return;

        weaponHolderUI.SetImage(weapon.WeaponSprite);
    }
}
