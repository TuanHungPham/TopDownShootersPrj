using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    #region public var
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public AttackArea AttackArea { get => attackArea; set => attackArea = value; }
    #endregion

    #region private var
    [SerializeField] private float atkCoolDownTimer;
    [SerializeField] private float atkDelay;
    [SerializeField] private AttackArea attackArea;
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private bool isAttacking;
    private bool cooldown;
    #endregion

    private void OnEnable()
    {
        isAttacking = false;
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
        AttackArea = GetComponentInChildren<AttackArea>();
        enemyCtrl = GetComponentInParent<EnemyCtrl>();

        atkDelay = 1.5f;
        atkCoolDownTimer = atkDelay;
    }

    private void Update()
    {
        CheckCooldownTime();
        Attack();
    }

    private void Attack()
    {
        if (!CanAttack())
        {
            isAttacking = false;
            return;
        }

        isAttacking = true;
        atkCoolDownTimer = atkDelay;
    }

    private void CheckCooldownTime()
    {
        if (atkCoolDownTimer <= 0)
        {
            cooldown = false;
            return;
        }

        atkCoolDownTimer -= Time.deltaTime;
        cooldown = true;
    }

    private bool CanAttack()
    {
        if (AttackArea.IsTrigger && !cooldown) return true;

        return false;
    }
}
