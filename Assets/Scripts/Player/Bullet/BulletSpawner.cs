using UnityEngine;
using MarchingBytes;

public class BulletSpawner : Spawner
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
        listOfObj = GetComponent<ListOfObj>();
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();

        gameObj = Resources.Load<GameObject>("Prefabs/Bullet");

        poolName = "Bullet";
    }

    protected void Update()
    {
        GetSpawnPosition();
    }

    private void GetSpawnPosition()
    {
        spawnPos = playerCtrl.PlayerWeaponSystem.ShootingPoint;
    }

    public override void Spawn()
    {
        base.Spawn();
    }

    protected override bool CanSpawn()
    {
        return true;
    }

    protected override void UpdateListGameObj()
    {
    }
}
