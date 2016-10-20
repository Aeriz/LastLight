using UnityEngine;
using System.Collections;
using InControl;

public class Magic : MonoBehaviour
{
    MyCharacterActions characterActions;
    public int beamAttack = 100;
    public int AOEAttack = 20;
    public float radius = 7;
    public float distance = 10;
    public float width;
    public GameObject line;
    // Use this for initialization
    void Start()
    {
        characterActions = new MyCharacterActions();
        characterActions.beamSpell.AddDefaultBinding(Key.Key1);
        characterActions.AOESpell.AddDefaultBinding(Key.Key2);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterActions.beamSpell.IsPressed)
        {
            beamSpell();
        }
    }

    void beamSpell()
    {
        /*
        LineRenderer tempLine = line.GetComponent<LineRenderer>();
        tempLine.enabled = false;
        tempLine.SetPosition(0, transform.position);
        */
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