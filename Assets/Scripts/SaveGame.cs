using UnityEngine;
using System.Collections;
using InControl;


public class SaveGame : MonoBehaviour
{
    public GameObject player;
    public GameObject[] mirrors;
    public GameObject[] lights;
    public GameObject[] enemies;
    public Transform[] loadedMirrors;
    MyCharacterActions characterActions;
    Health playerHealth;

    // Use this for initialization
    void Start()
    {
        mirrors = GameObject.FindGameObjectsWithTag("MirrorPane");
        player = GameObject.FindGameObjectWithTag("Player");
        lights = GameObject.FindGameObjectsWithTag("LightSource");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerHealth = player.GetComponent<Health>();
        characterActions = new MyCharacterActions();
        characterActions.save.AddDefaultBinding(Key.F1);
        characterActions.load.AddDefaultBinding(Key.F2);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterActions.save.IsPressed)
        {
            ES2.Save(transform, "myFile");
            ES2.Save(player.transform.position, "myFile.txt?tag=playerPosition");
            ES2.Save(playerHealth.currenthealth, "myFile.txt?tag=playerHealth");

            for (int i = 0; i < mirrors.Length; i++)
            {
                ES2.Save(mirrors[i].transform.position, "myFile.txt?tag=mirrorP" + i);
                ES2.Save(mirrors[i].transform.eulerAngles.y, "myFile.txt?tag=mirrorA" + i);
            }
            for (int i = 0; i < lights.Length; i++)
            {
                ES2.Save(lights[i].transform.position, "myFile.txt?tag=mirrorP" + i);
                ES2.Save(lights[i].transform.eulerAngles.y, "myFile.txt?tag=mirrorA" + i);
            }

            /*
            PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
            PlayerPrefs.Save();
            */
        }
        if (characterActions.load.IsPressed)
        {
            Application.LoadLevel(Application.loadedLevel);
            if (ES2.Exists("myFile"))
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    EnemyScript enemy = enemies[i].GetComponent<EnemyScript>();
                    enemy.aggro = false;
                }
                player.transform.position = ES2.Load<Vector3>("myFile.txt?tag=playerPosition");
                playerHealth.currenthealth = ES2.Load<int>("myFile.txt?tag=playerHealth");

                loadedMirrors = new Transform[mirrors.Length];
                for (int i = 0; i < mirrors.Length; i++)
                {
                    if (ES2.Exists("myFile.txt?tag=mirrorP" + i))
                    {
                        Vector3 pos = ES2.Load<Vector3>("myFile.txt?tag=mirrorP" + i);
                        float angle = ES2.Load<float>("myFile.txt?tag=mirrorA" + i);
                        for (int j = 0; j < mirrors.Length; j++)
                        {
                            if (mirrors[j].transform.position == pos)
                            {
                                mirrors[j].transform.eulerAngles = new Vector3(0, angle, 0);
                            }
                        }
                        for (int j = 0; j < lights.Length; j++)
                        {
                            if (lights[j].transform.position == pos)
                            {
                                lights[j].transform.eulerAngles = new Vector3(0, angle, 0);
                            }
                        }

                    }
                }
            }
            /*

              Vector3 tempPos;
              tempPos.x = PlayerPrefs.GetFloat("PlayerX");
              tempPos.y = PlayerPrefs.GetFloat("PlayerY");
              tempPos.z = PlayerPrefs.GetFloat("PlayerZ");
              player.transform.position = tempPos;
              */
        }
    }



    public void GameSave()
    {
        Debug.Log("SAVE");
        ES2.Save(transform, "myFile");
        ES2.Save(player.transform.position, "myFile.txt?tag=playerPosition");
        ES2.Save(playerHealth.currenthealth, "myFile.txt?tag=playerHealth");

        for (int i = 0; i < mirrors.Length; i++)
        {
            ES2.Save(mirrors[i].transform.position, "myFile.txt?tag=mirrorP" + i);
            ES2.Save(mirrors[i].transform.eulerAngles.y, "myFile.txt?tag=mirrorA" + i);
        }
        for (int i = 0; i < lights.Length; i++)
        {
            ES2.Save(lights[i].transform.position, "myFile.txt?tag=mirrorP" + i);
            ES2.Save(lights[i].transform.eulerAngles.y, "myFile.txt?tag=mirrorA" + i);
        }
   
    }

    public void LoadGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        if (ES2.Exists("myFile"))
        {
<<<<<<< HEAD
            Debug.Log("LOAD");
=======
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyScript enemy = enemies[i].GetComponent<EnemyScript>();
                enemy.aggro = false;
            }
>>>>>>> d6e284d7ee88c08391a33771aad946fc76a6da65
            player.transform.position = ES2.Load<Vector3>("myFile.txt?tag=playerPosition");
            playerHealth.currenthealth = ES2.Load<int>("myFile.txt?tag=playerHealth");

            loadedMirrors = new Transform[mirrors.Length];
            for (int i = 0; i < mirrors.Length; i++)
            {
                if (ES2.Exists("myFile.txt?tag=mirrorP" + i))
                {
                    Vector3 pos = ES2.Load<Vector3>("myFile.txt?tag=mirrorP" + i);
                    float angle = ES2.Load<float>("myFile.txt?tag=mirrorA" + i);
                    for (int j = 0; j < mirrors.Length; j++)
                    {
                        if (mirrors[j].transform.position == pos)
                        {
                            mirrors[j].transform.eulerAngles = new Vector3(0, angle, 0);
                        }
                    }
                    for (int j = 0; j < lights.Length; j++)
                    {
                        if (lights[j].transform.position == pos)
                        {
                            lights[j].transform.eulerAngles = new Vector3(0, angle, 0);
                        }
                    }

                }
            }
        }
    }
}






/*
 * THINGS TO SAVE * 
 * player position
 * player health
 * mirror rotations
 * light source rotations
 * player level?
 * 
 * 
 * 
 * 
 * 
 * 
 * get all the mirrors, save them
 * to load, compare the position of the loaded list to the mirror aquired and if it is the same one delete it from the list
*/




