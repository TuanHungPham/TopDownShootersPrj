using UnityEngine;

public class GrenadePickUp : PickUpSystem
{
    #region  public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LoadComponents()
    {
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();
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
        playerCtrl.GrenadeSystem.GrenadeQuantity++;
        SoundSystemManager.Instance.SetReloadSound();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void Reset()
    {
        base.Reset();
    }
}
