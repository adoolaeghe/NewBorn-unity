using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator {

    ShapeSettings settings;
    INoiseFilter[] noiseFilters;

    public float elevationMax;
    public float elevationMin;
    public Vector3 positionMax;
    public Vector3 positionMin;
    public MinMax elevationMinMax;

    public void UpdateSettings(ShapeSettings settings) 
    {
        if(settings != null){
			this.settings = settings;
			noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
            for (int i = 0; i < noiseFilters.Length; i++)
			{
				noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
			}
			elevationMinMax = new MinMax();   
        }
    }

	public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere, Vector3[] vertices, int resolution, int index)
	{
		float firstLayerValue = 0;
		float elevation = 0;

        if (noiseFilters.Length > 0)
		{
			firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
			if (settings.noiseLayers[0].enabled)
			{
				elevation = firstLayerValue;
			}
		}

		for (int i = 1; i < noiseFilters.Length; i++)
		{
			if (settings.noiseLayers[i].enabled)
			{
				float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
				elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }

        Vector3 returnVal = new Vector3(pointOnUnitSphere.x * settings.planetRadius.x, pointOnUnitSphere.y * settings.planetRadius.y, pointOnUnitSphere.z * settings.planetRadius.z);  
        /////////// YOU SHOULD REFACTOR THIS USING THE MINMAX//////////////
		if (elevationMax < (returnVal * (1 + elevation)).x + (returnVal * (1 + elevation)).y + (returnVal * (1 + elevation)).z)
		{
			elevationMax = (returnVal * (1 + elevation)).x + (returnVal * (1 + elevation)).y + (returnVal * (1 + elevation)).z;
			positionMax = returnVal * (1 + elevation);
		}
		if (elevationMin > (returnVal * (1 + elevation)).x + (returnVal * (1 + elevation)).y + (returnVal * (1 + elevation)).z)
		{
			elevationMin = (returnVal * (1 + elevation)).x + (returnVal * (1 + elevation)).y + (returnVal * (1 + elevation)).z;
			positionMin = returnVal * (1 + elevation);
		}
        ///////////////////////////////:
        elevation = (1 + elevation);
        elevationMinMax.AddValue(elevation, returnVal * elevation);
        elevationMinMax.analyse(returnVal * elevation, vertices, resolution, index);

        return returnVal * elevation;
	}
}
