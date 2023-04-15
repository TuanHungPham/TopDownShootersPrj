using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region public var
    public Vector3 startPoint;
    public Vector3 endPoint;

    public float time;
    public float angle;
    public float vel;
    #endregion

    #region private var
    [SerializeField] private GameObject explodeVFX;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private GrenadeTrajectorySystem grenadeTrajectorySystem;
    private Vector3 gravity = new Vector3(0, -9.81f);
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
        startPoint = playerCtrl.grenadeSystem.throwingPoint.position;
        endPoint = grenadeTrajectorySystem.lastPredictPosition;
    }

    private void Update()
    {
        StartCoroutine("GrenadeFly");
    }

    IEnumerator GrenadeFly()
    {
        float distance = Vector3.Distance(startPoint, endPoint);

        Vector3 lastPoint = new Vector3(endPoint.x - startPoint.x, endPoint.y - startPoint.y);

        float newHeight;
        PhysicExtension.CalculateHeight(startPoint, endPoint, distance, 0, out newHeight);

        gravity = new Vector3(0, -9.81f);
        float g = gravity.magnitude;

        PhysicExtension.CalculatePathWithHeight(lastPoint, g, newHeight, out time, out angle, out vel);

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float x = startPoint.x + PhysicExtension.GetX(t, vel, angle);
            float y = startPoint.y + PhysicExtension.GetY(g, t, vel, angle);

            transform.position = new Vector3(x, y);

            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        GameObject vfx = Instantiate(explodeVFX);
        vfx.transform.position = transform.position;
        vfx.transform.rotation = transform.rotation;

        Destroy(this.gameObject);
    }
}
