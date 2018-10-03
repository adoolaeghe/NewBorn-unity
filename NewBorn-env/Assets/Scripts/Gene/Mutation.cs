using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Mutation    {
    public bool storeGene;
    public int numLayers;
    public string uuid;
    public List<int> axisParts;
    public List<NoiseLayerParams> noiseLayersParams;
    public float angularYLimit;
    public float highAngularXLimit;
    public float lowAngularXLimit;
    public float roughness;
    public float baseRoughness;
    public float strength;
    public int resolution;
    public Vector3 radius;


    public Mutation(bool newMutation, int axis, float energy, List<Mutation> mutations)
    {
        resolution = 120;
        noiseLayersParams = new List<NoiseLayerParams>();
        if(newMutation) {
            //
            // TEMPORARY ONLY FIRST AXIS PART IS VALID
			axisParts = new List<int>() { 1, 1, 1, 1, 1, 1 };
            //
            //
            float radiusX = 1f;
            float radiusY = 1f;
            float radiusZ = 1f;

            if (axis == 1 || axis == 2){
                radiusX = Random.Range(1f, 4f) * energy;
            } else if(axis == 3 || axis == 4) {
                radiusY = Random.Range(1f, 4f) * energy;
            } else if(axis == 5 || axis == 6) {
                radiusZ = Random.Range(1f, 4f) * energy;
            }

			radius = new Vector3(radiusX, radiusY, radiusZ);
		
			angularYLimit = Random.Range(0f, 90f);
			highAngularXLimit = Random.Range(20f, 80f);
			lowAngularXLimit = Random.Range(0f, 20f);

            roughness = Random.Range(0.5f, 1.5f);
            baseRoughness = Random.Range(0.5f, 1.5f);
        	strength = Random.Range(0.5f, 1.5f);

            // LOOP OVER THE NUMBER OF NOISE LAYERS
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    noiseLayersParams.Add(new NoiseLayerParams(baseRoughness, roughness, 1f, 8, strength, true));
                }
                else 
                {
                    var nLParam = noiseLayersParams[i - 1];
                    noiseLayersParams.Add(new NoiseLayerParams(calculateNLParams(nLParam.baseRoughness), calculateNLParams(nLParam.roughness), nLParam.persistence, nLParam.numLayers, nLParam.strength, false));
                }
            }
        }
        else 
        {
            // EMPHASIS ON SYMETRY // 
            // DISTRIBUTION OF ENERGY // 
            axisParts = mutations[0].axisParts;
			angularYLimit = mutations[0].angularYLimit;
			highAngularXLimit = mutations[0].highAngularXLimit;
			lowAngularXLimit = mutations[0].lowAngularXLimit;
			for (int i = 0; i < 6; i++)
			{
                var useFirstLayerAsMask = i == 0 ? true : false;
                var noiseLayersParam = mutations[0].noiseLayersParams[i]; 
				noiseLayersParams.Add(new NoiseLayerParams(noiseLayersParam.baseRoughness,noiseLayersParam.roughness, noiseLayersParam.persistence, noiseLayersParam.numLayers, noiseLayersParam.strength, useFirstLayerAsMask));
			}
            radius = new Vector3(mutations[0].radius.x, mutations[0].radius.y, mutations[0].radius.z) * energy ;
        }
    }

    float calculateNLParams(float param) {
        // DIVIDE FORMULA
        return Mathf.Sin(param);
    }
}

public class NoiseLayerParams
{
	public float baseRoughness;
	public float roughness;
	public float persistence;
	public float strength;
    public int numLayers;
    public bool useFirstLayerAsMask;

	public NoiseLayerParams(float baseRoughness, float roughness, float persistence, int numLayers, float strength, bool useFirstLayerAsMask)
	{
		this.baseRoughness = baseRoughness;
		this.roughness = roughness;
		this.persistence = persistence;
		this.numLayers = numLayers;
		this.strength = strength;
        this.useFirstLayerAsMask = useFirstLayerAsMask;
	}
}