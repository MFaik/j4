using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundTrigger : MonoBehaviour
{
    PlayerController playerController;
    void Start() {
        playerController = GetComponentInParent<PlayerController>();
    }
    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Ground"))
            playerController.ResetGroundTimer();
    }
}
