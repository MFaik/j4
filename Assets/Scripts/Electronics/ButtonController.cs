using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    PowerSource source;
    private bool powered = false;
    private float tm=0f;
    private SpriteRenderer sr;
    [SerializeField] float pushTime = 5f;
    [SerializeField] Sprite[] sprites;
    void Start()
    {
        source = GetComponent<PowerSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tm > 0)
        {
            tm -= Time.deltaTime;
        }
        else if (powered)
        {
            powered = false;
            source.switchPower(false);
            sr.sprite = sprites[0];
        }
    }

    public void Interact()
    {
        if (tm <= 0)
        {
            tm = pushTime;
            powered = true;
            source.switchPower(true);
            sr.sprite = sprites[1];
        }
    }

}
