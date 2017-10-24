using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;
    private AnimationClip clip;
    private GameObject player;
    private Transform playerTransform;
    private IEnumerator moveChecker;
    private bool inRange = false;
    public float health = 5;

    public static float moveCheckDelay = 0.1f;
    public static int attackDamage = 1;

    public GameObject explosion;

    private void Start()
    {
        explosion = (GameObject)(Resources.Load("explosion"));
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        AddAttackAnimEvent();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        moveChecker = CheckPlayerPosition();
        StartCoroutine(moveChecker);
        anim.SetBool("Walking", true);
    }

    private void AddAttackAnimEvent()
    {
        AnimationEvent animEvent = new AnimationEvent();
        animEvent.functionName = "Attack";
        animEvent.stringParameter = "Attacking";
        animEvent.time = 1.01f;
        animEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
        clip = anim.runtimeAnimatorController.animationClips[2];
        clip.AddEvent(animEvent);
    }

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(explosion, this.transform.position, new Quaternion());
            this.GetComponent<ScoreManager>().incrementScore();
            int range = Random.Range(2, 7);
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().ammoCap += range;
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<ShootingScript>().refreshAmmo();

            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(moveChecker);
    }

    public void Attack(string message)
    {
        if (inRange)
        {
            player.GetComponent<PlayerHealthManager>().ApplyDamage(attackDamage);
        }
    }

    // Stop when encountered the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(moveChecker);
            inRange = true;
            anim.SetTrigger("Attack");
        }
    }

    // Restart movement if player moves away
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(moveChecker);
            inRange = false;
            anim.SetBool("Walking", true);
        }
    }

    // Reassigns movement target to player location every x seconds.
    private IEnumerator CheckPlayerPosition()
    {
        while (true)
        {
            nav.SetDestination(playerTransform.position);
            yield return new WaitForSeconds(moveCheckDelay);
        }
    }
}