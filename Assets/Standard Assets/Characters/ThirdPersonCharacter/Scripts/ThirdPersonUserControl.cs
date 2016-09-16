using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        public int m_Jump = 0;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public bool m_float = false;

        public GameObject camera;
        public FreeLookCam freeCamScript;
        public bool canPushMirror = false;
        public bool closeMode = false;
        public bool lockedOn = false;
        public bool dash = false;
        public bool dashTimer = false;
        float timer = 0;
        public int dashCooldown = 1;


        private void Start()
        {
            camera = GameObject.FindGameObjectWithTag("BaseCamera");
            freeCamScript = camera.GetComponentInChildren<FreeLookCam>();
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

        }


        private void Update()
        {

            if (m_Jump == 0 && m_Character.m_IsGrounded == true)
            {
                if(CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    m_Jump++;
                }
               
            }
            else if(m_Jump == 1 )
            {
                if (CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    m_Jump++;
                }
            }
          
                if (CrossPlatformInputManager.GetButton("Jump") == false)
                {
                    m_float = false;
                }
                else if(CrossPlatformInputManager.GetButton("Jump"))
                {
                    m_float = true;
                }
            
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            lockedOn = freeCamScript.m_LockedOn;
            Debug.DrawLine(transform.position, transform.forward);
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);


            m_Character.closeMode = false;

            if (canPushMirror)
            {
                h = 0;
                v = 0;
                crouch = false;
            }

            // fine a local forwards and right direction. Default to world space
            Vector3 fwd = Vector3.forward;
            Vector3 right = Vector3.right;

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                fwd = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                right = m_Cam.right;
            }

            if (closeMode)
            {
                fwd = (freeCamScript.target.position - transform.position).normalized;
                right = Vector3.Cross(Vector3.up, fwd);
            }

            m_Move = v * fwd + h * right;
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftAlt) && m_Character.devMode) m_Move *= 5f;
            if (Input.GetKey(KeyCode.LeftAlt)) m_Move *= 0.5f;
#endif
			if (Input.GetKey(KeyCode.LeftShift) && m_Character.m_IsGrounded == true)
            {
                if (!dashTimer)
                    dash = true;
                dashTimer = true;
            }
            // pass all parameters to the character control script
            if (dashTimer)
            {
                timer += Time.deltaTime;
                if (timer > 0.15f)
                {
                    dash = false;
                }
                if (timer > dashCooldown)
                {
                    timer = 0;
                    dashTimer = false;
                }
            }
            m_Character.Move(m_Move, crouch, m_Jump, dash, m_float);
            if(m_Jump == 2 || m_Character.m_IsGrounded == true)
            {
                m_Jump = 0;
            }


        }
    }
}
