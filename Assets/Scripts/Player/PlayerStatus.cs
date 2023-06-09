using UnityEngine;

public class PlayerStatus : Status
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void LoadComponents()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    protected override void Die()
    {
        base.Die();
    }

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
    }

    private void DisableWeapon()
    {
        foreach (Transform item in GameObject.Find("------ PLAYER ------").transform.GetChild(0).Find("PlayerWeapon"))
        {
            if (!item.gameObject.activeSelf) continue;

            item.gameObject.SetActive(false);
        }
    }

    protected override void DisableComponents()
    {
        DisableWeapon();

        playerCtrl.PlayerMovement.enabled = false;
        playerCtrl.PlayerWeaponSystem.enabled = false;
        playerCtrl.PlayerAimingSystem.enabled = false;
        playerCtrl.GrenadeAimSystem.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
}
