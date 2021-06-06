using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource : MonoBehaviour
{
    public static GameObject instance;

    AudioSource audioSource;

    private void Start() {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = PlayerPrefs.GetFloat("Music",.5f);
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public static void ChangeVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("Music",volume);
        instance.GetComponent<MusicSource>().audioSource.volume = volume;
    }
    
}
