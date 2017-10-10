// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class shootingScript : MonoBehaviour {
	public GameObject blood;
	public GameObject muzzle;
	public GameObject source;
	public GameObject cursor;

	private Vector3 aimPos = new Vector3 (0f, -0.288f, 0.682f);
	private Vector3 normalPos = new Vector3 (0.499f, -0.414f, 0.806f);

	private float gunMoveSpeed = 5f;
	private float muzzleOffset = 1f;
	private float rayDistance = 200f;

    private float zoomFov = 70f;
    private float normFov = 90f;
    private float fovMoveSpeed = 500f;

    private float yNormalHeight = -0.414f;
    private Quaternion recoilRot = new Quaternion(0f, 0.9659f, -0.2588f, 0f);

    private Camera mainCam;

    private Text ammoDisplay;
    private int ammoCount;

    private void Start()
    {
        this.mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.ammoDisplay = GameObject.FindGameObjectWithTag("Ammo").GetComponent<Text>();
        this.ammoCount = 8;
        
    }

    // Update is called once per frame
    void Update () {
		// Game is paused, don't shoot.
		if (Time.timeScale == 0.0f) {
			return;
		}

        // Reloading
        if(Input.GetKey(KeyCode.R))
        {
            //toDo : Reloading animation
            ammoCount += 1;
            this.ammoDisplay.text = "Ammo: " + ammoCount;
        }
        
        if(this.transform.localPosition.y > yNormalHeight)
        {
            // Return gun to normal height after recoil
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, new Vector3(this.transform.localPosition.x, yNormalHeight, this.transform.localPosition.z), 5f * Time.deltaTime);
        }

        if (this.transform.localRotation != new Quaternion(0f, 1f, 0f, 0f))
        {
            // Return gun to normal rotation after recoil
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, new Quaternion(0f,1f,0f,0f), 10f * Time.deltaTime);
        }

        // Shooting
		if (Input.GetMouseButtonDown(0) && ammoCount > 0) {
            ammoCount -= 1;
            this.ammoDisplay.text = "Ammo: " + ammoCount;

            // Create muzzle flash
            Instantiate(muzzle, this.transform.position - muzzleOffset * this.transform.forward, new Quaternion());

			// Create raycast forwards from player
			Ray ray = new Ray(source.transform.position, source.transform.forward);
			RaycastHit hit;
			bool raycasthit = Physics.Raycast(ray, out hit, rayDistance);
            
            // Recoil animation
            //this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, new Vector3(this.transform.localPosition.x, 0f, this.transform.localPosition.z), 25f * Time.deltaTime);
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, recoilRot, 10f * Time.deltaTime);

			if (raycasthit) {
				// Ray hit enemy
				if (hit.collider.tag == "EnemyMonster" || hit.collider.tag == "EnemySkeleton") {
					// Create blood effect, 
					Debug.DrawLine(ray.origin, hit.point, Color.yellow);
					Instantiate (blood, hit.point, new Quaternion ());
                    if (hit.collider.tag == "EnemyMonster")
                    {
                        hit.collider.gameObject.GetComponent<MonsterController>().health -= 1;
                    }
                    else if (hit.collider.tag == "EnemySkeleton")
                    {
                        hit.collider.gameObject.GetComponent<SkeletonController>().health -= 1;
                    }
				}
			}
		}

		// Holding right click, move gun to aim down sights.
		if (Input.GetMouseButton (1)) {
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, aimPos, gunMoveSpeed * Time.deltaTime);
			cursor.GetComponentInChildren<Canvas> ().enabled = false;

            if (mainCam.fieldOfView >= zoomFov) { 
                mainCam.fieldOfView -= fovMoveSpeed * Time.deltaTime;
            }
        }

		// Right click not pressed, keep gun in normal position.
		if (!Input.GetMouseButton (1)) {
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, normalPos, gunMoveSpeed * Time.deltaTime);
			cursor.GetComponentInChildren<Canvas> ().enabled = true;

            if (mainCam.fieldOfView <= normFov)
            {
                mainCam.fieldOfView += fovMoveSpeed * Time.deltaTime;
            }
        }
        
        // Bound field of view
        if (mainCam.fieldOfView > normFov)
        {
            mainCam.fieldOfView = normFov;
        }

        if(mainCam.fieldOfView < zoomFov)
        {
            mainCam.fieldOfView = zoomFov;
        }
	}
}
