using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controlMenuMeta : MonoBehaviour {
	public Button backButton;
	// Use this for initialization
	void Start () {
		backButton.onClick.AddListener (Back);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Back() {
		SceneManager.LoadScene ("UI");

	}
}
