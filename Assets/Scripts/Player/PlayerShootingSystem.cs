using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootingSystem : MonoBehaviour
{
    #region public var
    public float shootDistance;
    public float shootingDelay;
    public float shootingTimer;
    public int dmg;
    public GameObject muzzleFlash;
    public bool IsShooting { get => isShooting; set => isShooting = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject bulletTrail;
    [Space]
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Weapon weapon;
    [Space]
    [SerializeField] private bool isShooting;
    [SerializeField] private bool cooldown;
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
        weapon = transform.GetChild(0).GetComponentInChildren<Weapon>();
        bulletTrail = Resources.Load<GameObject>("Prefabs/BulletTrail");

        shootDistance = weapon.weaponData.ShootDistance;
        shootingDelay = 1 / weapon.weaponData.FireRate;
        dmg = weapon.weaponData.WeaponDmg;
        shootingTimer = 0;
    }

    private void Update()
    {
        CheckCooldown();
        Shoot();
        GetShootingVFX();
    }

    private void Shoot()
    {
        if (playerCtrl.playerWeaponSystem.hit.collider == null || !CanShoot())
        {
            IsShooting = false;
            return;
        }

        GameObject trail = Instantiate(bulletTrail, playerCtrl.playerWeaponSystem.shootingPoint.position, playerCtrl.playerWeaponSystem.shootingPoint.rotation);
        BulletTrail trailScript = trail.GetComponent<BulletTrail>();
        trailScript.SetTargetPoint(playerCtrl.playerWeaponSystem.hit.point);

        DamageReceiver damageReceiver = playerCtrl.playerWeaponSystem.hit.collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.ReceiveDamage(dmg);

        Achievement.Instance.totalDmg += dmg;

        IsShooting = true;
        shootingTimer = shootingDelay;
    }

    private void GetShootingVFX()
    {
        if (!IsShooting) muzzleFlash.SetActive(false);
        else muzzleFlash.SetActive(true);
    }

    private void CheckCooldown()
    {
        if (shootingTimer > 0)
        {
            shootingTimer -= Time.deltaTime;
            cooldown = true;
            return;
        }

        cooldown = false;
    }

    private bool CanShoot()
    {
        if (cooldown || !playerCtrl.ammoSystem.AmmoLeft) return false;

        return true;
    }
}
