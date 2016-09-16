using UnityEngine;
using System.Collections;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class LockOnBubble : MonoBehaviour
    {
        CapsuleCollider bubble;
        public bool closeMode;
        GameObject player;
        ThirdPersonUserControl playerControl;
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerControl = player.GetComponent<ThirdPersonUserControl>();
            bubble = GetComponent<CapsuleCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (closeMode == false)
            {
                playerControl.closeMode = false;
                bubble.enabled = false;
            }
            else if (closeMode)
            {
                playerControl.closeMode = true;
                bubble.enabled = true;
            }
        }

        void OnTriggerEnter(Collider hit)
        {
            if(hit.tag == "Player" && playerControl.lockedOn)
            {
                closeMode = true;
            }
        }
        void OnTriggerExit(Collider hit)
        {
            if (hit.tag == "Player")
            {
                closeMode = false;
            }
        }
    }
}
