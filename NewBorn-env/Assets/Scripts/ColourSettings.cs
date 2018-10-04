using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettings {

    public Gradient gradient;
    public Material planetMaterial;

    public ColourSettings() 
    {
        gradient = new Gradient();
        planetMaterial = new Material(Shader.Find("Planet"));     
    }
}
