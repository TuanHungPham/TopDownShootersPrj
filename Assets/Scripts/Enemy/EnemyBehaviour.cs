using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyCtrl enemyCtrl;
    private bool checkDeathAnimation;
    #endregion

    private void OnEnable()
    {
        checkDeathAnimation = false;
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

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetDeathAnimation();
        SetAttackAnimation();
        SetRunAnimation();
        SetIdleAnimation();
    }

    private void SetIdleAnimation()
    {
        if (!enemyCtrl.TargetExist)
        {
            animator.SetBool("Idle", true);
            return;
        }

        animator.SetBool("Idle", false);
    }

    private void SetDeathAnimation()
    {
        if (checkDeathAnimation) return;

        if (enemyCtrl.EnemyStatus.IsDeath)
        {
            animator.SetTrigger("isDeath");
            checkDeathAnimation = true;
        }

    }

    private void SetAttackAnimation()
    {
        if (enemyCtrl.EnemyCombat.IsAttacking)
        {
            animator.SetBool("Attack", true);
            return;
        }

        animator.SetBool("Attack", false);
    }

    private void SetRunAnimation()
    {
        if (enemyCtrl.EnemyMovement.IsRunning)
        {
            animator.SetBool("Run", true);
            return;
        }

        animator.SetBool("Run", false);
    }
}
