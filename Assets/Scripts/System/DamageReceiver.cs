using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    #region public var
    public bool IsHit { get => isHit; }
    #endregion

    #region private var
    [SerializeField] private Status objStatus;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isHit;
    [SerializeField] private GameObject deadVFX;
    [SerializeField] private string stateName;
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
        objStatus = GetComponent<Status>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CheckHit();
    }

    public void ReceiveDamage(int dmg)
    {
        objStatus.currentHP -= dmg;
        animator.SetTrigger("Hit");


        if (objStatus.currentHP <= 0)
        {
            Invoke("SetDeadVFX", 2.6f);

            if (!this.gameObject.CompareTag("Enemy")) return;

            EnemyCtrl enemyCtrl = this.gameObject.GetComponent<EnemyCtrl>();
            enemyCtrl.itemDropSystem.Invoke("DropItem", 2.6f);

            Achievement.Instance.enemiesKilled++;
            EnemyWaveManager.Instance.restOfEnemy--;
        }
    }

    private void SetDeadVFX()
    {
        if (deadVFX == null) return;

        GameObject vfx = Instantiate(deadVFX);
        vfx.transform.position = this.transform.position;
        vfx.transform.rotation = this.transform.rotation;
    }

    private void CheckHit()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            isHit = true;
            return;
        }

        isHit = false;
    }
}
