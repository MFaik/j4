using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1.5f;
    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelAnimation(levelName));
    }

    public void Finish(int nextLevel)
    {
        PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        SceneManager.LoadScene("level"+nextLevel);
    }

    IEnumerator LoadLevelAnimation(string levelName)
    {
        animator.SetTrigger("close");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
