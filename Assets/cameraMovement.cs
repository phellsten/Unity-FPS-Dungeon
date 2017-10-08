// Paul Hellsten - 758077
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
	public GameObject player;
    public GameObject explosion;
    public float bulletSpeed = 100f;
    
    Camera cam;
    private Vector2 mouseLook;
	private Vector2 deltaMousePos;
    public float sens = 2.0f;

    private void Start()
    {
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
        
        if (Input.GetMouseButton(0))
        {
            
            Ray ray = cam.ScreenPointToRay(cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction, new Color(1.0f,0,0),5 );
            RaycastHit hit;
            bool raycasthit = Physics.Raycast(ray, out hit, 200);
            GameObject myLine = new GameObject();
            myLine.transform.position = ray.origin;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            lr.SetColors(Color.red, Color.red);
            lr.SetWidth(0.1f, 0.1f);
            lr.SetPosition(0, ray.origin);
            lr.SetPosition(1, hit.point);
            if (raycasthit) {

                



                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("Hit!");
                    Debug.DrawLine(ray.origin, hit.point, Color.yellow);
                    Instantiate(explosion, hit.point, new Quaternion());

                    

                }
                

            }
            else
            {
                Debug.Log("NO HIT");
            }


            //GameObject bulletObj = Instantiate(bullet, ray.origin, transform.rotation) as GameObject;
            //bulletObj.transform.position = new Vector3(bulletObj.transform.position.x, bulletObj.transform.position.y, bulletObj.transform.position.z + 2);

            //bulletObj.GetComponent<Rigidbody>().velocity = -1.0f * (hit.point - transform.position).normalized * bulletSpeed;

        }
    }

}
