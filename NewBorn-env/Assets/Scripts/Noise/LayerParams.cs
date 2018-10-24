using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LayerParams
{
    public float[] layer1;
    public float[] layer2;
    public float[] layer3;
    public float[] layer4;
    public float[] layer5;
    public float[] layer6;

    public LayerParams()
    {
        float shapeBR = Random.Range(0, 0.1f); float textureBR = Random.Range(0, 2f);
        float shapeR = Random.Range(0, 0.1f); float textureR = Random.Range(0, 2f);
        float shapeP = 1f; float textureP = 1f;
        float shapeS = 5f; float textureS = 0.1f;
        // 1/ BaseRoughness 2/ Rougheness 3/ persistence 4/ strength
        this.layer1 = new float[]{shapeBR, shapeR, shapeP, shapeS}; // BASEROUGHNESS||ROUGHNESS: between 0.1 and 0.25 // STRENGHT: 5 (gives the basic shape deformation)
        this.layer2 = new float[]{textureBR, textureR, textureP, textureS}; // BaSEROUGHNESS||ROUGHNESS: between 0 to 5f // STRENGHT: 0.1 (gives some texture, more like skin)
        this.layer3 = new float[]{shapeBR * 2, shapeR * 2, shapeP / 2, shapeS / 2};
        this.layer4 = new float[]{textureBR / 2, textureR / 2, textureP, textureS * 2};
        this.layer5 = new float[]{shapeBR * 4, shapeR * 4, shapeP / 4, shapeS / 4};
        this.layer6 = new float[]{textureBR / 4, textureR / 4, textureP, textureS * 4}; ;
    }
}
