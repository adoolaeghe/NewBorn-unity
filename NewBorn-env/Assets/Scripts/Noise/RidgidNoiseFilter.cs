using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : INoiseFilter {

	NoiseSettings settings;
	Noise noise = new Noise();

	public RidgidNoiseFilter(NoiseSettings settings)
	{
		this.settings = settings;
	}


    public float Evaluate(Vector3 point)
	{
		float noiseValue = 0;
		float frequency = settings.baseRoughness;
		float amplitude = 1;
        float weight = 1;

        float v = 1-Mathf.Abs(noise.Evaluate(point * frequency + settings.center));
        v *= v;
        v *= weight;
        weight = v;

        noiseValue += v * amplitude;
		frequency *= settings.roughness;
		amplitude *= settings.persistence;
        //STORE THE NOISE VALUE

		noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
		return noiseValue * settings.strength;
	}
}
