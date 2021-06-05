using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class RoomManager : MonoBehaviour
{
    public GameObject Room1;
    public CinemachineVirtualCamera Room1Camera;
    public GameObject Room2;
    public CinemachineVirtualCamera Room2Camera;

    bool room1Active = true;

    bool isPlaying = false;

    GameObject player;

    LevelManager levelManager;

    void Start() {
        GameObject levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObject.GetComponent<LevelManager>();

        player = GameObject.FindGameObjectWithTag("Player");    
        player.SetActive(false);
        Room1.transform.rotation = Quaternion.Euler(0,270,0);
        Room2.transform.rotation = Quaternion.Euler(0,270,0);
        Room1.transform.DORotate(new Vector3(0,0,0),.2f).SetEase(Ease.InQuad).OnComplete(()=>{player.SetActive(true);});
        Room2.transform.DORotate(new Vector3(0,0,0),.2f).SetEase(Ease.InQuad);

        
    }

    public void StartSpin(bool isEndLevel)
    {
        if(isPlaying)
            return;
        isPlaying = true;
        if(!isEndLevel)
            Room1.transform.DORotate(new Vector3(0,90,0),.2f).SetEase(Ease.OutQuad).OnComplete(TeleportPlayer);
        else
        {
            Room1.transform.DORotate(new Vector3(0,90,0),.2f).SetEase(Ease.OutQuad).OnComplete(()=>{levelManager.Finish();});
        }
        Room2.transform.DORotate(new Vector3(0,90,0),.2f).SetEase(Ease.OutQuad);
        player.SetActive(false);
    }
    void TeleportPlayer()
    {
        room1Active = !room1Active;
        if(room1Active)
        {
            player.transform.position += Room1.transform.position-Room2.transform.position;
            Room1Camera.Priority = 2;
            Room2Camera.Priority = 1;
        }
        else 
        {
            player.transform.position += Room2.transform.position-Room1.transform.position;
            Room1Camera.Priority = 1;
            Room2Camera.Priority = 2;
        }
        EndSpin();
    }
    void EndSpin()
    {
        Room1.transform.rotation = Quaternion.Euler(0,270,0);
        Room2.transform.rotation = Quaternion.Euler(0,270,0);
        Room1.transform.DORotate(new Vector3(0,0,0),.2f).SetEase(Ease.InQuad).SetDelay(.1f).OnComplete(EnablePlayer).SetDelay(.2f);
        Room2.transform.DORotate(new Vector3(0,0,0),.2f).SetEase(Ease.InQuad).SetDelay(.1f);
    }
    void EnablePlayer()
    {
        isPlaying = false;
        player.SetActive(true);
    }
}
