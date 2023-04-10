using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinePickUp : PickUpSystem
{
    #region  public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private MagazineData magazineData;
    #endregion

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
        playerCtrl.ammoSystem.AddAmmo(magazineData.AmmoType, magazineData.AmmoQuantity);
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
