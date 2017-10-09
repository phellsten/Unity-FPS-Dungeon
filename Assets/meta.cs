using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class meta : MonoBehaviour {
	public GameObject pauseMenu;
	public Button resumeGame;
	public Button returnMenu;

	void Start () {
		ResumeGame ();

		// Set up pause menu button methods
		resumeGame.onClick.AddListener (ResumeGame);
		returnMenu.onClick.AddListener (QuitGame);
	}
	
	// Update is called once per frame
	void Update () {
		// Escape to open menu
		if (Input.GetKeyDown ("escape")) {
			if (Time.timeScale == 0.0f) {
				// Resume Game
				ResumeGame ();

			} else {
				// Pause Game
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				Time.timeScale = 0.0f;
				pauseMenu.GetComponentInChildren<Canvas> ().enabled = true;
			}
		}
	}

	void ResumeGame() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1.0f;
		pauseMenu.GetComponentInChildren<Canvas> ().enabled = false;
	}

	void QuitGame() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("UI");
	}
}
