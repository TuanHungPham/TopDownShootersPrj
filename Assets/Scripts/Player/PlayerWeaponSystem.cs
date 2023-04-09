using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
    #region public var
    [Space]
    public Transform selectedWeapon;
    public Transform shootingPoint;

    [Space]
    public float shootDistance;
    public float shootingTimer;
    public float shootingDelay;
    public int dmg;
    public GameObject muzzleFlash;

    [Space]
    public PlayerShootingSystem playerShootingSystem;
    public RaycastHit2D hit;
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private WeaponInventoryPanel weaponInventoryPanel;

    [Space]
    [SerializeField] private Transform weaponStorage;
    [SerializeField] private Transform primaryWeaponHolder;
    [SerializeField] private Transform secondaryWeaponHolder;
    [SerializeField] private Transform meleeWeaponHolder;
    [SerializeField] private Transform crosshair;

    [Space]
    [SerializeField] private LayerMask enemyLayer;

    private Vector3 direction;
    private Quaternion lastRotation;
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
        GetWeaponInHolder();

        playerCtrl = GetComponentInParent<PlayerCtrl>();

        weaponInventoryPanel = GameObject.Find("------ UI ------").transform.GetChild(0).GetComponentInChildren<WeaponInventoryPanel>();
        weaponStorage = GameObject.Find("------ ITEM ------").transform.Find("WeaponStorage");
        crosshair = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetChild(0);
        primaryWeaponHolder = transform.Find("PrimaryWeapon").Find("Holder");
        secondaryWeaponHolder = transform.Find("SecondaryWeapon").Find("Holder");

        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        GetWeaponFromStorage();
        SwitchWeapon();
        GetWeaponInHolder();

        FlipWeapon();
        GetWeaponDirection();

        GetShootDirection();
        SetUpAimingLine();
    }

    private void SetUpAimingLine()
    {
        shootDistance = playerShootingSystem.weapon.weaponData.ShootDistance;

        hit = Physics2D.Raycast(shootingPoint.position, direction, shootDistance, enemyLayer);
        Debug.DrawRay(shootingPoint.position, direction * shootDistance, Color.red);
        SetUpCrosshairPosition();
    }

    private void GetShootDirection()
    {
        direction = crosshair.position - shootingPoint.position;
        direction.Normalize();
    }

    private void SetUpCrosshairPosition()
    {
        crosshair.position = shootingPoint.position + direction * shootDistance;
    }

    private void FlipWeapon()
    {
        Vector2 scale = selectedWeapon.localScale;

        if (playerCtrl.playerAimingSystem.joystick.Horizontal < 0)
        {
            scale = new Vector2(-1, -1);
        }
        else if (playerCtrl.playerAimingSystem.joystick.Horizontal > 0)
        {
            scale = Vector2.one;
        }

        selectedWeapon.localScale = scale;
    }

    private void GetWeaponDirection()
    {
        Vector2 direction = selectedWeapon.position - crosshair.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (playerCtrl.playerAimingSystem.joystick.Horizontal != 0 && playerCtrl.playerAimingSystem.joystick.Vertical != 0)
        {
            selectedWeapon.rotation = Quaternion.Euler(0, 0, angle - 180);
            lastRotation = selectedWeapon.rotation;
        }
        else
        {
            selectedWeapon.rotation = lastRotation;
        }
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

        playerCtrl.playerWeaponInventory.IsUpdateInventory = false;
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

    private void GetWeaponInHolder()
    {
        foreach (Transform child in transform)
        {
            if (!child.Find("Holder").gameObject.activeSelf) continue;

            selectedWeapon = child.Find("Holder").GetChild(0);
            shootingPoint = selectedWeapon.Find("ShootingPoint");

            playerShootingSystem = child.GetComponent<PlayerShootingSystem>();
            GetWeaponInfo(playerShootingSystem, child);
            return;
        }
    }

    private void GetWeaponInfo(PlayerShootingSystem playerShootingSystem, Transform transform)
    {
        muzzleFlash = selectedWeapon.Find("Muzzle").gameObject;

        playerShootingSystem.weapon = transform.Find("Holder").GetComponentInChildren<Weapon>();
        shootDistance = playerShootingSystem.weapon.weaponData.ShootDistance;
        shootingDelay = 1 / playerShootingSystem.weapon.weaponData.FireRate;
        dmg = playerShootingSystem.weapon.weaponData.WeaponDmg;
    }
}
