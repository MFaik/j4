using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSource : MonoBehaviour
{
    public static GameObject instance;

    AudioSource audioSource;

    AudioClip[] clips;

    private void Start() {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerPrefs.GetFloat("Sound",.5f);

        clips = new AudioClip[]
        {
            Resources.Load("SFX/jump1") as AudioClip,
            Resources.Load("SFX/jump2") as AudioClip,
            Resources.Load("SFX/jump3") as AudioClip,
            Resources.Load("SFX/dead") as AudioClip,
            Resources.Load("SFX/door") as AudioClip,
            Resources.Load("SFX/piston_in") as AudioClip,
            Resources.Load("SFX/piston_out") as AudioClip
        };
    }

    public static void ChangeVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("Sound",volume);
        instance.GetComponent<SoundSource>().audioSource.volume = volume;
    }

    void PlaySFXInstance(int i)
    {
        audioSource.PlayOneShot(clips[i]);
    }

    public static void PlaySFX(int i)
    {
        instance.GetComponent<SoundSource>().PlaySFXInstance(i);
    }
}
