using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region public var
    public List<Vector3> listOfTrajectoryPoint = new List<Vector3>();
    #endregion

    #region private var
    [SerializeField] private GameObject explodeVFX;
    [SerializeField] private GrenadeTrajectorySystem grenadeTrajectorySystem;
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
        explodeVFX = Resources.Load<GameObject>("Prefabs/GrenadeExplosiveVFX");
        grenadeTrajectorySystem = GameObject.Find("------ PLAYER ------").transform.GetComponentInChildren<GrenadeTrajectorySystem>();

        listOfTrajectoryPoint = grenadeTrajectorySystem.grenadeTrajectory.lastListOfPoint;
    }

    private void Start()
    {
        StartCoroutine("GrenadeFly");
    }

    private void Update()
    {
        // StartCoroutine("GrenadeFly");
        Explode();
    }

    IEnumerator GrenadeFly()
    {
        for (int i = 0; i < listOfTrajectoryPoint.Count; i++)
        {
            transform.position = listOfTrajectoryPoint[i];
            yield return null;
        }
        grenadeTrajectorySystem.grenadeTrajectory.ClearTrajectoryPointList();
    }

    private void Explode()
    {
        float distacneToLastPoint = Vector3.Distance(listOfTrajectoryPoint[listOfTrajectoryPoint.Count - 1], transform.position);
        if (distacneToLastPoint >= 0.5f) return;

        GameObject vfx = Instantiate(explodeVFX);
        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;

        Destroy(this.gameObject);
    }
}
