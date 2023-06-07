using UnityEngine;

public class MagazineSpawner : ItemSpanwer
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
        listOfObj = transform.root.Find("MagazineStorage").GetComponent<ListOfObj>();
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

    protected override void UpdateListGameObj()
    {
        base.UpdateListGameObj();
    }
}
