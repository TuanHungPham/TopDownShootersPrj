using UnityEngine;

public class MenuSoundSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip themeSong;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        PlayThemeSong();
    }

    private void LoadComponents()
    {
        audioSource = GetComponent<AudioSource>();
        themeSong = Resources.Load<AudioClip>("Audio/Hidden-Agenda");
    }

    private void PlayThemeSong()
    {
        audioSource.clip = themeSong;
        audioSource.Play();
        audioSource.loop = true;
    }
}
