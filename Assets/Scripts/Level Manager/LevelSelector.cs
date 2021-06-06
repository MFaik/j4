using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    int UnlockedLevel;

    public LevelManager manager;
    public LevelDoor[] doors;

    // Start is called before the first frame update
    void Start()
    {
        UnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].locked = true;
        }

        for (int i = 0; i < UnlockedLevel; i++)
        {
            doors[i].locked = false;
        }
    }

    public void DeleteProgress()
    {
        UnlockedLevel = 1;
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        manager.LoadLevel("levelSelector");
    }

}
