using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSystem : MonoBehaviour
{
    #region public var
    public int itemDropQuantity;
    public Transform dropPos;
    #endregion

    #region private var
    [SerializeField] private bool isDrop;
    #endregion

    private void OnEnable()
    {
        isDrop = false;
    }

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
    }

    private void Update()
    {
        GetRandomItemDropQuantity();
    }

    private void GetRandomDropPosition()
    {
        Vector3 randomPosAround = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        dropPos.position = transform.parent.position + randomPosAround;
    }

    private void GetRandomItemDropQuantity()
    {
        itemDropQuantity = Random.Range(1, 7);
    }

    public void DropItem()
    {
        DropCoin();
    }

    private void DropCoin()
    {
        if (isDrop)
        {
            ItemSpawnerCtrl.Instance.coinSpawner.CanDrop = false;
            return;
        }

        ItemSpawnerCtrl.Instance.coinSpawner.GetSpawnPos(dropPos);
        ItemSpawnerCtrl.Instance.coinSpawner.maxObj += itemDropQuantity;
        ItemSpawnerCtrl.Instance.coinSpawner.CanDrop = true;
        Debug.Log("Drop Item");
        isDrop = true;
    }

    private void OnDisable()
    {
        ItemSpawnerCtrl.Instance.coinSpawner.CanDrop = false;
    }
}
