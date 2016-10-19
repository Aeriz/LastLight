using UnityEngine;
using System.Collections;
using InControl;


public class SaveGame : MonoBehaviour
{
    public GameObject player;
    MyCharacterActions characterActions;
    Health playerHealth;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            /*
            PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
            PlayerPrefs.Save();
            */
        }
        if(characterActions.load.IsPressed)
        {
            if (ES2.Exists("myFile"))
            {
                player.transform.position = ES2.Load<Vector3>("myFile.txt?tag=playerPosition");
                playerHealth.currenthealth = ES2.Load<int>("myFile.txt?tag=playerHealth");
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
*/

