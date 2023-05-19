using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;

    [Space(20)]
    [SerializeField] private AudioSource movingAudio;
    [SerializeField] private AudioSource hittingAudio;
    [SerializeField] private AudioSource weaponAudio;

    [Space(20)]
    [SerializeField] private AudioClip footStepSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip weaponSound;
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
        playerCtrl = GetComponentInParent<PlayerCtrl>();

        hittingAudio = GetComponent<AudioSource>();
        movingAudio = transform.parent.Find("PlayerMovement").GetComponent<AudioSource>();
        weaponAudio = transform.parent.Find("PlayerWeapon").GetComponent<AudioSource>();


        footStepSound = Resources.Load<AudioClip>("Audio/footstep");
        hitSound = Resources.Load<AudioClip>("Audio/player_hit");

        SetupHitSound();
        SetupMovingSound();
        SetupWeaponSound();
    }

    private void Update()
    {
        GetWeaponSound();
    }

    private void GetWeaponSound()
    {
        weaponSound = playerCtrl.playerWeaponSystem.playerShootingSystem.weapon.weaponData.WeaponSound;
        weaponAudio.clip = weaponSound;
    }

    private void SetupWeaponSound()
    {
        weaponAudio.enabled = false;
        weaponAudio.loop = true;
    }

    private void SetupMovingSound()
    {
        movingAudio.enabled = false;
        movingAudio.clip = footStepSound;
        movingAudio.loop = true;
    }

    private void SetupHitSound()
    {
        hittingAudio.playOnAwake = false;
        hittingAudio.enabled = true;
        hittingAudio.clip = hitSound;
        hittingAudio.loop = false;
    }

    public void PlayHitSound()
    {
        hittingAudio.Play();
    }

    public void SetMovingSound(bool set)
    {
        movingAudio.enabled = set;
    }

    public void SetWeaponSound(bool set)
    {
        weaponAudio.enabled = set;
    }
}
