using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;
    private IEnumerator arriveChecker;
    private bool moving = false;
    private bool running = false;

    void Awake () {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
	}
	
	void Update () {
        if (!moving && running)
        {
            Debug.Log("Stop coroutine");
            StopCoroutine(arriveChecker);
            running = false;
        }
        // Set destination
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            float cameraY = Camera.main.transform.position.y;
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector3 screenPosWithZDist = (Vector3)mouseScreenPos + (Vector3.forward * cameraY);
            Vector3 fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDist);
            Debug.Log("New Pos: " + fireToWorldPos);
            nav.SetDestination(fireToWorldPos);
            anim.SetBool("Walking",true);
            moving = true;
            arriveChecker = CheckForArrival();
            StartCoroutine(arriveChecker);
            running = true;
        }
        
    }
    
    // Check if at arrival point every 0.1 seconds.
    IEnumerator CheckForArrival()
    {     
        for (;;)
        {
            Debug.Log("Run coroutine");
            if (!nav.pathPending)
            {
                if (nav.remainingDistance <= nav.stoppingDistance)
                {
                    if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                    {
                        Debug.Log("Arrived");
                        anim.SetBool("Walking", false);
                        moving = false;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
