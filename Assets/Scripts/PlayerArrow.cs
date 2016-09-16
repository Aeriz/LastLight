using UnityEngine;
using System.Collections;

public class PlayerArrow : MonoBehaviour {
    public Transform Target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     
	}

    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        //transform.Rotate(Target.rotation.x, Target.rotation.y, Target.rotation.z);
        transform.rotation = Quaternion.Euler(0.0f, Target.rotation.eulerAngles.y + 180, 0.0f);
    }
}
