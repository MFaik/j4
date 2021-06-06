using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource : MonoBehaviour
{
    public static int volume = 50;

    public static GameObject instance;

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Update()
    {
        audioSource.volume = volume/100f;
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
