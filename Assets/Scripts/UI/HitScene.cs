using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScene : MonoBehaviour
{
    [SerializeField] private PlayerCtrl playerCtrl;

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
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        TriggerHitScene();
    }

    private void TriggerHitScene()
    {
        if (playerCtrl.damageReceiver.IsHit)
        {
            gameObject.SetActive(true);
            return;
        }

        gameObject.SetActive(false);
    }
}
