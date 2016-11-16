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
    public float attackTimer = 0.5f;
    bool inRange;
    float timer;
    float checkAggroTimer = 0;
    Health playerHealth;
    Mana_Stamina manaAndStamina;
    GameObject player;
    Rigidbody m_Rigidbody;
    NavMeshAgent nav;
    public bool aggro = false;
    public bool attackBool = false;
    public bool playerInRange = false;
    public bool playerBlocking = false;
    public bool stunned = false;
    public float stunTimer = 0;
    Vector3 lastKnowPlayerLocation = new Vector3(0, 0, 0);
    Vector3 lastKnowFriendlyLocation = new Vector3(0, 0, 0);
    Vector3 enemyWanderRange;
    CapsuleCollider capsule;
    public bool damaged;

    //EnemyHealth enemyHealth;
    // Use this for initialization
    void Start ()
    {
        capsule = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        currentHealth = startHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        manaAndStamina = player.GetComponent<Mana_Stamina>();
        enemyWanderRange = m_Rigidbody.transform.position;
        //enemyHealth = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(attackBool)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer < 0)
            {
                attack();
                attackBool = false;
            }
        }
        else
        {
            attackTimer = 0.5f;
        }
       if(!isDead)
        checkEnemyInRange();
         
	    if(inRange)
        {
            timer += Time.deltaTime;
        }
        if(timer >= attackSpeed && inRange && currentHealth > 0 && aggro && !stunned && !damaged)
        {
            //attack();
            attackBool = true;
        }
        if(currentHealth > 0 && playerHealth.currenthealth > 0 && aggro && !stunned)
        {
            nav.enabled = true;
        }
        else if(stunned)
        {
            nav.enabled = false;
            stunTimer += Time.deltaTime;
            if(stunTimer > 1)
            {
                stunned = false;
                stunTimer = 0;
            }
        }


	}

    void checkEnemyInRange()
    {
        int livingMask = 1 << 11;
        Collider[] hitColliders = Physics.OverlapSphere(enemyWanderRange, aggroRange, livingMask);
        int i = 0;
        while (i<=hitColliders.Length)
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
                        nav.enabled = true;
                        lastKnowPlayerLocation = hit.collider.transform.position;
                        nav.SetDestination(lastKnowPlayerLocation);
                        break;
                    }
                    /*
                    else if (aggro)
                    {
                        nav.SetDestination(lastKnowPlayerLocation);
                        Debug.Log("blocked");
                    }
                    else
                    {
                        playerInRange = false;
                        aggro = false;
                    }
                    */
                }

            }
            //Check if enemies in sight and range are aggro'd, if so, goes to the enemy 
            if (hitColliders[i].tag == "Enemy" && !aggro)
            {
                EnemyScript enemy = hitColliders[i].GetComponent<EnemyScript>();
                RaycastHit hit;

                if (Physics.Linecast(this.transform.position, hitColliders[i].transform.position, out hit, livingMask))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        if (enemy.aggro && enemy.playerInRange)
                        {
                            aggro = true;
                            nav.enabled = true;
                            lastKnowFriendlyLocation = hit.collider.transform.position;
                            nav.SetDestination(player.transform.position);
                            break;
                        }
                        /*
                        else if (aggro && hit.collider.tag != "Enemy")
                        {
                            nav.SetDestination(lastKnowFriendlyLocation);
                            Debug.Log("blocked");
                        }
                        else
                        {
                            aggro = false;
                        }
                        */
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
            if (playerBlocking == false)
            {
                playerHealth.takeDamage(attackDamage);
            }
            else
            {
                manaAndStamina.useStamina(10);
            }
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
        attackBool = false;
        currentHealth -= damage;
        m_Rigidbody.AddExplosionForce(100, player.transform.position, 10, 0);
        if (currentHealth < 0 && !isDead)
        {
            Death();
        }
        damaged = false;
    }

    public void Death()
    {
        if (QuestManager.questActive[0])
        {
            QuestManager.questTracker[0] += 1;
        }
        capsule.enabled = false;
        //play death animation here
        nav.enabled = false;
        isDead = true;
        m_Rigidbody.isKinematic = false;
        //m_Rigidbody.velocity = new Vector3(3, 10, 0);


    }
}
