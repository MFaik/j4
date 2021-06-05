using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(player.gameObject.activeSelf)
        {

        }
        else
        {
            
        }
    }
}
