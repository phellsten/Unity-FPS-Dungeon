// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charController : MonoBehaviour {
	public float baseSpeed = 0.1f;
	public float speed = 0.1f;
	public float jumpSpeed = 7.0f;
	public Rigidbody rb;
    public bool grounded = false;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		groundedCheck ();
		// Can jump if we're on the ground and press space.
		if (Input.GetKey (KeyCode.Space) && grounded) {
			rb.velocity = new Vector3();
			if (Input.GetKey (KeyCode.LeftControl)) {
				// Crouch jumping
				speed = baseSpeed;
				rb.AddForce (transform.up * jumpSpeed * 1.15f, ForceMode.Impulse);
			} else {
				rb.AddForce (transform.up * jumpSpeed, ForceMode.Impulse);
			}
		}	
		// Movement keys with WASD
		if(Input.GetKey(KeyCode.D)) {
			transform.Translate(Vector3.Normalize(Vector3.right * Time.deltaTime) * speed);
		}
		if(Input.GetKey(KeyCode.A)) {
			
			transform.Translate(Vector3.Normalize(-1.0f * Vector3.right * Time.deltaTime) * speed);
		}
		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(Vector3.Normalize(Vector3.forward * Time.deltaTime) * speed);
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.Translate(Vector3.Normalize(-1.0f * Vector3.forward * Time.deltaTime) * speed);
		}
		if(!Input.GetKey(KeyCode.LeftControl) && grounded) {
			speed = baseSpeed;
		}
		// Holding shift to run faster
		if(Input.GetKey(KeyCode.LeftShift) && grounded) {
			speed = 2 * baseSpeed;
		}
		if(!Input.GetKey(KeyCode.LeftShift) && grounded) {
			speed = baseSpeed;
		}
		if(Input.GetKey(KeyCode.LeftControl) && grounded) {
			speed = baseSpeed / 2;
		}

	}

	// Upon player colliding with an object, update grounded status.
    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;

		// Player sometimes not colliding with ground eg on a slope,
		// Do a ray trace to double check.
		RaycastHit ray;
		if (Physics.Raycast (transform.position, Vector3.down, out ray, 1.2f)) {
			grounded = true;
		}
    }

	// Check if player is on the ground.
	private void groundedCheck() {
		RaycastHit ray;
		if (Physics.Raycast (transform.position, Vector3.down, out ray, 1.2f)) {
			grounded = true;
		} else {
			grounded = false;
		}
	}
}