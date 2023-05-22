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
        SetBloodVFX();

        if (this.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.hitScene.TriggerHitScene();

            PlayerCtrl playerCtrl = gameObject.GetComponent<PlayerCtrl>();
            playerCtrl.playerSound.PlayHitSound();
        }

        if (objStatus.currentHP <= 0)
        {
            GetDeathSound();
            Invoke("SetDeadVFX", 1.2f);
            Drop();

            Achievement.Instance.enemiesKilled++;
            EnemyWaveManager.Instance.restOfEnemy--;
        }
    }

    private void GetDeathSound()
    {
        if (this.gameObject.CompareTag("Player")) return;

        EnemyCtrl enemyCtrl = gameObject.GetComponent<EnemyCtrl>();

        enemyCtrl.enemySound.SetRoarAudio();
    }

    private void SetBloodVFX()
    {
        GameObject blood = Instantiate(bloodVFX);
        blood.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        Vector3 rotate = blood.transform.localEulerAngles;
        if (transform.localScale.x == -1)
        {
            rotate.y = transform.rotation.y + 90;
        }
        else if (transform.localScale.x == 1)
        {
            rotate.y = transform.rotation.y - 90;
        }
        blood.transform.localEulerAngles = rotate;
    }

    private void SetDeadVFX()
    {
        if (deadVFX == null) return;

        GameObject vfx = Instantiate(deadVFX);
        vfx.transform.position = this.transform.position;
        vfx.transform.rotation = this.transform.rotation;
    }

    private void Drop()
    {
        if (!this.gameObject.CompareTag("Enemy")) return;
        EnemyCtrl enemyCtrl = this.gameObject.GetComponent<EnemyCtrl>();
        enemyCtrl.itemDropSystem.Invoke("DropItem", 1.3f);
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
