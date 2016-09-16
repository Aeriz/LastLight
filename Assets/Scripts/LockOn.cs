using UnityEngine;
using System.Collections;
using InControl;
namespace UnityStandardAssets.Cameras
{
    public class LockOn : MonoBehaviour
    {
        MyCharacterActions characterActions;
        RaycastHit hit;
        public FreeLookCam freeLookScript;
        public AutoCam autoCamScript;

        public Camera mainCamera;

        public bool lockedOn = false;
        // Use this for initialization
        void Awake()
        {
            characterActions = new MyCharacterActions();
            characterActions.LockOn.AddDefaultBinding(Key.Tab);
            characterActions.LockOn.AddDefaultBinding(InputControlType.Action2);
            //mainCamera = GetComponent<Camera>();
            freeLookScript = GetComponent<FreeLookCam>();
            autoCamScript = GetComponent<AutoCam>();
        }

        // Update is called once per frame
        void Update()
        {
            if (characterActions.LockOn.WasPressed == true && freeLookScript.m_LockedOn == false)
            {
                Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                //Debug.DrawRay(mainCamera.transform.position, Vector3.forward * 10, Color.white);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    print("Im looking at " + hit.transform.name);
                    if (hit.transform.tag == "Enemy")
                    {
                        freeLookScript.m_LockedOn = true;
                        lockedOn = true;
                        freeLookScript.target = hit.transform;
                        //freeLookScript.enabled = false;
                        //autoCamScript.enabled = true;
                       // autoCamScript.LockOn(hit.transform);
                    }

                }
                else
                {
                    print("Im looking at nothing");
                }
            }
            else if(characterActions.LockOn.WasPressed == true && freeLookScript.m_LockedOn == true)
            {
                freeLookScript.m_LockedOn = false;
                lockedOn = false;
            }
        }
    }
}
