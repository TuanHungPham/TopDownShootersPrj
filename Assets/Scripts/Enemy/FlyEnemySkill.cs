using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (!enemyCtrl.enemyCombat.IsAttacking || checkExplode)
        {
            return;
        }

        enemyCtrl.damageReceiver.ReceiveDamage(enemyCtrl.enemyStatus.maxHP);
        checkExplode = true;
    }
}
