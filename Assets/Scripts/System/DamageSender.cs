using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    #region public var
    public int dmgSending;
    public int rawDmg;
    public float criticalRate;
    public float criticalDmg;
    #endregion

    #region private var
    [SerializeField] private string targetTag;
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
        rawDmg = 10;
        dmgSending = rawDmg;
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
