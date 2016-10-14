using UnityEngine;
using System.Collections;
using InControl;

public class SaveGame : MonoBehaviour
{
    public GameObject player;
    MyCharacterActions characterActions;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterActions = new MyCharacterActions();
        characterActions.save.AddDefaultBinding(Key.F1);
        characterActions.load.AddDefaultBinding(Key.F2);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterActions.save.IsPressed)
        {
            PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
            PlayerPrefs.Save();
        }
        if(characterActions.load.IsPressed)
        {
            Vector3 tempPos;
            tempPos.x = PlayerPrefs.GetFloat("PlayerX");
            tempPos.y = PlayerPrefs.GetFloat("PlayerY");
            tempPos.z = PlayerPrefs.GetFloat("PlayerZ");
            player.transform.position = tempPos;
        }

    }
}




/*
 * THINGS TO SAVE * 
 * player position
 * player health
 * player mana
 * player stamina 
 * mirror rotations
 * light source rotations
 * player level?
 * 
 * 
*/

