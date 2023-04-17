using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSystem : MonoBehaviour
{
    #region public var
    public float throwTimer;
    public float throwDelay;
    public Transform throwingPoint;
    #endregion

    #region private var
    [SerializeField] private GrenadeTrajectorySystem grenadeTrajectorySystem;
    [SerializeField] private GameObject grenadePrefab;
    private bool isThrowing;
    private bool isDelay;
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
        grenadeTrajectorySystem = transform.parent.Find("GrenadeTrajectorySystem").GetComponent<GrenadeTrajectorySystem>();
        throwingPoint = transform.parent.Find("MainCharacter").Find("ThrowingPoint");
        grenadePrefab = Resources.Load<GameObject>("Prefabs/Grenade");

        throwDelay = 2;
        throwTimer = throwDelay;
    }

    private void Update()
    {
        CheckThrowTimer();
        ThrowGrenade();
    }

    public void ThrowGrenade()
    {
        if (!CanThrow() || isThrowing) return;

        GameObject grenade = Instantiate(grenadePrefab);
        grenade.transform.position = throwingPoint.position;
        grenade.transform.rotation = throwingPoint.rotation;

        isThrowing = true;
        throwTimer = throwDelay;
    }

    private void CheckThrowTimer()
    {
        if (throwTimer <= 0)
        {
            isDelay = false;
            isThrowing = false;
            return;
        }

        isDelay = true;
        throwTimer -= Time.deltaTime;
    }

    private bool CanThrow()
    {
        if (grenadeTrajectorySystem.grenadeTrajectory.listOfTrajectoryPoint.Count == 0 || grenadeTrajectorySystem.IsAreaActive || isDelay) return false;

        return true;
    }
}
