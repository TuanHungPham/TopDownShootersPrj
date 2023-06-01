using UnityEngine;

public class FlyEnemySkill : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private Rigidbody2D rb2d;
    private bool checkExplode;
    #endregion

    private void OnEnable()
    {
        checkExplode = false;
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
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        Explode();
    }

    private void Explode()
    {
        if (!enemyCtrl.EnemyCombat.IsAttacking || checkExplode)
        {
            return;
        }

        enemyCtrl.DamageReceiver.ReceiveDamage((int)enemyCtrl.EnemyStatus.MaxHP);
        checkExplode = true;
    }
}
