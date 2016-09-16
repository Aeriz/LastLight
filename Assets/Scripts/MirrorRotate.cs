using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using InControl;
using UnityStandardAssets.Cameras;


public class MirrorRotate : MonoBehaviour
{
    MyCharacterActions characterActions;
    //public GameObject mirror;
    public GameObject mirrorBase;
    GameObject player;
    ThirdPersonUserControl thirdPersonScript;
    FreeLookCam freeLookScript;
    ProtectCameraFromWallClip wallClipScript;
    GameObject mainCamera;
    public bool canPushMirror = false;
    float timer = 0;
    bool timerSet = false;

	// Use this for initialization
	void Start ()
    {
        characterActions = new MyCharacterActions();
        characterActions.RotateMirror.AddDefaultBinding(Key.R);
        characterActions.RotateMirror.AddDefaultBinding(InputControlType.RightBumper);
        characterActions.Left.AddDefaultBinding(Key.A);
        characterActions.Left.AddDefaultBinding(InputControlType.LeftStickRight);
        characterActions.Right.AddDefaultBinding(Key.D);
        characterActions.Right.AddDefaultBinding(InputControlType.LeftStickLeft);

        mainCamera = GameObject.FindGameObjectWithTag("BaseCamera");
        freeLookScript = mainCamera.GetComponentInChildren<FreeLookCam>();
        wallClipScript = mainCamera.GetComponentInChildren<ProtectCameraFromWallClip>();


        player = GameObject.FindGameObjectWithTag("Player");
        thirdPersonScript = player.GetComponentInChildren<ThirdPersonUserControl>();
       // mirrorBase = GameObject.FindGameObjectWithTag("MirrorBase");

    }

    // Update is called once per frame
    void Update()
    {
        canPushMirror = (player.transform.position - transform.position).magnitude < 4;

        //if (characterActions.RotateMirror.WasPressed)
        if (canPushMirror && Input.GetKeyDown(KeyCode.R))
        {
            thirdPersonScript.canPushMirror = !thirdPersonScript.canPushMirror;
            if (thirdPersonScript.canPushMirror)
            {
                freeLookScript.SetTarget(mirrorBase.transform);
                wallClipScript.closestDistance = 7;
            }
            else
            {
                freeLookScript.SetTarget(player.transform);
                wallClipScript.closestDistance = 1f;
            }
        }

        //if (timerSet == true)
        //{
        //    thirdPersonScript.canPushMirror = false;
        //    freeLookScript.SetTarget(player.transform);
        //    wallClipScript.closestDistance = 1f;
        //    timer += Time.deltaTime;
        //    if(timer >= 1.0f)
        //    {
        //        timerSet = false;
        //        timer = 0f;
        //    }
        //}
 
        if(thirdPersonScript.canPushMirror == true && canPushMirror)
        {
            if(characterActions.Left.IsPressed)
            {
                transform.Rotate(new Vector3(0, .5f, 0));
            }
            if(characterActions.Right.IsPressed)
            {
                transform.Rotate(new Vector3(0, -0.5f, 0));
            }
            //if (characterActions.RotateMirror.WasPressed == true)
            //{
                
            //    timerSet = true;
            //}
        }
        //else //if (thirdPersonScript.canPushMirror == false && timerSet == false)
        //{
        //    if (characterActions.RotateMirror.WasPressed == true && canPushMirror == true)
        //    {
        //        thirdPersonScript.canPushMirror = true;
        //        freeLookScript.SetTarget(mirrorBase.transform);
        //        wallClipScript.closestDistance = 7;
        //        timerSet = false;
        //    }
        //}
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        canPushMirror = true;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        canPushMirror = false;
    //    }
    //}
}
