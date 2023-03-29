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

    private void Update()
    {
        EnableComponents();
    }

    private void EnableComponents()
    {
        if (!this.gameObject.activeSelf) return;

        enemyStatus.enabled = true;
        enemyCombat.enabled = true;
        enemyMovement.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.Find("EnemySprite").GetChild(0).gameObject.SetActive(true);

    }

    public void DisableComponents()
    {
        enemyCombat.enabled = false;
        enemyMovement.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.Find("EnemySprite").GetChild(0).gameObject.SetActive(false);
    }
}
