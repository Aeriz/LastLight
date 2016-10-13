using UnityEngine;
using System.Collections;

public class LensScript : MonoBehaviour {
    public Color[] colours;
    public Color newColour;
    public int beamCounter = 0;
    public GameObject lensOne;
    public GameObject lensTwo;
    public int lightBeamInt = 0;
	// Use this for initialization
	void Start ()
    {
        newColour = Color.clear;
        colours = new Color[2];
        colours[0] = newColour;
        colours[1] = newColour;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(beamCounter > 1)
        {
            beamCounter = 1;
        }
	}
}
