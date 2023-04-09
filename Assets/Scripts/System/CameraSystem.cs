using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    #region public var
    public float camSpeed;
    #endregion

    #region private var
    [SerializeField] private Transform player;
    [SerializeField] private Transform crosshair;
    [SerializeField] private Vector3 midPoint;
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
        player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
        crosshair = GameObject.Find("------ PLAYER ------").transform.Find("AimingSystem").GetChild(0);
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        GetMidPoint();

        transform.position = Vector3.SmoothDamp(transform.position, midPoint - new Vector3(0.5f, 0.5f, 0.5f), ref velocity, camSpeed);
    }

    private void GetMidPoint()
    {
        midPoint = (crosshair.position + player.position) / 2;
        midPoint.z = -10;
    }

}
