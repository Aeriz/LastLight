using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {

    public GameObject loadScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            loadScreen.SetActive(true);
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
