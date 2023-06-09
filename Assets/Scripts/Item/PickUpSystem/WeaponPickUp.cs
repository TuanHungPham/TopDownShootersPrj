using UnityEngine;
using TigerForge;

public class WeaponPickUp : PickUpSystem
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private Weapon weapon;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
    }

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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        SelfDestroy();
    }

    protected override void SelfDestroy()
    {
        base.SelfDestroy();
    }

    protected override void AddItemToPlayerInventory()
    {
        playerCtrl.PlayerWeaponInventory.AddWeaponToInventory(weapon.WeaponData);

        SoundSystemManager.Instance.SetReloadSound();

        EventManager.EmitEvent(EventID.UPDATING_WEAPON_INVENTORY.ToString());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
