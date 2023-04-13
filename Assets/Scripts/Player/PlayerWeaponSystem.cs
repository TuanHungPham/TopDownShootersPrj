using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
    #region public var
    [Space]
    public Transform selectedWeapon;
    public Transform shootingPoint;
    public Transform crosshair;

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

    [Space]
    [SerializeField] private LayerMask enemyLayer;

    private Vector3 direction;
    private Quaternion lastRotation;
    private Vector2 lastScale;
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

        crosshair = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetChild(0);

        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
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
        FlipNewWeapon();

        Vector2 scale = selectedWeapon.localScale;

        if (playerCtrl.playerAimingSystem.joystick.Horizontal < 0)
        {
            scale = new Vector2(-1, -1);
        }
        else if (playerCtrl.playerAimingSystem.joystick.Horizontal > 0)
        {
            scale = Vector2.one;
        }

        lastScale = scale;
        selectedWeapon.localScale = scale;
    }

    private void FlipNewWeapon()
    {
        if (!UIManager.Instance.weaponInventoryPanel.IsWeaponSwitched) return;

        selectedWeapon.localScale = lastScale;
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

    private void GetWeaponInHolder()
    {
        foreach (Transform child in transform)
        {
            Transform holder = child.Find("Holder");
            if (!holder.gameObject.activeSelf) continue;

            if (holder.childCount <= 0) continue;
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
