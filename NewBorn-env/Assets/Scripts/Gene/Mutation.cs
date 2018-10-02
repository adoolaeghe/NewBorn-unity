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


    public Mutation(int partNum, int axisNum, float energy, List<Mutation> mutations)
    {
        resolution = 80;
        noiseLayersParams = new List<NoiseLayerParams>();
        if(partNum == 0) {
            //
            // TEMPORARY ONLY FIRST AXIS PART IS VALID
			axisParts = new List<int>() { 1, 1, 1, 1, 1, 1 };
            //
            //
            float radiusX = Random.Range(1f, 1f) * energy;
            float radiusY = Random.Range(1f, 1f) * energy;
            float radiusZ = Random.Range(1f, 1f) * energy;

			radius = new Vector3(radiusX, radiusY, radiusZ);
		
			angularYLimit = Random.Range(0f, 90f);
			highAngularXLimit = Random.Range(20f, 80f);
			lowAngularXLimit = Random.Range(0f, 20f);

			roughness = Random.Range(0.5f, 1.5f);
        	baseRoughness = Random.Range(0.5f, 1.5f);
        	strength = Random.Range(0.5f, 1.5f);

            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    noiseLayersParams.Add(new NoiseLayerParams(baseRoughness, roughness, 1f, 8, strength, true));
                }
                else 
                {
                    var nLParam = noiseLayersParams[i - 1];
                    noiseLayersParams.Add(new NoiseLayerParams(calculateNLParams(nLParam.baseRoughness), calculateNLParams(nLParam.roughness), nLParam.persistence, nLParam.numLayers, calculateNLParams(nLParam.strength), false));
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
        return param * (1.5f - param);
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