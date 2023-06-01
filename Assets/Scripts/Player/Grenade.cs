using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region public var
    public List<Vector3> listOfTrajectoryPoint = new List<Vector3>();
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private GameObject explodeVFX;
    [SerializeField] private GrenadeTrajectorySystem grenadeTrajectorySystem;
    private bool isExploded;
    private Vector3 velocity = Vector3.zero;
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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        grenadeTrajectorySystem = GameObject.Find("------ PLAYER ------").transform.GetComponentInChildren<GrenadeTrajectorySystem>();

        explodeVFX = Resources.Load<GameObject>("Prefabs/GrenadeExplosiveVFX");
    }

    private void Start()
    {
        listOfTrajectoryPoint = grenadeTrajectorySystem.GrenadeTrajectory.listOfTrajectoryPoint;
        StartCoroutine("GrenadeFly");
    }

    IEnumerator GrenadeFly()
    {
        for (int i = 0; i < listOfTrajectoryPoint.Count; i++)
        {
            transform.position = listOfTrajectoryPoint[i];
            yield return null;
        }

        grenadeTrajectorySystem.GrenadeTrajectory.ClearListOfTrajectoryPoint();
        Explode();
    }

    private void Explode()
    {
        Destroy(this.gameObject);

        if (isExploded) return;

        GameObject vfx = Instantiate(explodeVFX);
        vfx.transform.position = transform.position;
        isExploded = true;
    }
}
