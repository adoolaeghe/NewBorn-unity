using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinMax {
    public List<float> elevations;

    public float Min { get; private set; }
    public float Max { get; private set; }

    public Vector3 PreVal { get; private set; }
    public Vector3 CurVal { get; private set; }
    public Vector3 PostVal { get; private set; }

    public Vector3 PosMin { get; private set; }
    public Vector3 PosMax { get; private set; }

    public List<Vector3> peaks;
    public List<Vector3> holes;

    public MinMax() 
    {
        elevations = new List<float>();
        peaks = new List<Vector3>();
        holes = new List<Vector3>();

        Min = float.MaxValue;
        Max = float.MinValue;

        PreVal = new Vector3(0f, 0f, 0f);
        CurVal = new Vector3(0f, 0f, 0f);
        PostVal = new Vector3(0f, 0f, 0f);
    }

    public void analyse(Vector3 position, Vector3[] vertices, int resolution, int i) {
        if (i > ((resolution * 2) + 2)) {
            float center = vertices[i - resolution - 1].magnitude;
            float left = vertices[i - resolution - 2].magnitude;
            float right = vertices[i - resolution].magnitude;
            float top = vertices[i - (resolution * 2) - 1].magnitude;
            float topLeft = vertices[i - (resolution * 2) - 2].magnitude;
            float topRight = vertices[i - (resolution * 2)].magnitude;
            float bottom = vertices[i - 1].magnitude;
            float bottomRight = vertices[i].magnitude;
            float bottomLeft = vertices[i - 2].magnitude;

            if (center > left + 0.05f &&
               center > right + 0.05f &&
               center > top + 0.05f &&
               center > bottom + 0.05f) 
            {
                peaks.Add(vertices[i - resolution - 1]);
            }

            if (left > center &&
               right > center &&
               top > center &&
               bottom > center)
            {
                holes.Add(vertices[i - resolution - 1]);
            }
        }
    }

    public void filter(float margin) 
    {
        List<Vector3> filterPeaks = peaks.OrderByDescending(item => item.magnitude).ToList();
        List<Vector3> topPeaks = new List<Vector3>();

        float average = 0f;

        peaks.Clear();
        if (filterPeaks.Count > 0)
        {
            peaks.Add(filterPeaks[0]);

            for (int i = 1; i < filterPeaks.Count; i++)
            {
                average += filterPeaks[i].magnitude;
            }
            average = average / filterPeaks.Count;

            for (int y = 1; y < filterPeaks.Count; y++)
            {
                if(filterPeaks[y].magnitude > average) {
                    topPeaks.Add(filterPeaks[y]);
                }
            }

            for (int x = 1; x < 4; x++)
            {
                peaks.Add(topPeaks[Random.Range(0, topPeaks.Count)]);
            }
            
        }


        //foreach (var item in peaks)
        //{
        //    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    sphere.transform.parent = gameObject.transform;
        //    sphere.transform.localPosition = item;
        //}

        //foreach (var item in shapeGenerator.elevationMinMax.holes)
        //{
        //    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    sphere.transform.parent = gameObject.transform;
        //    sphere.transform.localPosition = item;
        //}
    }

    public void AddValue(float v, Vector3 position)
    {
        elevations.Add(v);

        if(v > Max)
        {
            Max = v;
            PosMax = position;
        }
        if(v < Min)
        {
            Min = v;
            PosMin = position;
        }
    }
}
