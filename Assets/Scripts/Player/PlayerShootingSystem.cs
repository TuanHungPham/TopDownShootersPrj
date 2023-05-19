using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootingSystem : MonoBehaviour
{
    #region public var
    public bool IsShooting { get => isShooting; set => isShooting = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject bulletTrail;
    [Space]
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] public Weapon weapon;
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

        bulletTrail = Resources.Load<GameObject>("Prefabs/BulletTrail");
    }

    private void Update()
    {
        CheckCooldown();
        Shoot();
        GetWeaponSound();
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
        damageReceiver.ReceiveDamage(playerCtrl.playerWeaponSystem.dmg);

        Achievement.Instance.totalDmg += playerCtrl.playerWeaponSystem.dmg;

        IsShooting = true;
        playerCtrl.ammoSystem.ConsumpAmmo();
        playerCtrl.playerWeaponSystem.shootingTimer = playerCtrl.playerWeaponSystem.shootingDelay;
    }

    private void GetWeaponSound()
    {
        if (playerCtrl.playerWeaponSystem.hit.collider == null)
        {
            playerCtrl.playerSound.SetWeaponSound(false);
            return;
        }

        playerCtrl.playerSound.SetWeaponSound(true);
    }

    private void GetShootingVFX()
    {
        if (playerCtrl.playerWeaponSystem.muzzleFlash == null) return;

        if (!IsShooting) playerCtrl.playerWeaponSystem.muzzleFlash.SetActive(false);
        else playerCtrl.playerWeaponSystem.muzzleFlash.SetActive(true);
    }

    private void CheckCooldown()
    {
        if (playerCtrl.playerWeaponSystem.shootingTimer > 0)
        {
            playerCtrl.playerWeaponSystem.shootingTimer -= Time.deltaTime;
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
