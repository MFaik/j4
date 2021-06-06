using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    RoomManager roomManager;
    
    float jumpButtonTimer = 0;
    const float jumpButtonTimerMax = .2f;
    float groundTimer = 0;
    const float groundTimerMax = .2f;

    const float jumpSpeed = 10;
    const float jumpSlowDampen = .02f;
    const float jumpFastDampen = .005f;

    const float walkAcceleration = 7*4;
    const float walkMaxSpeed = 7f;
    const float walkDampen = 0.3f;

    LevelManager levelManager;

    [SerializeField]GameObject DeathParticle;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        GameObject roomManagerObject = GameObject.FindGameObjectWithTag("RoomManager");
        roomManager = roomManagerObject.GetComponent<RoomManager>();
        GameObject levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObject.GetComponent<LevelManager>();
    }

    void Update()
    {
        //Jumping Input
        if(Input.GetButtonDown("Jump"))
        {
            jumpButtonTimer = jumpButtonTimerMax;
        }
        
        if(!Input.GetButton("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
        }
        else if(rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y-(9f)*Time.deltaTime);
        }
        //Jump Check
        if(jumpButtonTimer > 0 && groundTimer > 0)
        {
            jumpButtonTimer = 0;
            groundTimer = 0;
            spriteRenderer.transform.DOScaleY(1.2f,.1f).SetEase(Ease.OutQuad).OnComplete(()=>{spriteRenderer.transform.DOScaleY(1,.1f).SetEase(Ease.OutQuad);});
            spriteRenderer.transform.DOScaleX(.9f,.1f).SetEase(Ease.OutQuad).OnComplete(()=>{spriteRenderer.transform.DOScaleX(1,.1f).SetEase(Ease.OutQuad);});
            rb.velocity = new Vector2(rb.velocity.x,jumpSpeed);
            SoundSource.PlaySFX(Random.Range(0,3));
        }
        //Decrease Timers
        if(jumpButtonTimer > 0)
        {
            jumpButtonTimer -= Time.deltaTime;
        }
        if(groundTimer > 0)
        {
            groundTimer -= Time.deltaTime;
        }
        //Walking
        float horizontalVelocity = rb.velocity.x;

        if(Input.GetAxisRaw("Horizontal") == 0 || (horizontalVelocity > 0) != (Input.GetAxisRaw("Horizontal") > 0))
            horizontalVelocity *= walkDampen;
        if(Input.GetAxisRaw("Horizontal") > 0f)
            horizontalVelocity += walkAcceleration*Time.deltaTime;
        else if(Input.GetAxisRaw("Horizontal") < 0f)
            horizontalVelocity -= walkAcceleration*Time.deltaTime;

        if(Mathf.Abs(horizontalVelocity) > walkMaxSpeed)
        {
            horizontalVelocity = walkMaxSpeed * ((horizontalVelocity > 0) ? 1 : -1);
        }
        rb.velocity = new Vector2(horizontalVelocity,rb.velocity.y);
    }

    public void RestartGroundTimer()
    {
        groundTimer = groundTimerMax;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButton("Interact"))
        {
            if(other.CompareTag("BackDoor") && groundTimer > 0)
            {
                roomManager.StartSpin(false);
                rb.velocity = Vector2.zero;
                groundTimer = 0;
                jumpButtonTimer = 0;
                SoundSource.PlaySFX(4);
            }   
            else if(other.CompareTag("Door"))
            {
                roomManager.StartSpin(true);
                SoundSource.PlaySFX(4);
            }
            else if (other.CompareTag("Button"))
            {
                other.GetComponent<ButtonController>().Interact();
            }
            else if (other.CompareTag("Lever"))
            {
                other.GetComponent<LeverController>().Interact();
            }
            else if (other.CompareTag("LevelDoor"))
            {
                if(!other.GetComponent<LevelDoor>().locked)
                    roomManager.StartSpin(other.GetComponent<LevelDoor>().level);
            }
        }    
        if(other.CompareTag("Respawn"))
        {
            SoundSource.PlaySFX(3);
            roomManager.StartSpin("Level" + levelManager.level,true);
        }
    }

    public void Die()
    {
        Instantiate(DeathParticle,transform.position,Quaternion.identity);
        spriteRenderer.enabled = false;
        rb.isKinematic = true;
    }
}
