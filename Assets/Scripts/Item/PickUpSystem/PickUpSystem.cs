using UnityEngine;
using MarchingBytes;

public abstract class PickUpSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] protected float existTime = 15f;
    #endregion

    protected virtual void OnEnable()
    {
        existTime = 15f;
    }

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected abstract void LoadComponents();

    protected virtual void SelfDestroy()
    {
        if (existTime <= 0)
        {
            EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
            return;
        }

        existTime -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        AddItemToPlayerInventory();
        EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
    }

    protected abstract void AddItemToPlayerInventory();
}
