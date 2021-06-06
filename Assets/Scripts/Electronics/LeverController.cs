using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    PowerSource source;
    [SerializeField]bool powered = false;
    private float interactDelay = 0f;
    private SpriteRenderer sr;
    [SerializeField] float delay = 0.5f;
    [SerializeField] Sprite[] sprites;
    void Start()
    {
        source = GetComponent<PowerSource>();
        sr = GetComponent<SpriteRenderer>();
        if(powered)
            sr.sprite = sprites[powered?1:0];
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
            source.switchPower(powered);
            sr.sprite = sprites[powered?1:0];
        }
    }
}
