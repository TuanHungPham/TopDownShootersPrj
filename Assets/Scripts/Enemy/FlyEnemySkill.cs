using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyEnemySkill : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private EnemyCtrl enemyCtrl;
    [SerializeField] private GameObject explodeVFX;
    [SerializeField] private Rigidbody2D rb2d;
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
        explodeVFX = Resources.Load<GameObject>("Prefabs/EnemyExplosiveVFX");
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        Explode();
    }

    private void Explode()
    {
        if (!enemyCtrl.enemyCombat.IsAttacking)
        {
            return;
        }

        StopWhenExplosiveCharging();
        Invoke("DisableGameObject", 2.6f);
    }

    private void SetExplodeVFX()
    {
        GameObject vfx = Instantiate(explodeVFX);
        vfx.transform.position = transform.parent.position;
        vfx.transform.rotation = transform.parent.rotation;
    }
    
    private void StopWhenExplosiveCharging()
    {
        rb2d.velocity = Vector3.zero;
    }

    private void DisableGameObject()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        SetExplodeVFX();
    }
}
