using UnityEngine;
using System.Collections;

public class PortalToPerseus : MonoBehaviour {

    public GameObject loadScreen;

	// Use this for initialization
	void Start () {
        loadScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            loadScreen.SetActive(true);
            Application.LoadLevel(2);
        }
    }
}
