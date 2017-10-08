using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour {
	public GameObject blood;
	public GameObject muzzle;
	public GameObject source;

	// Update is called once per frame
	void Update () {
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
	}
}
