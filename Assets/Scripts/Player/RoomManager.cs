using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RoomManager : MonoBehaviour
{
    public GameObject Room1;
    public GameObject Room2;

    bool room1Active = true;

    bool isPlaying = false;

    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    public void StartSpin()
    {
        if(isPlaying)
            return;
        isPlaying = true;
        Room1.transform.DORotate(new Vector3(0,90,0),.2f).OnComplete(TeleportPlayer);
        Room2.transform.DORotate(new Vector3(0,90,0),.2f);
        player.SetActive(false);
    }
    void TeleportPlayer()
    {
        if(room1Active)
            player.transform.position += Room2.transform.position-Room1.transform.position;
        else 
            player.transform.position += Room1.transform.position-Room2.transform.position;
        room1Active = !room1Active;
        EndSpin();
    }
    void EndSpin()
    {
        Room1.transform.rotation = Quaternion.Euler(0,270,0);
        Room2.transform.rotation = Quaternion.Euler(0,270,0);
        Room1.transform.DORotate(new Vector3(0,0,0),.2f).SetDelay(0.1f).OnComplete(EnablePlayer);
        Room2.transform.DORotate(new Vector3(0,0,0),.2f).SetDelay(0.1f);
    }
    void EnablePlayer()
    {
        isPlaying = false;
        player.SetActive(true);
    }
}
