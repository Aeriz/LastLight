using UnityEngine;
using System.Collections;

//Written by Zachary De Maria

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class TestMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public float turnRate = 90.0f;
    public float jumpVel = 1.0f;
    public float slopeLimit = 45.0f;
    public bool bFirstPerson = true;
    private Vector3 _eyeAngles = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 eyeAngles
    {
        get
        {
            return _eyeAngles;
        }
    }
    private Rigidbody rb;
    private Transform camTransform;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camTransform = transform.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rc = new RaycastHit();
        float moveForwardBack = Input.GetAxis("Vertical");
        float moveLeftRight = Input.GetAxis("Horizontal");
        float aimUpDown = -Input.GetAxis("Mouse Y");
        float aimLeftRight = Input.GetAxis("Mouse X");

        if (Physics.Raycast(transform.position, -transform.up, out rc, 1.01f))
        {
            Vector3 v = rb.velocity;
            v.y = 0;

            if ((Mathf.Abs(Vector3.Angle(rc.normal, transform.up)) < slopeLimit))
            {
                if (Input.GetButton("Jump"))
                {
                    v.y = jumpVel;
                }
            }
            rb.velocity = v;
        }

        Vector3 rotVec = new Vector3(aimUpDown * turnRate * Time.deltaTime, aimLeftRight * turnRate * Time.deltaTime, 0.0f);
        Vector3 moveVec = new Vector3(moveLeftRight, 0, moveForwardBack);

        _eyeAngles = new Vector3(Mathf.Min(Mathf.Max(_eyeAngles.x + rotVec.x, -90.0f), 90.0f), (_eyeAngles.y + rotVec.y) % 359, 0.0f);
        Vector3 charAng = _eyeAngles;
        charAng.x = 0.0f;
        transform.rotation = Quaternion.Euler(charAng);

        if (bFirstPerson)
        {
            camTransform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
            camTransform.rotation = Quaternion.Euler(_eyeAngles);
        }
        else
        {
            camTransform.localPosition = (Quaternion.Euler((_eyeAngles - charAng)) * new Vector3(1.0f, 2.0f, -5.0f));
            camTransform.localRotation = Quaternion.Euler((_eyeAngles - charAng));
        }
        if (Physics.Raycast(transform.position, Quaternion.Euler(charAng) * moveVec, out rc, 1))
        {
            print(Mathf.Abs(Vector3.Angle(rc.normal, transform.up)));
            if (Mathf.Abs(Vector3.Angle(rc.normal, transform.up)) < slopeLimit)
            {
                rb.AddForce((Quaternion.Euler(charAng) * moveVec * speed) - new Vector3(rb.velocity.x, 0, rb.velocity.z), ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce((Quaternion.Euler(-charAng) * new Vector3(0, 0, 0.01f) * speed) - new Vector3(rb.velocity.x, 0, rb.velocity.z), ForceMode.VelocityChange);
            }
        }
        else
        {
            rb.AddForce((Quaternion.Euler(charAng) * moveVec * speed) - new Vector3(rb.velocity.x, 0, rb.velocity.z), ForceMode.VelocityChange);
        }
    }
}