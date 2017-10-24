using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	void Start () {
		// Destroy object after 1 second.
		Destroy(this.gameObject, 1.0f);
	}

	void Explode() {
		ParticleSystem particle = GetComponent<ParticleSystem>();
		particle.Play ();

	}
}
