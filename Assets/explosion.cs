using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Explode() {
		ParticleSystem particle = GetComponent<ParticleSystem>();
		particle.Play ();
		Destroy(gameObject, particle.duration);
	}
}
