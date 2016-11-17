using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

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
            PlayerPrefs.SetInt("CollectableCounter", PlayerPrefs.GetInt("CollectableCounter") + 1);

            col.gameObject.SetActive(false);
            gotCollectable = true;
            CamPan();

            if (QuestManager.questActive[1])
            {
                QuestManager.questTracker[1] += 1;
            }
        } 
    }

    void CamPan()
    {
        if (gotCollectable)
        {
            GameObject.Find("ThirdPersonController").GetComponent<Rigidbody>().isKinematic = true;
            ThirdPersonUserControl playerControl = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonUserControl>();
            ThirdPersonCharacter playerMove = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter>();
            playerMove.enabled = false;
            playerControl.enabled = false;

            panCam.gameObject.SetActive(true);
            playerCam.gameObject.SetActive(false);

            Invoke("Reset", 10);
        }
    }

    void Reset()
    {
        GameObject.Find("ThirdPersonController").GetComponent<Rigidbody>().isKinematic = false;
        ThirdPersonUserControl playerControl = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonUserControl>();
        ThirdPersonCharacter playerMove = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter>();
        playerMove.enabled = true;
        playerControl.enabled = true;

        gotCollectable = false;

        panCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
    }
}
