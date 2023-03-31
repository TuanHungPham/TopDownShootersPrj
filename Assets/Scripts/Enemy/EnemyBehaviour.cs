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
    }

    public void SetDeathAnimation()
    {
        if (checkDeathAnimation) return; 

        if (enemyCtrl.enemyStatus.IsDeath)
        {
            animator.SetTrigger("isDeath");
            checkDeathAnimation = true;
        }

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
