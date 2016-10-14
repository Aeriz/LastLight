using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject loadScreen;

	// Use this for initialization
	void Start () {
		loadScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame() {
		loadScreen.SetActive (true);
		Application.LoadLevel (1);
	}

    public void LoadGame()
    {
        loadScreen.SetActive(true);
        Application.LoadLevel(2);
    }

	public void Exit () {
		Application.Quit ();
	}
}
