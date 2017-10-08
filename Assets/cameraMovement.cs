// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
	public GameObject player;
	public Camera cam;

    private Vector2 mouseLook;
	private Vector2 deltaMousePos;
    public float sens = 2.0f;

	private float maxY = 90f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update () {
		// Game is paused, don't move camera.
		if (Time.timeScale == 0.0f) {
			return;
		}
		// Change new direction based on mouse axis.
		deltaMousePos.x = Mathf.Lerp(deltaMousePos.x, Input.GetAxisRaw("Mouse X") * sens, 1f);
		deltaMousePos.y = Mathf.Lerp(deltaMousePos.y, Input.GetAxisRaw("Mouse Y") * sens, 1f);

		mouseLook += deltaMousePos;

		// Clamp camera between -90 and 90 degrees.
		mouseLook.y = Mathf.Clamp (mouseLook.y, -maxY, maxY);

		// Apply rotation
		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
