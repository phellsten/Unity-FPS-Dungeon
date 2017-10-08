// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charController : MonoBehaviour {

	public float speed = 5.0f;
	public float jumpSpeed = 5.0f;
	public Rigidbody rb;
    public bool grounded = false;

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

		// Holding shift to run faster
		if(Input.GetKey(KeyCode.LeftShift)) {
			speed = 8.0f;
		}
		if(!Input.GetKey(KeyCode.LeftShift)) {
			speed = 5.0f;
		}
    }

	void FixedUpdate() {
		// Can jump if we're on the ground and press space.
		if (Input.GetKey (KeyCode.Space) && grounded) {
			rb.velocity += new Vector3(0,jumpSpeed,0);
		}
	}

	// Check if player is on the ground.
    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}