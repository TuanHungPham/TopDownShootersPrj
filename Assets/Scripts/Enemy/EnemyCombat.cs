using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    #region public var
    public float atkCoolDownTimer;
    public float atkDelay;
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public AttackArea attackArea;
    #endregion

    #region private var
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
        attackArea = GetComponentInChildren<AttackArea>();
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
        if (attackArea.IsTrigger && !cooldown) return true;

        return false;
    }
}
