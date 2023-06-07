using UnityEngine;
using MarchingBytes;

public class DestroyAfterAnimation : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private AudioSource explosionAudio;
    [SerializeField] private AudioClip explosionSound;
    #endregion

    private void OnEnable()
    {
        explosionAudio = GetComponent<AudioSource>();
        explosionSound = Resources.Load<AudioClip>("Audio/Explosion");

        SetExplosionSound();

        DestroyAfterSoundEnded();
        DestroyAfterAnimationEnded();
    }

    private void DestroyAfterAnimationEnded()
    {
        if (explosionAudio != null) return;

        Invoke(nameof(DisableObj), this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void DestroyAfterSoundEnded()
    {
        if (explosionAudio == null) return;

        PlayExplosionSound();

        Invoke(nameof(DisableObj), explosionAudio.clip.length);
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

    public void DisableObj()
    {
        EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
    }

    public void DestroyParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
