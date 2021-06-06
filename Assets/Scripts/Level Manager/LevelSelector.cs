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

        for (int i = 0; i < UnlockedLevel; i++)
        {
            doors[i].locked = false;
        }

        for (int i = UnlockedLevel; i < doors.Length; i++)
        {
            doors[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            doors[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void DeleteProgress()
    {
        UnlockedLevel = 1;
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        manager.LoadLevel("levelSelector");
    }

}
