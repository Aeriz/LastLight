using UnityEngine;
using System.Collections;
using InControl;
using UnityStandardAssets.Characters.ThirdPerson;

public class Combat : MonoBehaviour {
    MyCharacterActions characterActions;
    public int lightAttack = 10;
    public int heavyAttack = 20;
    public float radius = 2;
    int j = 0;
	// Use this for initialization
	void Start ()
    {
        characterActions = new MyCharacterActions();
        characterActions.lightAttack.AddDefaultBinding(Mouse.LeftButton);
        characterActions.heavyAttack.AddDefaultBinding(Mouse.RightButton);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (characterActions.lightAttack.IsPressed)
        {
            attackEnemies(lightAttack, 0.5f);
        }
        if (characterActions.heavyAttack.IsPressed)
        {
            attackEnemies(heavyAttack, 0.5f);
        }
    }

    void attackEnemies(int damage, float range)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        float dot = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                Vector3 targetDir = hitColliders[i].transform.position - transform.position;
                float angle = Vector3.Angle(targetDir, transform.forward);
                //dot = Vector3.Dot(hitColliders[i].transform.position, transform.TransformDirection(Vector3.forward));

                // get the distance the enemy is in front of the player
                float forwardsDist = Vector3.Dot(targetDir, transform.forward);

                // get the distance from the enemy to a line forwards of the player
                targetDir.Normalize();
                float sidewaysDist = Vector3.Dot(targetDir, transform.right);
                
                if (sidewaysDist > -range && sidewaysDist < range && forwardsDist > 0 && forwardsDist < radius)
                {
                    EnemyScript enemy = hitColliders[i].GetComponent<EnemyScript>();
                    enemy.takeDamage(damage);
                    j++;
                    Debug.Log("hit " + j + " " + sidewaysDist);
                }
                
                //Debug.Log(angle);
            }
            i++;
        }
    }
}
