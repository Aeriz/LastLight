using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour
{
    public bool lightOnKey = false;
    public bool unlock = false;
    public float timer = 0;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (lightOnKey)
        {
            timer += Time.deltaTime;
            if(timer > 3)
            {
                unlock = true;
            }
        }
	}

}
