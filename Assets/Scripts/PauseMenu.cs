using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;


    private void Start()
    {
        PauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;

            //Time.timeScale = 0.3; se haría un slowmotion

        }
    }

    public void Resume()
    {
        paused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}﻿