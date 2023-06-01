using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private GameObject weaponAudioSource;

    [Space(20)]
    [SerializeField] private AudioSource movingAudio;
    [SerializeField] private AudioSource hittingAudio;

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
        weaponAudioSource = Resources.Load<GameObject>("Prefabs/WeaponSound");

        hittingAudio = GetComponent<AudioSource>();
        movingAudio = transform.parent.Find("PlayerMovement").GetComponent<AudioSource>();

        footStepSound = Resources.Load<AudioClip>("Audio/footstep");
        hitSound = Resources.Load<AudioClip>("Audio/player_hit");

        SetupHitSound();
        SetupMovingSound();
    }

    private void Update()
    {
        GetWeaponSound();
    }

    private void GetWeaponSound()
    {
        weaponSound = playerCtrl.PlayerWeaponSystem.PlayerShootingSystem.Weapon.WeaponData.WeaponSound;
    }

    public void CreateWeaponAudioSource()
    {
        GameObject audioSource = Instantiate(weaponAudioSource);
        audioSource.transform.SetParent(transform);

        AudioSource source = audioSource.GetComponent<AudioSource>();
        source.clip = weaponSound;
        source.Play();
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
}
