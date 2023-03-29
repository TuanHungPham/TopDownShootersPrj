using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyCtrl enemyCtrl;
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
        enemyCtrl = GetComponentInParent<EnemyCtrl>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetDeathAnimation();
        SetAttackAnimation();
        SetRunAnimation();
    }

    private void SetDeathAnimation()
    {
        if (enemyCtrl.enemyStatus.IsDeath)
        {
            animator.SetBool("isDeath", true);
            return;
        }

        animator.SetBool("isDeath", false);
    }

    private void SetAttackAnimation()
    {
        if (enemyCtrl.enemyCombat.IsAttacking)
        {
            animator.SetBool("Attack", true);
            return;
        }

        animator.SetBool("Attack", false);
    }

    private void SetRunAnimation()
    {
        if (enemyCtrl.enemyMovement.IsRunning)
        {
            animator.SetBool("Run", true);
            return;
        }

        animator.SetBool("Run", false);
    }
}
