using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private AudioSource explosionAudio;
    [SerializeField] private AudioClip explosionSound;
    #endregion

    private void Awake()
    {
        explosionAudio = GetComponent<AudioSource>();
        explosionSound = Resources.Load<AudioClip>("Audio/Explosion");

        SetExplosionSound();
    }

    void Start()
    {
        DestroyAfterSoundEnded();
        DestroyAfterAnimationEnded();
    }

    private void DestroyAfterAnimationEnded()
    {
        if (explosionAudio != null) return;
        Destroy(gameObject, this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void DestroyAfterSoundEnded()
    {
        if (explosionAudio == null) return;

        PlayExplosionSound();
        Destroy(gameObject, explosionAudio.clip.length);
    }

    private void SetExplosionSound()
    {
        if (explosionAudio == null) return;
        explosionAudio.clip = explosionSound;
    }

    private void PlayExplosionSound()
    {
        explosionAudio.Play();
    }
}
