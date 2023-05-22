using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    #region private var
    [SerializeField] private AudioSource audioSource;
    #endregion

    private void Start()
    {
        DestroyAfterAudioEnded();
    }

    private void DestroyAfterAudioEnded()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(this.gameObject, audioSource.clip.length);

        if (audioSource.clip == null)
        {
            Destroy(this.gameObject);
        }
    }
}
