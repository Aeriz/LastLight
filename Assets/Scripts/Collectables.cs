using UnityEngine;
using System.Collections;

public class Collectables : MonoBehaviour {

    public Camera panCam;
    public Camera playerCam;

    public GameObject panCamObj;
    public Transform focusPoint;

    public static bool gotCollectable;

	// Use this for initialization
	void Start () {
        gotCollectable = false;
        panCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

        panCamObj.transform.LookAt(focusPoint);

        if (Input.GetKeyDown(KeyCode.L))
        {
            gotCollectable = true;
            CamPan();
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Collectable")
        {
            col.gameObject.SetActive(false);
            gotCollectable = true;
            CamPan();
        } 
    }

    void CamPan()
    {
        if (gotCollectable)
        {
            panCam.gameObject.SetActive(true);
            playerCam.gameObject.SetActive(false);

            Invoke("Reset", 10);
        }
    }

    void Reset()
    {
        gotCollectable = false;

        panCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
    }
}
