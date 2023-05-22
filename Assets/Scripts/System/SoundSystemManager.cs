using UnityEngine;

public class SoundSystemManager : MonoBehaviour
{
    private static SoundSystemManager instance;

    public static SoundSystemManager Instance { get => instance; private set => instance = value; }

    #region public var
    #endregion

    #region private var
    [SerializeField] private AudioSource itemPickUpAudioSource;
    [SerializeField] private AudioSource weaponPickUpAudioSource;
    [SerializeField] private AudioClip coinAudio;
    [SerializeField] private AudioClip reloadAudio;
    #endregion

    private void Awake()
    {
        instance = this;

        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        itemPickUpAudioSource = transform.Find("ItemPickUpAudioSource").GetComponent<AudioSource>();
        weaponPickUpAudioSource = transform.Find("WeaponPickUpAudioSource").GetComponent<AudioSource>();
        coinAudio = Resources.Load<AudioClip>("Audio/collectcoin");
        reloadAudio = Resources.Load<AudioClip>("Audio/reload");
    }

    public void SetCoinSound()
    {
        itemPickUpAudioSource.clip = coinAudio;
        itemPickUpAudioSource.Play();
    }

    public void SetReloadSound()
    {
        weaponPickUpAudioSource.clip = reloadAudio;
        weaponPickUpAudioSource.Play();
    }
}
