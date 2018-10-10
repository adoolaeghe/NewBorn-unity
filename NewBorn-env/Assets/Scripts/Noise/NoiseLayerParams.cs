using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseLayerParams
{
    public float baseRoughness;
    public float roughness;
    public float persistence;
    public float strength;
    public float[] layerParams;
    public int numLayers;
    public bool useFirstLayerAsMask;
    public Vector3 centre;
    public NoiseSettings.FilterType filterType;
    public NoiseLayerParams(float[] layerParams, Vector3 centre, bool useFirstLayerAsMask, NoiseSettings.FilterType filterType)
    {
        this.layerParams = layerParams;
        this.baseRoughness = layerParams[0];
        this.roughness = layerParams[1];
        this.persistence = layerParams[2];
        this.numLayers = 8;
        this.strength = layerParams[3];
        this.centre = centre;
        this.useFirstLayerAsMask = useFirstLayerAsMask;
        this.filterType = filterType;
    }
}