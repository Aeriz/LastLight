using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject loadScreen;

    public GameObject altStart;
    public GameObject altExit;

	// Use this for initialization
	void Start () {
		loadScreen.SetActive (false);
        PlayerPrefs.SetInt("CollectableCounter", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame() {
		loadScreen.SetActive (true);
		Application.LoadLevel (1);
	}

    public void NewGameEnter()
    {
        altStart.SetActive(true);
    }

    public void NewGameExit()
    {
        altStart.SetActive(false);
    }

    public void LoadGame()
    {
        loadScreen.SetActive(true);
        Application.LoadLevel(2);
    }

	public void Exit () {
		Application.Quit ();
	}

    public void ExitEnter()
    {
        altExit.SetActive(true);
    }

    public void ExitExit()
    {
        altExit.SetActive(false);
    }
}
