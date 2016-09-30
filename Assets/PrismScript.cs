using UnityEngine;
using System.Collections;

public class PrismScript : MonoBehaviour {
    public Color[] colours;
    float r;
    float g;
    float b;
    float a;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Color addColours()
    {
        for(int i = 0; i <= colours.Length; i++)
        {
            r += colours[i].r;
            g += colours[i].g;
            b += colours[i].b;
            a += colours[i].a;
        }
        if(r > 1)
        {
            r = 1;
        }
        if (g > 1)
        {
            g = 1;
        }
        if (b > 1)
        {
            b = 1;
        }
        if (a > 1)
        {
            a = 1;
        }
        Vector4 temp = new Vector4(r, g, b, a);
        Color tempColour;
        tempColour = temp;
        return tempColour;
    }

    
}
