// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charController : MonoBehaviour {

	public float speed = 5.0f;
	public float jumpSpeed = 5.0f;
	public Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		// Movement keys with WASD
		if(Input.GetKey(KeyCode.D)) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A)) {
			transform.Translate(-Vector3.right * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.Translate(-Vector3.forward * speed * Time.deltaTime);
		}
	}

	void FixedUpdate() {
		// Can jump if we're on the ground and press space.
		if (Input.GetKey (KeyCode.Space) && isGrounded()) {
			rb.velocity += new Vector3(0,jumpSpeed,0);
		}
	}

	// Check if player is on the ground.
	bool isGrounded() {
		RaycastHit ray;

		// Use a ray cast from the player directly down at a distance of 1 to check if we're on the ground.
		if (Physics.Raycast (transform.position, Vector3.down, out ray, 1f)) {
			return true;
		}
		else {
			return false;
		}
	}
}