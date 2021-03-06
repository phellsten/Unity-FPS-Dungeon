﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class meta : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeGame;
    public Button returnMenu;
    private bool showMessage = true;
    private float messageDelay = 10f;

    private void Start()
    {
        ResumeGame();

        // Set up pause menu button methods
        resumeGame.onClick.AddListener(ResumeGame);
        returnMenu.onClick.AddListener(QuitGame);

        GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = "0";
    }

    // Update is called once per frame
    private void Update()
    {
        if (messageDelay >= 0f)
        {
            messageDelay -= Time.deltaTime;
        }
        else
        {
            showMessage = false;
        }
        // Escape to open menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0.0f)
            {
                // Resume Game
                ResumeGame();
            }
            else
            {
                // Pause Game
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.0f;
                pauseMenu.GetComponentInChildren<Canvas>().enabled = true;
            }
        }
    }

    private void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        pauseMenu.GetComponentInChildren<Canvas>().enabled = false;
    }

    private void QuitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnGUI()
    {
        if (showMessage)
        {
            GUIStyle g = GUI.skin.GetStyle("label");
            g.fontSize = 18;
            g.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 250, 300, 100),
                                "New Objective: Kill the Lava Demon!", g);
        }
    }
}