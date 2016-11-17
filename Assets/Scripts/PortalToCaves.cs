using UnityEngine;
using System.Collections;

public class PortalToCaves : MonoBehaviour {

    public int caveSpawn;
    public GameObject loadingScreen;

    // Use this for initialization
    void Start () {
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            loadingScreen.SetActive(true);
            PlayerPrefs.SetInt("CaveSpawn", caveSpawn);
            Debug.Log(caveSpawn);
            //loadingScreen.SetActive(true);
            Application.LoadLevel(3);
        }
    }
}
