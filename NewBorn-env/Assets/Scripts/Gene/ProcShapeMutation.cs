using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcShapeMutation
{

    public int resolution;
    public int numLayers;
    public float strength;
    public float baseRoughness;
    public float roughness;
    public float persistence;
    public Vector3 radius;

    public ProcShapeMutation() {
        resolution = 40;
        numLayers = 6;
        strength = 1f;
        baseRoughness = 1.3f;
        roughness = 1f;
        persistence = 1f;
        radius = new Vector3(1f, 1f, 1f);
    }
}
