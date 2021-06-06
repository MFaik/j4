using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSource : MonoBehaviour
{
    public static int volume = 50;

    public static GameObject instance;

    public static AudioSource audioSource;

    public static AudioClip[] clips;

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

        clips = new AudioClip[]
        {
            Resources.Load("SFX/1") as AudioClip,
            Resources.Load("SFX/2") as AudioClip,
            Resources.Load("SFX/3") as AudioClip,
            Resources.Load("SFX/4") as AudioClip
        };
    }
    
    void Update()
    {
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume/100f;

    }

    public static void PlaySFX(int i)
    {
        if(PlayerPrefs.GetInt("Sound",1) == 0)
            return;
        audioSource.PlayOneShot(clips[i]);
    }
}
