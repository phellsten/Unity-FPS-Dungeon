// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
	public GameObject player;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    RaycastHit hit;
    Camera cam;
    private Vector2 mouseLook;
	private Vector2 deltaMousePos;
    public float sens = 2.0f;

    private void Start()
    {
        hit = new RaycastHit();
        cam = GetComponent<Camera>();
    }

    void Update () {
		// Change new direction based on mouse axis.
		deltaMousePos.x = Mathf.Lerp(deltaMousePos.x, Input.GetAxisRaw("Mouse X") * sens, 1f);
		deltaMousePos.y = Mathf.Lerp(deltaMousePos.y, Input.GetAxisRaw("Mouse Y") * sens, 1f);

		mouseLook += deltaMousePos;

		// Clamp camera between -90 and 90 degrees.
		mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

		// Apply rotation
		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Vector3 gunPos = new Vector3(transform.position.x + 0.886f, transform.position.y - 0.281f, transform.position.z + 0.925f);
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, new Color(1.0f,0,0),5 );

            
            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            bulletObj.GetComponent<Rigidbody>().velocity = 1.0f * (hit.point - transform.position + new Vector3(0f, 0f, -5f)).normalized * bulletSpeed;
            
        }
    }

}
