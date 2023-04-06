using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private bool isDrop;
    private Transform dropPos;
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

    public void DropItem()
    {
        if (isDrop)
        {
            ItemSpawnerCtrl.Instance.coinSpawner.canSpawn = false;
            return;
        }

        //Vector3 position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        //dropPos.position = new Vector3(transform.parent.position.x + position.x, transform.parent.position.y + position.y, 0);
        dropPos = transform.parent;

        ItemSpawnerCtrl.Instance.coinSpawner.GetSpawnPos(dropPos);
        ItemSpawnerCtrl.Instance.coinSpawner.canSpawn = true;
        Debug.Log("Drop Item");
        isDrop = true;
    }
}
