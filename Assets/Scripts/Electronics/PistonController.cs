using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    [SerializeField]bool poweredAtStart;
    bool state = false;
    [SerializeField] PowerSource[] powers;
    private SpriteRenderer sr;
    private BoxCollider2D col;
    [SerializeField] Sprite[] sprites;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        if(poweredAtStart)
        {
            col.enabled = true;
            sr.sprite = sprites[1];
            state = true;
        }
    }

    public void refresh()
    {
        bool isPowered = true;
        foreach(PowerSource power in powers)
        {
            isPowered = isPowered && power.powered;
        }
        if (isPowered && !state)
        {
            turnOn();
        }
        else if (!isPowered && state)
        {
            turnOff();
        }
    }

    private void turnOn()
    {
        col.enabled = true;
        sr.sprite = sprites[1];
        state = true;
        SoundSource.PlaySFX(6);
    }

    private void turnOff()
    {
        col.enabled = false;
        sr.sprite = sprites[0];
        state = false;
        SoundSource.PlaySFX(5);
    }
    
}
