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
}