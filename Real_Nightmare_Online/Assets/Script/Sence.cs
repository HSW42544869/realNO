﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sence : MonoBehaviour
{
    public void GO()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);                          
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
        plsyermovement.live = 100;

    }
    public void QuitGame()
    {
        //Debug.Log("退出遊戲");
        Application.Quit();
    }
    public void Developer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
