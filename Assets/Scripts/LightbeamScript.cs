using UnityEngine;
using System.Collections;

public class LightbeamScript : MonoBehaviour {
    public Material beamMaterial;
    public Shader shader;
    public Texture texture;
    public Color color;
    Renderer rend;

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        shader = beamMaterial.shader;
        texture = beamMaterial.mainTexture;
        color = Color.red;

        

        rend.material = new Material(shader);
        rend.material.mainTexture = texture;
        rend.material.SetColor("_TintColor", color);
        
        //beamMaterial = GetComponent<Material>();
        //this.beamMaterial.SetColor("_TintColor", Color.blue);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void changeColor(Color colour)
    {
        color = colour;
        rend.material.SetColor("_TintColor", colour);
    }
}
