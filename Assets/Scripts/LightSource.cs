using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using InControl;
using UnityStandardAssets.Cameras;


public class LightSource : MonoBehaviour {
    public Color colour;
    public Vector4 colourVec;
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
    void Start () {
        this.colour = colourVec;
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
    }
	
	// Update is called once per frame
	void Update ()
    {
        canPushMirror = (player.transform.position - transform.position).magnitude < 3;

        //if (characterActions.RotateMirror.WasPressed)
        if (canPushMirror && Input.GetKeyDown(KeyCode.R))
        {
            thirdPersonScript.canPushMirror = !thirdPersonScript.canPushMirror;
            if (thirdPersonScript.canPushMirror)
            {
                freeLookScript.SetTarget(this.transform);
                wallClipScript.closestDistance = 7;
            }
            else
            {
                freeLookScript.SetTarget(player.transform);
                wallClipScript.closestDistance = 1f;
            }

        }

        if (thirdPersonScript.canPushMirror == true && canPushMirror)
        {
            if (characterActions.Left.IsPressed)
            {
                transform.Rotate(new Vector3(0, .5f, 0));
                //mirrorBase.transform.Rotate(new Vector3(0, .5f, 0));
            }
            if (characterActions.Right.IsPressed)
            {
                transform.Rotate(new Vector3(0, -0.5f, 0));
                //mirrorBase.transform.Rotate(new Vector3(0, -0.5f, 0));
            }
            //if (characterActions.RotateMirror.WasPressed == true)
            //{

            //    timerSet = true;
            //}
        }

    }
}
