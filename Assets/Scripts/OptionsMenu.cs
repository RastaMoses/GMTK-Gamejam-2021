using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public void SetMusic(float Music)
    {
        musicMixer.SetFloat("Music", Music);
    }
    public void SetSfx(float SFX)
    {
        sfxMixer.SetFloat("SFX", SFX);
    }
}
