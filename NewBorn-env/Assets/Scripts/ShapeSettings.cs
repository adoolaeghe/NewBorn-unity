using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSettings
{
    public Vector3 planetRadius;
    public NoiseLayer[] noiseLayers;

    public ShapeSettings(Vector3 planetRadius, NoiseLayer[] noiseLayers)
    {
        this.planetRadius = new Vector3(1f, 1f, 1f);
        this.noiseLayers = noiseLayers;
    }
}

