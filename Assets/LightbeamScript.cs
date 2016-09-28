using UnityEngine;
using System.Collections;

public class LightbeamScript : MonoBehaviour {
    public Material beamMaterial;
    public Shader shader;
    public Texture texture;
    public Color color;
    
	// Use this for initialization
	void Start ()
    {
        shader = beamMaterial.shader;
        color = Color.red;

        Renderer rend = GetComponent<Renderer>();
        rend.material = new Material(shader);
        rend.material.mainTexture = beamMaterial.mainTexture;
        rend.material.SetColor("_TintColor", color);
        
        //beamMaterial = GetComponent<Material>();
        //this.beamMaterial.SetColor("_TintColor", Color.blue);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
