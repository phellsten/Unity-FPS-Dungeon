﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour {
	public GameObject blood;
	public GameObject muzzle;
	public GameObject source;

	private Vector3 aimPos = new Vector3 (0f, -0.318f, 0.682f);
	private Vector3 normalPos = new Vector3 (0.499f, -0.414f, 0.806f);

	private float gunMoveSpeed = 5f;
	private float muzzleOffset = 1.5f;
	private float rayDistance = 200;

	// Update is called once per frame
	void Update () {
		// Game is paused, don't shoot.
		if (Time.timeScale == 0.0f) {
			return;
		}
		if (Input.GetMouseButtonDown(0)) {
			// Create muzzle flash
			Instantiate(muzzle, this.transform.position - muzzleOffset * this.transform.forward, new Quaternion());

			// Create raycast forwards from player
			Ray ray = new Ray(source.transform.position, source.transform.forward);
			RaycastHit hit;
			bool raycasthit = Physics.Raycast(ray, out hit, rayDistance);

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

		// Holding right click, move gun to aim down sights.
		if (Input.GetMouseButton (1)) {
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, aimPos, gunMoveSpeed * Time.deltaTime);
		}

		// Right click not pressed, keep gun in normal position.
		if (!Input.GetMouseButton (1)) {
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, normalPos, gunMoveSpeed * Time.deltaTime);
		}
	}
}
