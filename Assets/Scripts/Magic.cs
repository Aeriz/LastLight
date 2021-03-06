﻿using UnityEngine;
using System.Collections;
using InControl;
using UnityStandardAssets.Characters.ThirdPerson;

public class Magic : MonoBehaviour
{
    public int beamAttack = 100;
    public int AOEAttack = 20;
    public float beamCost = 50;
    public float AOECost = 30;
    public float radius = 7;
    public float distance = 10;
    public float AOEdistance = 5;
    public float width = 2;
    public GameObject lineObject;
    LineRenderer line;
    bool firing = false;

    float widthTimer = 0;
    float expandTimer = 0;
    float shrinkTimer = 1;
    public float beamCoolDown = 0;
    public float AOECoolDown = 0;

    GameObject player;
    public GameObject camera;
    ThirdPersonUserControl thirdPersonScript;
    Mana_Stamina mana;
    GameObject AOEparticle;
    float AOEtimer = 1.3f;
    bool AOE;
    public ParticleSystem ps;

    float mergeTime = 5;

    public MyCharacterActions characterActions;

    // Use this for initialization
    void Start()
    {
        AOEparticle = GameObject.FindGameObjectWithTag("AOEAttack");
        ps = AOEparticle.GetComponentInChildren<ParticleSystem>();
        line = lineObject.GetComponent<LineRenderer>();
        characterActions = new MyCharacterActions();
        characterActions.beamSpell.AddDefaultBinding(Key.Key1);
        characterActions.AOESpell.AddDefaultBinding(Key.Key2);
        mana = GetComponent<Mana_Stamina>();
        ps.Pause();
        camera = GameObject.FindGameObjectWithTag("BaseCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        thirdPersonScript = player.GetComponentInChildren<ThirdPersonUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterActions.beamSpell.IsPressed && beamCoolDown <= 0 && (mana.currentMana - beamCost) >= 0 && !firing)
        {
                player.transform.rotation = new Quaternion(player.transform.rotation.x, camera.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);
                merge(mergeTime);
                mana.useMana(beamCost);
                thirdPersonScript.canPushMirror = true;
                beamCoolDown = 5;
                firing = true;
                line.enabled = true;
                line.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
                line.SetPosition(1, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward * distance);
                line.SetWidth(0.1f, 0.1f);
                beamSpell();
        }
        if(beamCoolDown > 0)
        {
            beamCoolDown -= Time.deltaTime;
        }

        if (characterActions.AOESpell.IsPressed && AOECoolDown <= 0 && (mana.currentMana - AOECost) >= 0)
        {
            AOE = true;
            thirdPersonScript.canPushMirror = true;
            /*
            mana.useMana(AOECost);
            //thirdPersonScript.canPushMirror = true;
            AOECoolDown = 5;
            AOESpell();
            */
        }
        if(AOE)
        {
            AOEtimer -= Time.deltaTime;
            if (!ps.isPlaying)
            {
                mana.useMana(AOECost);
                ps.Play();
            }

            if(AOEtimer < 0)
            {
                thirdPersonScript.canPushMirror = false;
                ps.Stop();
                ps.time = 0;
                AOECoolDown = 5;
                AOESpell();
                AOE = false;
                AOEtimer = 1.3f;
            }
        }
        if (AOECoolDown > 0)
        {
            AOECoolDown -= Time.deltaTime;
        }

        if (firing)
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
        int livingMask = 1 << 11;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, distance, livingMask);
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
        int livingMask = 1 << 11;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AOEdistance, livingMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //checks if player is in sight and range, if no in sight the enemy will go to players last known location and if it cannot see the player or aggro'd enemies then it will return to its home
            if (hitColliders[i].tag == "Enemy")
            {
                EnemyScript tempEnemy = hitColliders[i].GetComponent<EnemyScript>();
                tempEnemy.stunned = true;
                tempEnemy.takeDamage(AOEAttack);
            }
            i++;
        }
    }

    void merge(float seconds)
    {
        float timer = seconds;
        Quaternion fromRotation = player.transform.rotation;

        while(timer > 0)
        {
            player.transform.rotation = Quaternion.RotateTowards(fromRotation, camera.transform.rotation, (seconds - timer)/seconds);
            timer -= Time.deltaTime;
        }

    }
}