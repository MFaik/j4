using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    
    float jumpButtonTimer = 0;
    const float jumpButtonTimerMax = .2f;
    float groundTimer = 0;
    const float groundTimerMax = .2f;

    const float jumpSpeed = 7;
    const float jumpDampen = .5f;

    const float walkAcceleration = 7*4;
    const float walkMaxSpeed = 7f;
    const float walkDampen = 0.3f;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Jumping Input
        if(Input.GetButtonDown("Jump"))
        {
            jumpButtonTimer = jumpButtonTimerMax;
        }
        
        if(!Input.GetButton("Jump") && rigidbody.velocity.y > 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x,rigidbody.velocity.y*jumpDampen*Time.deltaTime);
        }
        //Jump Check
        if(jumpButtonTimer > 0 && groundTimer > 0)
        {
            jumpButtonTimer = 0;
            groundTimer = 0;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x,jumpSpeed);
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
        float horizontalVelocity = rigidbody.velocity.x;

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
        rigidbody.velocity = new Vector2(horizontalVelocity,rigidbody.velocity.y);
    }

    public void ResetGroundTimer()
    {
        groundTimer = groundTimerMax;
    }
}
