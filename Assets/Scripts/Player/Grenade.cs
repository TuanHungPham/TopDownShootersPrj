using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region public var
    public List<Vector3> listOfTrajectoryPoint = new List<Vector3>();
    public float flyTime;
    #endregion

    #region private var
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private GameObject explodeVFX;
    [SerializeField] private GrenadeTrajectorySystem grenadeTrajectorySystem;
    private bool isExploded;
    #endregion

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
        rb2d = GetComponent<Rigidbody2D>();
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        grenadeTrajectorySystem = GameObject.Find("------ PLAYER ------").transform.GetComponentInChildren<GrenadeTrajectorySystem>();

        explodeVFX = Resources.Load<GameObject>("Prefabs/GrenadeExplosiveVFX");
    }

    private void Start()
    {
        listOfTrajectoryPoint = grenadeTrajectorySystem.grenadeTrajectory.listOfTrajectoryPoint;
    }

    private void Update()
    {
        StartCoroutine("GrenadeFly");
    }

    IEnumerator GrenadeFly()
    {
        for (int i = 0; i < listOfTrajectoryPoint.Count; i++)
        {
            transform.position = listOfTrajectoryPoint[i];
            yield return new WaitForSeconds(flyTime);
        }

        grenadeTrajectorySystem.grenadeTrajectory.ClearListOfTrajectoryPoint();
        Explode();
    }

    private void Explode()
    {
        Destroy(this.gameObject);

        if (isExploded) return;

        GameObject vfx = Instantiate(explodeVFX);
        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;
        isExploded = true;
    }
}
