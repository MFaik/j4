using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void Finish(int nextLevel)
    {
        PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        SceneManager.LoadScene("level"+nextLevel);
    }
}
