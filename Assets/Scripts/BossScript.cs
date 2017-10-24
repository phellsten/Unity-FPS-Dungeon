using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("Victory").GetComponent<RawImage> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void death() {
		GameObject.Find ("Victory").GetComponent<RawImage> ().enabled = true;
		GameObject.Find("Player").GetComponent<PlayerHealthManager>().gameWon = true;
		GameObject.Find ("Player").GetComponent<PlayerHealthManager> ().win ();

	}
}
