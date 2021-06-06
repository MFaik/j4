using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundTrigger : MonoBehaviour
{

    [SerializeField] GameObject GroundParticle;

    PlayerController playerController;
    void Start() {
        playerController = GetComponentInParent<PlayerController>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Ground")|| other.CompareTag("Piston"))
        {
            Instantiate(GroundParticle,transform.position,Quaternion.identity);
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Ground")|| other.CompareTag("Piston"))
        {
            playerController.RestartGroundTimer();
        }
    }
}
