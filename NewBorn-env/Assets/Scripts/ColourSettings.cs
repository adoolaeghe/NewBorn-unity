using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettings {

    public Gradient gradient;
    GradientColorKey[] gck;
    GradientAlphaKey[] gak;
    public Material planetMaterial;

    public ColourSettings() 
    {
        planetMaterial = Resources.Load<Material>("Materials/Test");
    }
}
