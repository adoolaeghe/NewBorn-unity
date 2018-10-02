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
        gradient = new Gradient();
        gck = new GradientColorKey[2];
        gck[0].color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        gck[0].time = 0.0F;
        gck[1].color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        gck[1].time = 1.0F;
        gak = new GradientAlphaKey[2];
        gak[0].alpha = 1.0F;
        gak[0].time = 0.0F;
        gak[1].alpha = 0.0F;
        gak[1].time = 1.0F;
        gradient.SetKeys(gck, gak);
        planetMaterial = new Material(Shader.Find("Planet"));     
    }
}
