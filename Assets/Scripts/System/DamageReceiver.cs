using UnityEngine;
using MarchingBytes;

public class DamageReceiver : MonoBehaviour
{
    #region public var
    public bool IsHit { get => isHit; }
    #endregion

    #region private var
    [SerializeField] private bool isHit;
    [SerializeField] private string stateName;

    [Space(20)]
    [SerializeField] private GameObject deadVFX;
    [SerializeField] private Status objStatus;
    [SerializeField] private Animator animator;
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
        objStatus.CurrentHP -= dmg;
        animator.SetTrigger("Hit");
        SetBloodVFX();
        ShowDamageText(this.transform.position, dmg);

        if (this.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.HitScene.TriggerHitScene();

            PlayerCtrl playerCtrl = this.gameObject.GetComponent<PlayerCtrl>();
            playerCtrl.PlayerSound.PlayHitSound();
        }

        HandleDeath();
    }

    private void ShowDamageText(Vector3 showPosition, int damage)
    {
        UIManager.Instance.ShowDamageUIManager.ShowDamage(showPosition, damage);
    }

    private void HandleDeath()
    {
        if (objStatus.CurrentHP > 0) return;

        GetDeathSound();
        Invoke("SetDeadVFX", 1.2f);
        Drop();

        Achievement.Instance.EnemiesKilled++;
        EnemyWaveManager.Instance.RestOfEnemy--;

    }

    private void GetDeathSound()
    {
        if (this.gameObject.CompareTag("Player")) return;

        EnemyCtrl enemyCtrl = gameObject.GetComponent<EnemyCtrl>();

        enemyCtrl.EnemySound.SetRoarAudio();
    }

    private void SetBloodVFX()
    {

        // Quaternion rotate = new(1, 1, 1, 1);
        // if (transform.localScale.x == -1)
        // {
        //     rotate.y = transform.rotation.y + 90;
        // }
        // else if (transform.localScale.x == 1)
        // {
        //     rotate.y = transform.rotation.y - 90;
        // }

        GameObject blood = EasyObjectPool.instance.GetObjectFromPool("Blood", transform.position, transform.rotation);
    }

    private void SetDeadVFX()
    {
        if (deadVFX == null) return;

        GameObject vfx = EasyObjectPool.instance.GetObjectFromPool(deadVFX.name, this.transform.position, this.transform.rotation);
    }

    private void Drop()
    {
        if (!this.gameObject.CompareTag("Enemy")) return;
        EnemyCtrl enemyCtrl = this.gameObject.GetComponent<EnemyCtrl>();
        enemyCtrl.ItemDropSystem.Invoke("DropItem", 1.3f);
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
