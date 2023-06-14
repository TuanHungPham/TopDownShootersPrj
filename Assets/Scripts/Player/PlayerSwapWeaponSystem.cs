using TigerForge;
using UnityEngine;

public class PlayerSwapWeaponSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private UIManager uIManager;
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

        uIManager = GameObject.Find("------ UI ------").GetComponentInChildren<UIManager>();
        weaponStorage = GameObject.Find("------ ITEM ------").transform.Find("WeaponStorage");
        primaryWeaponHolder = transform.Find("PrimaryWeapon").Find("Holder");
        secondaryWeaponHolder = transform.Find("SecondaryWeapon").Find("Holder");
    }

    private void Update()
    {
        StoreWeapon();
        GetWeaponFromStorage();
        SwitchWeapon();
    }

    public void GetWeaponFromStorage()
    {
        if (!playerCtrl.PlayerWeaponInventory.IsUpdateInventory) return;

        for (int i = 0; i < playerCtrl.PlayerWeaponInventory.weaponInventory.Count; i++)
        {
            Transform weapon = GetWeaponInStorageByName(playerCtrl.PlayerWeaponInventory.weaponInventory[i]);

            if (weapon == null) continue;

            PutWeaponToHolder(weapon);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = transform.parent.rotation;
            weapon.gameObject.SetActive(true);
        }

        playerCtrl.PlayerWeaponInventory.IsUpdateInventory = false;
    }

    private void PutWeaponToHolder(Transform obj)
    {
        Weapon weapon = obj.GetComponent<Weapon>();

        if (weapon.WeaponData.WeaponType == WeaponType.PRIMARY_WEAPON)
        {
            obj.SetParent(primaryWeaponHolder);
        }
        else if (weapon.WeaponData.WeaponType == WeaponType.SECONDARY_WEAPON)
        {
            obj.SetParent(secondaryWeaponHolder);
        }

        obj.localPosition = Vector3.zero;
        obj.localScale = Vector3.one;
        obj.localRotation = obj.parent.rotation;
    }

    private void StoreWeapon()
    {
        if (!playerCtrl.PlayerWeaponInventory.IsUpdateInventory || playerCtrl.PlayerWeaponInventory.SwappedWeapon == null) return;

        Transform holder = null;

        if (playerCtrl.PlayerWeaponInventory.SwappedWeapon.WeaponType == WeaponType.PRIMARY_WEAPON)
        {
            holder = primaryWeaponHolder;
        }
        else if (playerCtrl.PlayerWeaponInventory.SwappedWeapon.WeaponType == WeaponType.SECONDARY_WEAPON)
        {
            holder = secondaryWeaponHolder;
        }

        foreach (Transform item in holder)
        {
            Weapon weapon = item.GetComponent<Weapon>();

            if (weapon.WeaponData != playerCtrl.PlayerWeaponInventory.SwappedWeapon) continue;

            item.gameObject.SetActive(false);
            PutWeaponToStorage(item);
        }
    }

    private void PutWeaponToStorage(Transform obj)
    {
        obj.SetParent(weaponStorage);
    }

    private Transform GetWeaponInStorageByName(WeaponData weaponData)
    {
        int childCount = 0;
        if (childCount > weaponStorage.childCount) return null;

        foreach (Transform item in weaponStorage)
        {
            childCount++;
            Weapon weapon = item.GetComponent<Weapon>();

            if (!weapon.WeaponData.WeaponName.Equals(weaponData.WeaponName)) continue;

            return item;
        }

        return null;
    }

    private void SwitchWeapon()
    {
        if (!uIManager.WeaponInventoryPanel.IsWeaponSwitched) return;

        WeaponHolderUI weaponHolderUI = uIManager.WeaponInventoryPanel.WeaponHolderSelected();

        GetHolder(weaponHolderUI);

        EventManager.EmitEvent(EventID.SWITCHING_WEAPON.ToString());
    }

    private void GetHolder(WeaponHolderUI weaponHolderUI)
    {
        primaryWeaponHolder.gameObject.SetActive(false);
        secondaryWeaponHolder.gameObject.SetActive(false);

        if (weaponHolderUI.HolderType == HolderType.PRIMARY_HOLDER)
        {
            primaryWeaponHolder.gameObject.SetActive(true);
        }
        else if (weaponHolderUI.HolderType == HolderType.SECONDARY_HOLDER)
        {
            secondaryWeaponHolder.gameObject.SetActive(true);
        }
    }
}
