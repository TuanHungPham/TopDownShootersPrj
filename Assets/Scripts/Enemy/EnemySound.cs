using UnityEngine;

public class EnemySound : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private AudioSource enemyAudioSource;
    [SerializeField] private AudioClip roarAudio;
    [SerializeField] private AudioClip screamAudio;
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
        enemyAudioSource = GetComponent<AudioSource>();
        roarAudio = Resources.Load<AudioClip>("Audio/monsterroar");
        screamAudio = Resources.Load<AudioClip>("Audio/demonic-woman-scream");
    }

    public void SetRoarAudio()
    {
        if (transform.parent.name.Contains("Enemy3"))
        {
            enemyAudioSource.clip = screamAudio;
        }
        else
        {
            enemyAudioSource.clip = roarAudio;
        }
        Debug.Log(transform.parent.name);

        enemyAudioSource.Play();
    }

    private void OnDisable()
    {
        enemyAudioSource.clip = null;
    }
}
