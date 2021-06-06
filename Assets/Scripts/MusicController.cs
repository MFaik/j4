using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private bool powered = false;
    private float interactDelay = 0f;
    private float volume = .5f;
    private SpriteRenderer sr;
    [SerializeField] float delay = 0.5f;
    [SerializeField] Sprite[] sprites;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        volume = PlayerPrefs.GetFloat("Music", .5f);
        sr.sprite = sprites[volume==0?1:0];
        powered = volume > 0 ? false : true;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactDelay > 0)
        {
            interactDelay -= Time.deltaTime;
        }
    }

    public void Interact()
    {
        if (interactDelay <= 0)
        {
            interactDelay = delay;
            powered = !powered;
            sr.sprite = sprites[powered ? 1 : 0];
            MusicSource.ChangeVolume(powered ? 0f : .5f);
        }
    }
}
