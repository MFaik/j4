using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    int UnlockedLevel;

    public LevelManager manager;
    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        UnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < UnlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
        Debug.Log("Unlocked Level " + UnlockedLevel);
    }

    public void DeleteProgress()
    {
        UnlockedLevel = 1;
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        manager.LoadLevel("levelSelector");
    }

}
