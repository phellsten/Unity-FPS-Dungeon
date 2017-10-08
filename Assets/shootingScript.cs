using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour {
	public GameObject blood;
	public GameObject muzzle;
	public GameObject source;

	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0.0f) {
			return;
		}
		if (Input.GetMouseButtonDown(0)) {
			// Create muzzle flash
			Instantiate(muzzle, this.transform.position - 1.5f * this.transform.forward, new Quaternion());

			// Create raycast forwards from player
			Ray ray = new Ray(source.transform.position, source.transform.forward);
			RaycastHit hit;
			bool raycasthit = Physics.Raycast(ray, out hit, 200);

			if (raycasthit) {
				// Ray hit enemy
				if (hit.collider.tag == "Enemy") {
					// Create blood effect, 
					// ToDo : Subtract HP from enemy
					Debug.DrawLine(ray.origin, hit.point, Color.yellow);
					Instantiate (blood, hit.point, new Quaternion ());
				}

			}
		}

		if (Input.GetMouseButton (1)) {
			//this.transform.localPosition = new Vector3 (0f, -0.318f, 0.682f);
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, new Vector3 (0f, -0.318f, 0.682f), 5f * Time.deltaTime);
		}
		if (!Input.GetMouseButton (1)) {
			//this.transform.localPosition = new Vector3 (0.499f, -0.414f, 0.806f);
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, new Vector3 (0.499f, -0.414f, 0.806f), 5f * Time.deltaTime);


		}
	}
}
