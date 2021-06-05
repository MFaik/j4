using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public int level;
    [SerializeField]bool lastLevel;

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void Finish()
    {
        if (PlayerPrefs.GetInt("UnlockedLevel") < level + 1)
        {
            PlayerPrefs.SetInt("UnlockedLevel", level + 1);
        }
        if(!lastLevel)
            LoadLevel("Level"+(level+1));
        else
            LoadLevel("LevelSelector");
    }
}
