using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcShape : MonoBehaviour {

    [Range(2,256)]
    public int resolution = 20;
    public int[] resolutions;
    public bool autoUpdate = true;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
	[HideInInspector]
	public bool colourSettingsFoldout;

    public ShapeGenerator shapeGenerator = new ShapeGenerator();
    public ColourGenerator colourGenerator = new ColourGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    public TerrainFace[] terrainFaces;

    public void Initialize()
    {
        NoiseLayer[] noiseLayers = new NoiseLayer[2];
        //
        // NEED TO UPDATE THIS 
        noiseLayers[0] = new NoiseLayer();
		noiseLayers[1] = new NoiseLayer();
        colourSettings = new ColourSettings();
        //
        //
        shapeSettings = new ShapeSettings(new Vector3(1f, 1f, 1f), noiseLayers);
        shapeGenerator.UpdateSettings(shapeSettings);
		colourGenerator.UpdateSettings(colourSettings);

        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];    
        }
        terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
      

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("side" + (i + 1).ToString());
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
        GenerateColours();
    }

    public void GenerateProcShape() 
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated() 
    {
        if(autoUpdate) 
        {
			Initialize();
            GenerateMesh();   
        }
    }

    public void OnColourSettingsUpdated() 
    {
        if(autoUpdate)
        {
			Initialize();
            GenerateColours();   
        }
    }

    public void GenerateMesh() {
        foreach (TerrainFace face in terrainFaces) 
        {
            face.ConstructMesh();    
        }
        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax, transform.position);
    }

    void GenerateColours() {
        foreach(MeshFilter m in meshFilters)
        {
            colourGenerator.UpdateColours();    
        }
    }

	void mergeMesh()
	{
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		int i = 0;
		while (i < meshFilters.Length)
		{
            combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.active = false;
            i++;
		}
        MeshFilter meshFilter = transform.gameObject.AddComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
		meshFilter.mesh.CombineMeshes(combine, true, true);
		transform.gameObject.active = true;
	}
}
