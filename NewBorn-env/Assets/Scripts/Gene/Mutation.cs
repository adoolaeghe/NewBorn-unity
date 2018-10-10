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
    public NoiseSettings.FilterType filterType;


    public Mutation(string mutationType, int partNb, int axis, float energy, List<Mutation> mutations)
    {
        resolution = 20;
        noiseLayersParams = new List<NoiseLayerParams>();
        if(mutationType == "base" || mutationType == "division") {
            //
            // TEMPORARY ONLY FIRST AXIS PART IS VALID
			axisParts = new List<int>() { 1, 1, 1, 1, 1, 1 };
            //
            //
            float radiusX = 1f;
            float radiusY = 1f;
            float radiusZ = 1f;

            // THIS CODE WOULD BE RESTORES WHEN THE MUTATION WOULD TAKE INTO ACCOUNT THE AXIS
            // DEFINE THE RADIUS ACCORDING TO THE AXIS
            //if (axis == 1 || axis == 2){
            //    radiusX = Random.Range(1f, 4f);
            //} else if(axis == 3 || axis == 4) {
            //    radiusY = Random.Range(1f, 4f);
            //} else if(axis == 5 || axis == 6) {
            //    radiusZ = Random.Range(1f, 4f);
            //}

            radius = new Vector3(radiusX, radiusY, radiusZ) * energy; ;
		
			angularYLimit = Random.Range(0f, 30f);
			highAngularXLimit = Random.Range(10f, 20f);
			lowAngularXLimit = Random.Range(0f, 20f);

            roughness = Random.Range(0.5f, 1f);
            baseRoughness = Random.Range(0.5f, 1.5f);
        	strength = Random.Range(0.5f, 1.5f);

            //noiseLayersParams.Add(new NoiseLayerParams(0f, -2.3f, 1f, 8, 0.26f, false, NoiseSettings.FilterType.Ridgid));
            //noiseLayersParams.Add(new NoiseLayerParams(1.3f, -0.04f, 0.55f, 8, 0.08f, true, NoiseSettings.FilterType.Simple));
            //noiseLayersParams.Add(new NoiseLayerParams(1.87f, 0.11f, -0.45f, 8, 1.87f, false, NoiseSettings.FilterType.Simple));
            //noiseLayersParams.Add(new NoiseLayerParams(-0.13f, -0.06f, 0.66f, 8, 1.34f, false, NoiseSettings.FilterType.Ridgid));
            //noiseLayersParams.Add(new NoiseLayerParams(1.84f, 6.34f, 0f, 8, 0.05f, false, NoiseSettings.FilterType.Ridgid));
            //noiseLayersParams.Add(new NoiseLayerParams(0.23f, 0.6f, 0.57f, 4, -0.3f, false, NoiseSettings.FilterType.Simple));

            //noiseLayersParams.Add(new NoiseLayerParams(0.2f, 1.3f, 1f, 2f, centre, true, NoiseSettings.FilterType.Simple)); // init SHAPE LAYER
            //noiseLayersParams.Add(new NoiseLayerParams(1f, 1f, 1f, 0.1f, centre, false, NoiseSettings.FilterType.Ridgid)); // init TEXTURE LAYER
            //noiseLayersParams.Add(new NoiseLayerParams(0.2f, 0.1f, 0.45f, 0.3f, centre, false, NoiseSettings.FilterType.Simple)); // secondary SHAPE LAYER
            //noiseLayersParams.Add(new NoiseLayerParams(0f, 2f, 0f, 0.005f, centre, false, NoiseSettings.FilterType.Ridgid)); // secondary TEXTURE LAYER
            //noiseLayersParams.Add(new NoiseLayerParams(0f, 0f, 0f, 0f, centre, false, NoiseSettings.FilterType.Ridgid));
            //noiseLayersParams.Add(new NoiseLayerParams(0f, 0f, 0f, 0f, centre, false, NoiseSettings.FilterType.Simple));

            Vector3 centre = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));
            LayerParams layerParams = new LayerParams();

            noiseLayersParams.Add(new NoiseLayerParams(layerParams.layer1, centre, true, NoiseSettings.FilterType.Simple)); // init SHAPE LAYER
            noiseLayersParams.Add(new NoiseLayerParams(layerParams.layer2, centre, false, NoiseSettings.FilterType.Ridgid)); // init TEXTURE LAYER
            noiseLayersParams.Add(new NoiseLayerParams(layerParams.layer3, centre, false, NoiseSettings.FilterType.Simple)); // secondary SHAPE LAYER
            noiseLayersParams.Add(new NoiseLayerParams(layerParams.layer4, centre, false, NoiseSettings.FilterType.Ridgid)); // secondary TEXTURE LAYER
            noiseLayersParams.Add(new NoiseLayerParams(layerParams.layer5, centre, false, NoiseSettings.FilterType.Ridgid));
            noiseLayersParams.Add(new NoiseLayerParams(layerParams.layer6, centre, false, NoiseSettings.FilterType.Simple));
        }
        else 
        {
            // EMPHASIS ON SYMETRY // 
            // DISTRIBUTION OF ENERGY // 
            axisParts = mutations[partNb - 1].axisParts;
			angularYLimit = mutations[partNb - 1].angularYLimit;
			highAngularXLimit = mutations[partNb - 1].highAngularXLimit;
			lowAngularXLimit = mutations[partNb - 1].lowAngularXLimit;

			for (int i = 0; i < 6; i++)
			{
                var useFirstLayerAsMask = i == 0 ? true : false;
                var noiseLayersParam = mutations[partNb - 1].noiseLayersParams[i];
                Debug.Log(noiseLayersParam);
                noiseLayersParams.Add(new NoiseLayerParams(noiseLayersParam.layerParams, noiseLayersParam.centre, useFirstLayerAsMask, noiseLayersParam.filterType));
			}
            radius = new Vector3(mutations[partNb - 1].radius.x, mutations[partNb - 1].radius.y, mutations[partNb - 1].radius.z);
        }
    }

    float calculateNLParams(float param) {
        // DIVIDE FORMULA
        //return Mathf.Sin(param);
        return param + 4;
    }
}
