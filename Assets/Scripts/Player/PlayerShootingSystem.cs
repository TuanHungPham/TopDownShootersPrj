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
    [Space]
    public Transform crosshair;
    #endregion

    #region private var
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private GameObject muzzleFlash;
    [Space]
    [SerializeField] private PlayerCtrl playerCtrl;
    [Space]
    [SerializeField] private LayerMask enemyLayer;
    [Space]
    [SerializeField] private bool isShooting;
    [SerializeField] private bool cooldown;
    private Vector2 direction;
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
        crosshair = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetChild(0);
        enemyLayer = LayerMask.GetMask("Enemy");
        shootingPoint = transform.GetChild(0).Find("ShootingPoint");
        muzzleFlash = transform.GetChild(0).Find("Muzzle").gameObject;
        bulletTrail = Resources.Load<GameObject>("Prefabs/BulletTrail");

        shootDistance = 7;
        shootingTimer = 0;
    }

    private void Update()
    {
        CheckCooldown();
        GetShootDirection();
        GetShootDistance();
        Shoot();
        GetShootingVFX();
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, direction, shootDistance, enemyLayer);
        Debug.DrawRay(shootingPoint.position, direction * shootDistance, Color.red);

        if (hit.collider == null || !CanShoot())
        {
            isShooting = false;
            return;
        }

        GameObject trail = Instantiate(bulletTrail, shootingPoint.position, shootingPoint.rotation);
        BulletTrail trailScript = trail.GetComponent<BulletTrail>();
        trailScript.SetTargetPoint(hit.point);

        DamageReceiver damageReceiver = hit.collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.ReceiveDamage(dmg);

        Achievement.Instance.totalDmg += dmg;

        isShooting = true;
        shootingTimer = shootingDelay;
    }

    private void GetShootDirection()
    {
        direction = crosshair.position - shootingPoint.position;
        direction.Normalize();
    }

    private void GetShootDistance()
    {
        shootDistance = Vector2.Distance(crosshair.position, shootingPoint.position);
    }

    private void GetShootingVFX()
    {
        if (!isShooting) muzzleFlash.SetActive(false);
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
        if (cooldown) return false;

        return true;
    }
}
