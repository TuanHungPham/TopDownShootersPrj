using UnityEngine;

public class CoinSpawner : ItemSpanwer
{
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
        base.LoadComponents();
        listOfObj = transform.root.Find("CoinStorage").GetComponent<ListOfObj>();
    }

    public override void SetSpawnPos(Transform position)
    {
        base.SetSpawnPos(position);
    }

    public override void Spawn()
    {
        base.Spawn();
    }

    protected override bool CanSpawn()
    {
        return base.CanSpawn();
    }

    protected override void ClearActiveList()
    {
        base.ClearActiveList();
    }

    protected override void GetObjFromList()
    {
        base.GetObjFromList();
    }

    // protected override void SetActiveObj()
    // {
    //     base.SetActiveObj();
    // }

    protected override void UpdateListGameObj()
    {
        base.UpdateListGameObj();
    }
}
