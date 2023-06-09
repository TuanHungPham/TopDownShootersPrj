using UnityEngine;

public class CoinPickUp : PickUpSystem
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private int coinQuantity;
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
    }

    private void Update()
    {
        GetRandomCoinQuantity();
        SelfDestroy();
    }

    protected override void AddItemToPlayerInventory()
    {
        Achievement.Instance.TotalMoney += coinQuantity;
        SoundSystemManager.Instance.SetCoinSound();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void SelfDestroy()
    {
        base.SelfDestroy();
    }

    private void GetRandomCoinQuantity()
    {
        coinQuantity = Random.Range(2, 10);
    }
}
