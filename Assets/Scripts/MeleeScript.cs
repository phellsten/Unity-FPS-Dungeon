using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    private Vector3 stabPos = new Vector3(-0.15f, -0.25f, 0.5f);
    private Vector3 hidePos = new Vector3(-0.255f, -0.4383f, -0.251f);
    public GameObject source;
    private GameObject weapon;
    public GameObject blood;

    private float stabTimer = 0.3f;
    private float stabCooldown = 0f;
    public bool melee = false;

    // Use this for initialization
    private void Start()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    // Update is called once per frame
    private void Update()
    {
        if (stabCooldown > 0f)
        {
            stabCooldown -= Time.deltaTime;
        }

        // If press melee, currently not melee attacking and not aiming with weapon

        // NOTE: if more weapons added this will need to be adapted ! //
        if (Input.GetKeyDown(KeyBindings.MeleeKey) && melee == false
            && !weapon.GetComponent<ShootingScript>().aiming
            && !weapon.GetComponent<ShootingScript>().reloading && stabCooldown <= 0f)
        {
            melee = true;
            GetComponents<AudioSource>()[0].Play();
            stabCooldown = 0.5f;

            Ray ray = new Ray(source.transform.position, source.transform.forward);
            RaycastHit hit;
            bool raycasthit = Physics.Raycast(ray, out hit, 1f);
            if (raycasthit)
            {
                Debug.Log("melee hit");

                if (hit.collider.tag == "EnemyMonster" || hit.collider.tag == "EnemySkeleton")
                {
                    Instantiate(blood, hit.point, new Quaternion());
                    if (hit.collider.tag == "EnemyMonster")
                    {
                        hit.collider.gameObject.GetComponent<MonsterController>().health -= 1f;
                    }
                    else if (hit.collider.tag == "EnemySkeleton")
                    {
                        hit.collider.gameObject.GetComponent<SkeletonController>().health -= 1f;
                    }
                }
            }
        }

        if (melee)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            stabTimer -= Time.deltaTime;
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, stabPos, 8f * Time.deltaTime);
            if (stabTimer <= 0f)
            {
                melee = false;
                stabTimer = 0.3f;
            }
        }

        if (this.transform.localPosition != hidePos && melee == false)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, hidePos, 8f * Time.deltaTime);
        }

        if (this.transform.localPosition == hidePos)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}