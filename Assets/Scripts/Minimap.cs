using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

    public Transform Target;

	// Use this for initialization
	void Start () {
	
	}

    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        //transform.rotation = Quaternion.Euler(new Vector3(90, Target.transform.eulerAngles.y, 0));
    }

}
