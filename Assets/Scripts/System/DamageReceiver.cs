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
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Update()
    {
        CheckHit();
    }

    private void LoadComponents()
    {
        objStatus = GetComponent<Status>();
        animator = GetComponentInChildren<Animator>();
    }

    public void ReceiveDamage(int dmg)
    {
        objStatus.currentHP -= dmg;
        animator.SetTrigger("Hit");
        if (objStatus.currentHP <= 0) 
        {
            Invoke("SetDeadVFX", 2.6f);

            if (!this.gameObject.CompareTag("Enemy")) return;
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            isHit = true;
            return;
        }

        isHit = false;
    }
}
