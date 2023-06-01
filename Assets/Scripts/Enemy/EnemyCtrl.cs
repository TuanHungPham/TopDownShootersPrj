using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    #region public var
    public bool TargetExist { get => targetExist; set => targetExist = value; }
    public PlayerCtrl PlayerCtrl { get => playerCtrl; set => playerCtrl = value; }
    public EnemyMovement EnemyMovement { get => enemyMovement; set => enemyMovement = value; }
    public EnemyBehaviour EnemyBehaviour { get => enemyBehaviour; set => enemyBehaviour = value; }
    public Status EnemyStatus { get => enemyStatus; set => enemyStatus = value; }
    public EnemyCombat EnemyCombat { get => enemyCombat; set => enemyCombat = value; }
    public EnemySound EnemySound { get => enemySound; set => enemySound = value; }
    public DamageReceiver DamageReceiver { get => damageReceiver; set => damageReceiver = value; }
    public ItemDropSystem ItemDropSystem { get => itemDropSystem; set => itemDropSystem = value; }
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    [SerializeField] private Status enemyStatus;
    [SerializeField] private EnemyCombat enemyCombat;
    [SerializeField] private EnemySound enemySound;
    [SerializeField] private DamageReceiver damageReceiver;
    [SerializeField] private ItemDropSystem itemDropSystem;
    [SerializeField] private bool targetExist;

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
        DamageReceiver = GetComponent<DamageReceiver>();
        EnemyStatus = GetComponent<Status>();
        EnemyMovement = GetComponentInChildren<EnemyMovement>();
        EnemyBehaviour = GetComponentInChildren<EnemyBehaviour>();
        EnemyCombat = GetComponentInChildren<EnemyCombat>();
        EnemySound = GetComponentInChildren<EnemySound>();
        ItemDropSystem = GetComponentInChildren<ItemDropSystem>();
        PlayerCtrl = GameObject.Find("MainCharacter").GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        EnableComponents();
        CheckTargetExist();
    }

    private void EnableComponents()
    {
        if (!this.gameObject.activeSelf) return;

        EnemyStatus.enabled = true;
        EnemyCombat.enabled = true;
        EnemyMovement.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        GameObject enemyWeapon = transform.Find("EnemySprite").GetChild(0).gameObject;
        if (enemyWeapon == null) return;
        enemyWeapon.SetActive(true);
    }

    public void DisableComponents()
    {
        EnemyCombat.enabled = false;
        EnemyMovement.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void DisableWeapon()
    {
        GameObject enemyWeapon = transform.Find("EnemySprite").GetChild(0).gameObject;
        if (enemyWeapon == null) return;
        enemyWeapon.SetActive(false);
    }

    private void CheckTargetExist()
    {
        if (PlayerCtrl.PlayerStatus.IsDeath)
        {
            targetExist = false;
            return;
        }

        targetExist = true;
    }
}
