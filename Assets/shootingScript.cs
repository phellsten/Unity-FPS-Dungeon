// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
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
				if (hit.collider.tag == "EnemyMonster" || hit.collider.tag == "EnemySkeleton") {
					// Create blood effect, 
					// ToDo : Subtract HP from enemy
					Debug.DrawLine(ray.origin, hit.point, Color.yellow);
					Instantiate (blood, hit.point, new Quaternion ());
					if (hit.collider.tag == "EnemyMonster") {
						hit.collider.gameObject.GetComponent<MonsterController> ().health -= 1;
					} else if (hit.collider.tag == "EnemySkeleton") {
						hit.collider.gameObject.GetComponent<SkeletonController> ().health -= 1;

					}

				}
			}
		}

		// Holding right click, move gun to aim down sights.
		if (Input.GetMouseButton (1)) {
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, aimPos, gunMoveSpeed * Time.deltaTime);
			cursor.GetComponentInChildren<Canvas> ().enabled = false;

		}

		// Right click not pressed, keep gun in normal position.
		if (!Input.GetMouseButton (1)) {
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, normalPos, gunMoveSpeed * Time.deltaTime);
			cursor.GetComponentInChildren<Canvas> ().enabled = true;

		}
	}
}
