using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinMax {
    public List<float> elevations;

    public bool allowPeak { get; private set; }
    public bool allowHole { get; private set; }

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
        allowPeak = true;
        allowHole = false;

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

            if (center > left + 0.2 &&
                center > right + 0.2 &&
                center > top + 0.2 &&
                center > bottom + 0.2 &&
                allowPeak) 
            {
                bool allow = true;
                foreach (var peak in peaks)
                {
                    if((peak.x < vertices[i - resolution - 1].x - 3f && peak.x > vertices[i - resolution - 1].x + 3f) &&
                       (peak.y < vertices[i - resolution - 1].y - 3f && peak.y > vertices[i - resolution - 1].y + 3f) &&
                       (peak.z < vertices[i - resolution - 1].z - 3f && peak.z > vertices[i - resolution - 1].z + 3f))
                    {
                        allow = false;
                    }
                }

                if(allow) {
                    peaks.Add(vertices[i - resolution - 1]);
                    allowHole = !allowHole;
                    allowPeak = !allowPeak;
                }
            }

            if (left > center &&
                right > center &&
                top > center &&
                bottom > center &&
                allowHole)
            {
                holes.Add(vertices[i - resolution - 1]);
                allowHole = !allowHole;
                allowPeak = !allowPeak;
            }
        }
    }

    public void holePeakFilter() 
    {
        //List<Vector3> filterPeaks = peaks.OrderByDescending(item => item.magnitude).ToList();
        //List<Vector3> filterHoles = holes.OrderByDescending(item => item.magnitude).ToList();
        //List<Vector3> topPeaks = new List<Vector3>();
        //List<Vector3> topHoles = new List<Vector3>();

        //float peakAverage = 0f;
        //float holeAverage = 0f;

        //peaks.Clear();
        //holes.Clear();

        //if (filterPeaks.Count > 0 && filterHoles.Count > 0)
        //{
            //peaks.Add(filterPeaks[0]);

            //for (int i = 1; i < filterPeaks.Count; i++)
            //{
            //    peakAverage += filterPeaks[i].magnitude;
            //}
            //for (int i = 1; i < filterHoles.Count; i++)
            //{
            //    holeAverage += filterHoles[i].magnitude;
            //}

            //peakAverage = peakAverage / filterPeaks.Count;
            //holeAverage = holeAverage / filterHoles.Count;

            //for (int y = 1; y < filterPeaks.Count; y++)
            //{
            //    //if(filterPeaks[y].magnitude > peakAverage) {
            //        peaks.Add(filterPeaks[y]);
            //    //}
            //}

            //for (int y = 1; y < filterHoles.Count; y++)
            //{
            //    //if (filterHoles[y].magnitude < holeAverage)
            //    //{
            //        holes.Add(filterHoles[y]);
            //    //}
            //}

            ////for (int x = 0; x < 4; x++)
            ////{
            ////    peaks.Add(topPeaks[x]);
            ////    holes.Add(topHoles[x]);
            ////}

            //peaks = peaks.OrderByDescending(item => item.magnitude).ToList();
        //    //holes = holes.OrderBy(item => item.magnitude).ToList();
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
