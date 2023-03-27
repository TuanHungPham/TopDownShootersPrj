using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public EnemyBehaviour enemyBehaviour;
    public Status enemyStatus;
    public EnemyCombat enemyCombat;

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
        enemyStatus = GetComponent<Status>();
        enemyMovement = GetComponentInChildren<EnemyMovement>();
        enemyBehaviour = GetComponentInChildren<EnemyBehaviour>();
        enemyCombat = GetComponentInChildren<EnemyCombat>();
    }
}
