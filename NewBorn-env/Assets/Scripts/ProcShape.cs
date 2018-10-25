using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcShape : MonoBehaviour {

    [Range(2,256)]
    public int resolution = 10;
    public int[] resolutions;

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
        NoiseLayer[] noiseLayers = new NoiseLayer[6];
        //
        // NEED TO UPDATE THIS 
        noiseLayers[0] = new NoiseLayer();
		noiseLayers[1] = new NoiseLayer();
        noiseLayers[2] = new NoiseLayer();
        noiseLayers[3] = new NoiseLayer();
        noiseLayers[4] = new NoiseLayer();
        noiseLayers[5] = new NoiseLayer();
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

    public void UpdateSettings(ShapeSettings shapeSettings)
    {
        shapeGenerator.UpdateSettings(shapeSettings);
    }

    public void GenerateMesh() {

        foreach (TerrainFace face in terrainFaces) 
        {
            face.ConstructMesh();
        }
        //////////////////////////////////////////////////////
        /// FILTER THE PEAK/HOLE ANALYSIS FROM MESH GENERATION
        shapeGenerator.elevationMinMax.holePeakFilter();
      

        foreach (var peak in shapeGenerator.elevationMinMax.peaks)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = gameObject.transform;
            sphere.transform.localPosition = peak;
        }

       
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = gameObject.transform;
        cube.transform.localPosition = shapeGenerator.elevationMinMax.PosMin;

        //////////////////////////////////////////////////////
        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax, transform.position);
    }

    void GenerateColours() {
        foreach(MeshFilter m in meshFilters)
        {
            colourGenerator.UpdateColours();    
        }
    }
    
}
