using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region public var
    public float moveSpeed;
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    #endregion

    #region private var
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Transform player;
    private bool isRunning;

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
        rb2d = GetComponentInParent<Rigidbody2D>();
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        player = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter");
    }

    private void FixedUpdate()
    {
        ChasePlayer();
        GetFaceDirection();
    }

    private void ChasePlayer()
    {
        if (enemyCtrl.damageReceiver.IsHit) return;

        Vector3 direction = player.position - transform.parent.position;
        direction.Normalize();
        rb2d.MovePosition(transform.parent.position + direction * moveSpeed * Time.fixedDeltaTime);
        isRunning = true;
    }

    private void GetFaceDirection()
    {
        Vector2 scale = transform.parent.localScale;

        if (player.position.x < transform.parent.position.x) 
        {
            scale.x = -1;
        }
        else if (player.position.x > transform.parent.position.x)
        {
            scale.x = 1;
        }

        transform.parent.localScale = scale;
    }
}
