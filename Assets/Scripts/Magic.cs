using UnityEngine;
using System.Collections;
using InControl;
using UnityStandardAssets.Characters.ThirdPerson;

public class Magic : MonoBehaviour
{
    MyCharacterActions characterActions;
    public int beamAttack = 100;
    public int AOEAttack = 20;
    public float radius = 7;
    public float distance = 10;
    public float width;
    public GameObject lineObject;
    LineRenderer line;
    bool firing = false;

    float widthTimer = 0;
    float expandTimer = 0;
    float shrinkTimer = 1;
    public float beamCoolDown = 0;
    float AOECoolDown = 0;

    GameObject player;
    ThirdPersonUserControl thirdPersonScript;


    // Use this for initialization
    void Start()
    {
        
        line = lineObject.GetComponent<LineRenderer>();
        characterActions = new MyCharacterActions();
        characterActions.beamSpell.AddDefaultBinding(Key.Key1);
        characterActions.AOESpell.AddDefaultBinding(Key.Key2);

        player = GameObject.FindGameObjectWithTag("Player");
        thirdPersonScript = player.GetComponentInChildren<ThirdPersonUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterActions.beamSpell.IsPressed && beamCoolDown <= 0)
        {
            thirdPersonScript.canPushMirror = true;
            beamCoolDown = 5;
            firing = true;
            line.enabled = true;
            line.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
            line.SetPosition(1, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward * distance);
            line.SetWidth(0.1f, 0.1f);
            //beamSpell();
        }
        if(beamCoolDown > 0)
        {
            beamCoolDown -= Time.deltaTime;
        }

        if(firing)
        {

            widthTimer += Time.deltaTime;
            if(shrinkTimer <= 0)
            {
                thirdPersonScript.canPushMirror = false;
                beamSpell();
                firing = false;
                line.enabled = false;
                widthTimer = 0;
                expandTimer = 0;
                shrinkTimer = 1;
            }
            if(expandTimer > 1)
            {
                shrinkTimer -= Time.deltaTime * 15;
                line.SetWidth(shrinkTimer, shrinkTimer);
            }
            if(widthTimer > 0.8)
            {
                expandTimer += Time.deltaTime *10;
                line.SetWidth(expandTimer, expandTimer);
            }
        }
    }

    void beamSpell()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, distance);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //checks if player is in sight and range, if no in sight the enemy will go to players last known location and if it cannot see the player or aggro'd enemies then it will return to its home
            if (hitColliders[i].tag == "Enemy")
            {
                Vector3 delta = hitColliders[i].transform.position - transform.position;
                float range = Vector3.Dot(delta, transform.forward);
                float d = (delta - range * transform.forward).magnitude;
                if(range>0 && d<width)
                {
                    EnemyScript tempEnemy = hitColliders[i].GetComponent<EnemyScript>();
                    tempEnemy.takeDamage(beamAttack);
                }
            }
            i++;
        }
    }

    void AOESpell()
    {

    }
}