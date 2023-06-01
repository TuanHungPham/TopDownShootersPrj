using UnityEngine;

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
        parent = transform;
    }

    protected void Update()
    {
        GetSpawnPosition();
        UpdateListGameObj();
    }

    private void GetSpawnPosition()
    {
        spawnPos = playerCtrl.PlayerWeaponSystem.ShootingPoint;
    }

    public void Spawn(Transform target, int dmg)
    {
        GameObject bullet;
        if (listOfInactiveObj.Count > 0)
        {
            bullet = RandomGameObj();
            listOfInactiveObj.Remove(bullet);
            listOfActiveObj.Add(bullet);
        }
        else
        {
            bullet = NewGameObj(gameObj);
            listOfActiveObj.Add(bullet);
        }

        SetupBullet(bullet, target, dmg);
        bullet.transform.position = spawnPos.position;
        bullet.transform.rotation = spawnPos.rotation;
        bullet.transform.parent = parent;
        bullet.SetActive(true);
    }

    private void SetupBullet(GameObject bullet, Transform target, int dmg)
    {
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.SetupBullet(target, dmg);
    }

    protected override GameObject NewGameObj(GameObject obj)
    {
        return base.NewGameObj(obj);
    }

    protected override GameObject RandomGameObj()
    {
        return base.RandomGameObj();
    }

    protected override void SetActiveObj()
    {
        base.SetActiveObj();
    }

    protected override bool CanSpawn()
    {
        return true;
    }

    protected override void UpdateListGameObj()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf && !listOfActiveObj.Contains(child.gameObject))
            {
                listOfActiveObj.Add(child.gameObject);
            }
            else if (!child.gameObject.activeSelf && listOfActiveObj.Contains(child.gameObject))
            {
                listOfActiveObj.Remove(child.gameObject);
            }
            else if (!child.gameObject.activeSelf && !listOfInactiveObj.Contains(child.gameObject))
            {
                listOfInactiveObj.Add(child.gameObject);
            }
        }
    }
}
