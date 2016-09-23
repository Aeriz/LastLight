using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public int startHealth = 100;
    public int currentHealth;
    public bool isDead = false;
    public float attackSpeed = 0.5f;
    public int attackDamage = 10;
    public float aggroRange = 30;
    bool inRange;
    float timer;
    float checkAggroTimer = 0;
    Health playerHealth;
    GameObject player;
    Rigidbody m_Rigidbody;
    NavMeshAgent nav;
    public bool aggro = false;
    public bool attackBool = false;
    public bool playerInRange = false;

    Vector3 lastKnowPlayerLocation = new Vector3(0, 0, 0);
    Vector3 lastKnowFriendlyLocation = new Vector3(0, 0, 0);

    //EnemyHealth enemyHealth;
    // Use this for initialization
    void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        currentHealth = startHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        //enemyHealth = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        checkEnemyInRange();
         
	    if(inRange)
        {
            timer += Time.deltaTime;
        }
        if(timer >= attackSpeed && inRange && currentHealth > 0 && aggro)
        {
            //attack();
            attackBool = true;
        }
        if(currentHealth > 0 && playerHealth.currenthealth > 0 && aggro)
        {
            nav.enabled = true;
        }
        else
        {
            nav.enabled = false;
        }

	}

    void checkEnemyInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aggroRange);
        int i = 0;
        while (i<hitColliders.Length)
        {
            //checks if player is in sight and range, if no in sight the enemy will go to players last known location and if it cannot see the player or aggro'd enemies then it will return to its home
            if (hitColliders[i].tag == "Player")
            {
                RaycastHit hit;
                if (Physics.Linecast(this.transform.position, hitColliders[i].transform.position, out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        playerInRange = true;
                        aggro = true;
                        lastKnowPlayerLocation = hit.collider.transform.position;
                        nav.SetDestination(player.transform.position);
                        break;
                    }
                    else if (aggro && hit.collider.tag != "Player")
                    {
                        nav.SetDestination(lastKnowPlayerLocation);
                        Debug.Log("blocked");
                    }
                    else
                    {
                        playerInRange = false;
                        aggro = false;
                    }
                }

            }
            //Check if enemies in sight and range are aggro'd, if so, goes to the enemy 
            if (hitColliders[i].tag == "Enemy")
            {
                EnemyScript enemy = hitColliders[i].GetComponent<EnemyScript>();
                RaycastHit hit;
                int layerMask = 1 << 10;

                layerMask = ~layerMask;
                if (Physics.Linecast(this.transform.position, hitColliders[i].transform.position, out hit, layerMask))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        if (enemy.aggro && !playerInRange)
                        {
                            aggro = true;
                            lastKnowFriendlyLocation = hit.collider.transform.position;
                            nav.SetDestination(hitColliders[i].transform.position);
                            break;
                        }
                        else if (aggro && hit.collider.tag != "Enemy")
                        {
                            nav.SetDestination(lastKnowFriendlyLocation);
                            Debug.Log("blocked");
                        }
                        else
                        {
                            aggro = false;
                        }
                    }
                }
            }
            i++;
        }
    }

    void attack()
    {
        timer = 0;
        if (playerHealth.currenthealth > 0)
        {
            playerHealth.takeDamage(attackDamage);
        }
        attackBool = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
           inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == player)
        {
            inRange = false;
        }
    }


    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth < 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        //play death animation here
        isDead = true;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.velocity = new Vector3(3, 10, 0);

    }
}
