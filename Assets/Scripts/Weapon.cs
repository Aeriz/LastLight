using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public Transform HandR;
	// Use this for initialization
	void Start () {
        transform.parent = HandR;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
