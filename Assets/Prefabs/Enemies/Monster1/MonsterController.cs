using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;
    private AnimationClip clip;
    private GameObject player;
    private Transform playerTransform;
    private IEnumerator moveChecker;
    private bool inRange = false;
    //private bool moving = false;
    //private bool running = false;
    public float health = 5;

    public static float moveCheckDelay = 0.1f;
    public static int attackDamage = 1;

    public GameObject explosion;

	void Start() {
		explosion = (GameObject)(Resources.Load ("explosion"));
	}

    void Awake () {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        AddAttackAnimEvent();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        moveChecker = CheckPlayerPosition();
        StartCoroutine(moveChecker);
        anim.SetBool("Walking", true);
	}

    private void AddAttackAnimEvent()
    {
        //Debug.Log("Create animEvent");
        AnimationEvent animEvent = new AnimationEvent();
        animEvent.functionName = "Attack";
        animEvent.stringParameter = "Attacking";
        animEvent.time = 0.12f;
        animEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
        clip = anim.runtimeAnimatorController.animationClips[2];
        clip.AddEvent(animEvent);
    }

    void Update () {
        if (health <= 0) {
			    Instantiate (explosion, this.transform.position, new Quaternion ());
                this.GetComponent<scoreManager>().incrementScore();
                Destroy (this.gameObject);
		    }
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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            anim.SetTrigger("Attack");
        }

    }


    public void Attack(String message)
    {
        if (inRange)
        {
            player.GetComponent<PlayerHealthManager>().ApplyDamage(attackDamage);
        }
        Debug.Log("Monster AnimEvent: " + message);
    }

    // Stop when encountered the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(moveChecker);
            inRange = true;
            anim.SetBool("Walking", false);
        }
    }

    // Restart movement if player moves away
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(moveChecker);
            inRange = false; ;
            anim.SetBool("Walking", true);
        }
    }

    // Reassigns movement target to player location every 0.1 seconds.
    IEnumerator CheckPlayerPosition()
    {     
        while (true)
        {
            //Debug.Log("Run coroutine");
            nav.SetDestination(playerTransform.position);
            yield return new WaitForSeconds(moveCheckDelay);
        }
    }
}
