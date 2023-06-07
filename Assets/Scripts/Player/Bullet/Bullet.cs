using UnityEngine;
using MarchingBytes;

public class Bullet : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private int bulletDmg;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float flyTimer;
    [SerializeField] private float flyTime;
    [SerializeField] private bool isDealingDmg;

    [Space(20)]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 direction;

    [Space(20)]
    [SerializeField] private PoolObject poolObject;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Rigidbody2D rb2d;
    #endregion

    private void OnEnable()
    {
        flyTimer = flyTime;
        isDealingDmg = false;

        InitializeBullet();
        GetFlyDirection();
    }

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
        poolObject = GetComponent<PoolObject>();
        rb2d = GetComponent<Rigidbody2D>();

        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
    }

    private void Update()
    {
        CheckLifeTime();
    }

    private void FixedUpdate()
    {
        Fly();
    }

    public void InitializeBullet()
    {
        target = playerCtrl.PlayerWeaponSystem.Hit.transform;
        bulletDmg = playerCtrl.PlayerWeaponSystem.Dmg;
    }

    private void GetFlyDirection()
    {
        if (target == null) return;

        direction = target.position - playerCtrl.PlayerWeaponSystem.ShootingPoint.position;
        direction.Normalize();
    }

    private void CheckLifeTime()
    {
        if (flyTimer <= 0)
        {
            DisableBullet();
            return;
        }

        flyTimer -= Time.deltaTime;
    }

    private void Fly()
    {
        rb2d.MovePosition(transform.position + direction * bulletSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Enemy") || isDealingDmg) return;

        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        damageReceiver.ReceiveDamage(bulletDmg);
        Achievement.Instance.TotalDmg += bulletDmg;

        isDealingDmg = true;
        DisableBullet();
    }

    private void DisableBullet()
    {
        if (poolObject.isPooled) return;

        EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
    }
}
