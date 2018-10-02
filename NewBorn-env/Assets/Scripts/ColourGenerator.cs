using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGenerator
{

	ColourSettings settings;
    public Texture2D texture;
    const int textureResolution = 50;
    public bool hello = false;

	public void UpdateSettings(ColourSettings settings)
	{
		this.settings = settings;
        if(texture == null) {
         texture = new Texture2D(textureResolution, 1);   
        }
	}

	public void UpdateElevation(MinMax elevationMinMax, Vector3 position)
	{
        //settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));

        // Testing with 0 to 10 value we can imagine a place where it would grow //
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(0f, 10f));
        ///////////////////////////////////////////////////////////////////////////
        settings.planetMaterial.SetVector("_position", position);
    }

    public void UpdateColours()
    {
        Color[] colours = new Color[textureResolution];
        for (int i = 0; i < textureResolution; i++)
        {
            colours[i] = settings.gradient.Evaluate(i / (textureResolution - 1f));
        }
        texture.SetPixels(colours);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);
        hello = true;
    }
}