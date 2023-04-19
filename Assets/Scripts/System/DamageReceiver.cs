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
    [SerializeField] private GameObject bloodVFX;
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
        bloodVFX = Resources.Load<GameObject>("Prefabs/ParticalEffects/CFX2_Blood");
    }

    private void Update()
    {
        CheckHit();
    }

    public void ReceiveDamage(int dmg)
    {
        objStatus.currentHP -= dmg;
        animator.SetTrigger("Hit");

        GameObject blood = Instantiate(bloodVFX);
        blood.transform.position = transform.position;
        // blood.transform.rotation = new Vector3(bloodVFX.transform.rotation.x, transform.rotation.y - 180, bloodVFX.transform.rotation.z);

        if (this.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.hitScene.TriggerHitScene();
        }

        if (objStatus.currentHP <= 0)
        {
            Invoke("SetDeadVFX", 1.2f);

            if (!this.gameObject.CompareTag("Enemy")) return;
            EnemyCtrl enemyCtrl = this.gameObject.GetComponent<EnemyCtrl>();
            enemyCtrl.itemDropSystem.Invoke("DropItem", 1.3f);

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
