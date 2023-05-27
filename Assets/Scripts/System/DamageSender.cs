using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    #region public var
    public int dmgSending;
    public int rawDmg;
    #endregion

    #region private var
    [SerializeField] private float dealDmgDistance;
    [SerializeField] private string targetTag;
    [SerializeField] private Transform player;
    [SerializeField] private Transform thisEnemy;
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
        thisEnemy = transform.parent;

        dmgSending = rawDmg;
    }

    private void SendDamage()
    {
        float distance = Vector2.Distance(thisEnemy.position, player.position);

        if (distance > dealDmgDistance) return;

        DamageReceiver damageReceiver = player.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        damageReceiver.ReceiveDamage(dmgSending);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            DamageReceiver damageReceiver = collision.GetComponent<DamageReceiver>();
            if (damageReceiver == null) return;

            damageReceiver.ReceiveDamage(dmgSending);
        }
    }
}
