using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour
{
    public bool lightOnKey = false;
    public bool saveGameBool = false;
    public bool unlock = false;
    public float timer = 0;
    public Vector4 color;
    public Color colour;
    SaveGame save;
    GameObject saveGame;
	// Use this for initialization
	void Start ()
    {
        colour = color;
        saveGame = GameObject.FindGameObjectWithTag("SaveGame");
        save = saveGame.GetComponent<SaveGame>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (lightOnKey)
        {
           
            if(timer > 3)
            {
                unlock = true;
                if (saveGameBool == false)
                {
                    save.GameSave();
                    saveGameBool = true;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
	}

}
