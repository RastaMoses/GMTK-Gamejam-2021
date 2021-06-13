using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioClip explosionClip;
    [SerializeField] AudioClip[] stickClips;
    [SerializeField] AudioClip cowClip;
    [SerializeField] AudioClip saveClip;
    [SerializeField] AudioClip ejectClip;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ExplosionSFX()
    {
        AudioSource.PlayClipAtPoint(explosionClip, transform.position);
    }

    public void StickSFX()
    {
        var i = Random.Range(0, stickClips.Length);
        var stickClip = stickClips[i];
        audioSource.clip = stickClip;
        audioSource.Play();
    }

    public void CowSFX()
    {
        audioSource.clip = cowClip;
        audioSource.Play();
    }
    public void SaveSFX()
    {
        audioSource.clip = saveClip;
        audioSource.Play();
    }

    public void EjectSFX()
    {
        audioSource.clip = ejectClip;
        audioSource.Play();
    }
}
