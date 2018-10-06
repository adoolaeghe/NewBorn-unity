using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGenerator : MonoBehaviour
{

	ColourSettings settings;
    public Texture2D texture;
    public TextAsset imageAsset;
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
        ///////////////////////////////////////////////////////////////////////////
    }

    public void UpdateColours()
    {

        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices = mesh.vertices;
        //Vector2[] uvs = new Vector2[vertices.Length];

        //for (int i = 0; i < uvs.Length; i++)
        //{
        //    uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        //}
        //mesh.uv = uvs;
        //settings.planetMaterial.SetVector("_u", uvs);
    }
}