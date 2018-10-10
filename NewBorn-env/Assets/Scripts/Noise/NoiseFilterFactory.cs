using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory {

    public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
    {
        Debug.Log(settings.filterType);
        switch (settings.filterType)
        {
            case NoiseSettings.FilterType.Simple:
                return new SimpleNoiseFilter(settings);
            case NoiseSettings.FilterType.Ridgid:
                return new RidgidNoiseFilter(settings);
        }
        return null;
    }
}
