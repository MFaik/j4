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
        isPlaying = true;
        Room1.transform.DORotate(new Vector3(0,0,0),.2f).SetEase(Ease.InQuad).OnComplete(()=>{isPlaying = false;player.SetActive(true);});
        Room2.transform.DORotate(new Vector3(0,0,0),.2f).SetEase(Ease.InQuad);
        Room2Camera.enabled = false;
        
    }

    public void StartSpin(bool isEndLevel)
    {
        if(isPlaying)
            return;
        
        if(!isEndLevel)
        {
            //isPlaying = true;
            TeleportPlayer();
        }
        else
        {
            isPlaying = true;
            Room2.transform.DORotate(new Vector3(0,90,0),.2f).SetEase(Ease.OutQuad);
            Room1.transform.DORotate(new Vector3(0,90,0),.2f).SetEase(Ease.OutQuad).OnComplete(()=>{levelManager.Finish();});
            player.SetActive(false);
        }
    }

    public void StartSpin(string customLevel, bool die = false)
    {
        if (die)
        {
            player.GetComponent<PlayerController>().Die();
            Room1.transform.DORotate(new Vector3(0, 90, 0), .2f).SetEase(Ease.OutQuad).SetDelay(2f);
            Room2.transform.DORotate(new Vector3(0, 90, 0), .2f).SetEase(Ease.OutQuad).SetDelay(2f).OnComplete(() => { levelManager.LoadLevel(customLevel); });
        }
        else
        {
            Room1.transform.DORotate(new Vector3(0, 90, 0), .2f).SetEase(Ease.OutQuad);
            Room2.transform.DORotate(new Vector3(0, 90, 0), .2f).SetEase(Ease.OutQuad).OnComplete(() => { levelManager.LoadLevel(customLevel); });
        }
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
            Room1Camera.transform.position = Room2Camera.transform.position + Room1.transform.position-Room2.transform.position;
            Room2Camera.enabled = false;
            Room1Camera.enabled = true;
        }
        else 
        {
            player.transform.position += Room2.transform.position-Room1.transform.position;
            Room1Camera.Priority = 1;
            Room2Camera.Priority = 2;
            Room2Camera.transform.position = Room1Camera.transform.position + Room2.transform.position-Room1.transform.position;
            Room2Camera.enabled = true;
            Room1Camera.enabled = false;
        }
        //Invoke(nameof(EndSpin),.2f);
    }
    void EndSpin()
    {
        isPlaying = false;
    }
    void EnablePlayer()
    {
        isPlaying = false;
        player.SetActive(true);
    }
}
