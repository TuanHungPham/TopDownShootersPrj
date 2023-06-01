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
