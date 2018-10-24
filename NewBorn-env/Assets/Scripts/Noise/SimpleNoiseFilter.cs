using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    NoiseSettings settings;
	Noise noise = new Noise();

	public SimpleNoiseFilter(NoiseSettings settings)
	{
		this.settings = settings;
	}

    public float Evaluate(Vector3 point)
	{
		float noiseValue = 0;
		float frequency = settings.baseRoughness;
		float amplitude = 1;
		float v = noise.Evaluate(point * frequency + settings.center);
        //NOISE VALUE IS WHAT YOU NEED TO BE WORKING WITH
		noiseValue += (v + 1) * .5f * amplitude;
		frequency *= settings.roughness;
		amplitude *= settings.persistence;

		noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
		return noiseValue * settings.strength;
	}
}