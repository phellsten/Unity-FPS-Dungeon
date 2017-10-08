using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;
    private Transform playerTransform;
    private IEnumerator moveChecker;
    //private bool moving = false;
    //private bool running = false;
	public float health = 5;

    public float moveCheckDelay = 0.1f;

	public GameObject explosion;

	void Start() {
		explosion = (GameObject)(Resources.Load ("explosion"));
	}

    void Awake () {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        moveChecker = CheckPlayerPosition();
        StartCoroutine(moveChecker);
        anim.SetBool("Walking", true);
	}
	
	void Update () {
		if (health <= 0) {
			Instantiate (explosion, this.transform.position, new Quaternion ());
			Destroy (this.gameObject);
		}
	}
	//void Update () {
        //if (!moving && running)
        //{
        //    Debug.Log("Stop coroutine");
        //    StopCoroutine(arriveChecker);
        //    running = false;
        //}

        // Set destination
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //
        //    float cameraY = Camera.main.transform.position.y;
        //    Vector2 mouseScreenPos = Input.mousePosition;
        //    Vector3 screenPosWithZDist = (Vector3)mouseScreenPos + (Vector3.forward * cameraY);
        //    Vector3 fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDist);
        //    Debug.Log("New Pos: " + fireToWorldPos);
        //    nav.SetDestination(fireToWorldPos);
        //    anim.SetBool("Walking",true);
        //    moving = true;
        //    arriveChecker = CheckForArrival();
        //    StartCoroutine(arriveChecker);
        //    running = true;
        //}
        
    //}

    // Stop when encountered the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(moveChecker);
            anim.SetBool("Walking", false);
        }
    }

    // Restart movement if player moves away
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(moveChecker);
            anim.SetBool("Walking", true);
        }
    }

    // Reassigns movement target to player location every 0.1 seconds.
    IEnumerator CheckPlayerPosition()
    {     
        while (true)
        {
            Debug.Log("Run coroutine");
            nav.SetDestination(playerTransform.position);
            yield return new WaitForSeconds(moveCheckDelay);
        }
    }
}
