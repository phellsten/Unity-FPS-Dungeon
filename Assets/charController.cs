// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charController : MonoBehaviour {
	public float baseSpeed = 0.1f;
	public float speed = 0.1f;
	public float jumpSpeed = 7.0f;
	public Rigidbody rb;
    public AudioClip walkSound;
    public AudioClip sprintSound;
    public bool grounded = false;

    private bool moving = false;
    private bool stopped = false;
    private AudioSource audioSource;


	void Start () {
		rb = GetComponent<Rigidbody> ();
        audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate() {
        if (Time.timeScale == 0.0f)
        {
            audioSource.Stop();
        }

        if (stopped)
        {
            return;
        }
		if(GameObject.Find("Player").GetComponent<PlayerHealthManager>().gameWon) {
			return;
		}

        groundedCheck ();

		// Can jump if we're on the ground and press space.
		if (Input.GetKey (KeyBindings.JumpKey) && grounded) {
			rb.velocity = new Vector3();
            audioSource.Stop();
            if (Input.GetKey (KeyBindings.CrouchKey)) {
				// Crouch jumping
				speed = baseSpeed;
				rb.AddForce (transform.up * jumpSpeed * 1.15f, ForceMode.Impulse);
			} else {
				rb.AddForce (transform.up * jumpSpeed, ForceMode.Impulse);
			}
		}	
		// Movement keys with WASD
		if(Input.GetKey(KeyBindings.RightKey)) {
			transform.Translate(Vector3.Normalize(Vector3.right * Time.deltaTime) * speed);
            moving = true;
        }
		if(Input.GetKey(KeyBindings.LeftKey)) {
			
			transform.Translate(Vector3.Normalize(-1.0f * Vector3.right * Time.deltaTime) * speed);
            moving = true;
        }
		if(Input.GetKey(KeyBindings.ForwardKey)) {
			transform.Translate(Vector3.Normalize(Vector3.forward * Time.deltaTime) * speed);
            moving = true;
        }
		if(Input.GetKey(KeyBindings.BackwardKey)) {
			transform.Translate(Vector3.Normalize(-1.0f * Vector3.forward * Time.deltaTime) * speed);
            moving = true;
        }

        if(!Input.GetKey(KeyBindings.LeftKey) && !Input.GetKey(KeyBindings.ForwardKey)
            && !Input.GetKey(KeyBindings.BackwardKey) && !Input.GetKey(KeyBindings.RightKey))
        {
            moving = false;
        }

		if(!Input.GetKey(KeyBindings.CrouchKey) && grounded) {
			speed = baseSpeed;
		}
		// Holding shift to run faster
		if(Input.GetKey(KeyBindings.SprintKey) && grounded) {
			speed = 2 * baseSpeed;
		}
		if(!Input.GetKey(KeyBindings.SprintKey) && grounded) {
			speed = baseSpeed;
		}
		if(Input.GetKey(KeyBindings.CrouchKey) && grounded) {
			speed = baseSpeed / 2;
		}

        groundedCheck();

        if(moving)
        {
            if (!Input.GetKey(KeyBindings.SprintKey))
            {
                audioSource.clip = walkSound;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            if (Input.GetKey(KeyBindings.SprintKey))
            {
                audioSource.clip = sprintSound;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

        }
        else
        {
            audioSource.Stop();
        }

        if(!grounded)
        {
            audioSource.Stop();
        }

	}

	// Upon player colliding with an object, update grounded status.
    void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.tag == "Lava") {
			this.GetComponent<PlayerHealthManager> ().Death();
		}
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

    public void StopMovement()
    {
        stopped = true;
    }
}