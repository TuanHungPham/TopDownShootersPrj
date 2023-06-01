using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private int bulletDmg;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float flyTime;
    [SerializeField] private bool isDealingDmg;

    [Space(20)]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 direction;

    [Space(20)]
    [SerializeField] private Rigidbody2D rb2d;
    #endregion

    private void OnEnable()
    {
        isDealingDmg = false;

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
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Invoke("DisableBullet", flyTime);
    }

    private void FixedUpdate()
    {
        Fly();
    }

    public void SetupBullet(Transform targetDestination, int dmg)
    {
        target = targetDestination;
        bulletDmg = dmg;
    }

    private void GetFlyDirection()
    {
        direction = target.position - transform.position;
        direction.Normalize();
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
        gameObject.SetActive(false);
    }
}
