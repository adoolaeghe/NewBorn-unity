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

    public float margin { get; private set; }

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

        margin = 5f;

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
                    if((peak.x < vertices[i - resolution - 1].x - margin && peak.x > vertices[i - resolution - 1].x + margin) &&
                       (peak.y < vertices[i - resolution - 1].y - margin && peak.y > vertices[i - resolution - 1].y + margin) &&
                       (peak.z < vertices[i - resolution - 1].z - margin && peak.z > vertices[i - resolution - 1].z + margin))
                    {
                        allow = false;
                    }
                }

                foreach (var hole in holes)
                {
                    if ((hole.x < vertices[i - resolution - 1].x - margin && hole.x > vertices[i - resolution - 1].x + margin) &&
                       (hole.y < vertices[i - resolution - 1].y - margin && hole.y > vertices[i - resolution - 1].y + margin) &&
                       (hole.z < vertices[i - resolution - 1].z - margin && hole.z > vertices[i - resolution - 1].z + margin))
                    {
                        allow = false;
                    }
                }

                if (allow) {
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
                bool allow = true;
                foreach (var peak in peaks)
                {
                    if ((peak.x < vertices[i - resolution - 1].x - margin && peak.x > vertices[i - resolution - 1].x + margin) &&
                       (peak.y < vertices[i - resolution - 1].y - margin && peak.y > vertices[i - resolution - 1].y + margin) &&
                       (peak.z < vertices[i - resolution - 1].z - margin && peak.z > vertices[i - resolution - 1].z + margin))
                    {
                        allow = false;
                    }
                }

                foreach (var hole in holes)
                {
                    if ((hole.x < vertices[i - resolution - 1].x - margin && hole.x > vertices[i - resolution - 1].x + margin) &&
                       (hole.y < vertices[i - resolution - 1].y - margin && hole.y > vertices[i - resolution - 1].y + margin) &&
                       (hole.z < vertices[i - resolution - 1].z - margin && hole.z > vertices[i - resolution - 1].z + margin))
                    {
                        allow = false;
                    }
                }

                if(allow) {
                    holes.Add(vertices[i - resolution - 1]);
                    allowHole = !allowHole;
                    allowPeak = !allowPeak;
                }
            }
        }
    }

    public void holePeakFilter() 
    {
        List<Vector3> filterPeaks = peaks.OrderByDescending(item => item.magnitude).ToList();
        List<Vector3> filterHoles = holes.OrderBy(item => item.magnitude).ToList();
        List<Vector3> topPeaks = new List<Vector3>();
        List<Vector3> topHoles = new List<Vector3>();

        float peakAverage = 0f;
        float holeAverage = 0f;

        peaks.Clear();
        holes.Clear();

        if (filterPeaks.Count > 0 && filterHoles.Count > 0)
        {

            for (int i = 0; i < filterPeaks.Count; i++)
            {
                peakAverage += filterPeaks[i].magnitude;
            }
            for (int i = 0; i < filterHoles.Count; i++)
            {
                holeAverage += filterHoles[i].magnitude;
            }

            peakAverage = peakAverage / filterPeaks.Count;
            holeAverage = holeAverage / filterHoles.Count;

            for (int y = 0; y < filterPeaks.Count; y++)
            {
                if(filterPeaks[y].magnitude > peakAverage) {
                    bool allow = true;
                    foreach (var peak in filterPeaks)
                    {
                        if ((peak.x < filterPeaks[y].x - margin && peak.x > filterPeaks[y].x + margin) &&
                           (peak.y < filterPeaks[y].y - margin && peak.y > filterPeaks[y].y + margin) &&
                           (peak.z < filterPeaks[y].z - margin && peak.z > filterPeaks[y].z + margin))
                        {
                            allow = false;
                        }
                    }

                    foreach (var hole in filterHoles)
                    {
                        if ((hole.x < filterPeaks[y].x - margin && hole.x > filterPeaks[y].x + margin) &&
                           (hole.y < filterPeaks[y].y - margin && hole.y > filterPeaks[y].y + margin) &&
                           (hole.z < filterPeaks[y].z - margin && hole.z > filterPeaks[y].z + margin))
                        {
                            allow = false;
                        }
                    }

                    if(allow) {
                        peaks.Add(filterPeaks[y]);
                    }
                }
            }

            for (int y = 0; y < filterHoles.Count; y++)
            {
                if (filterHoles[y].magnitude < holeAverage)
                {
                    bool allow = true;
                    foreach (var peak in filterPeaks)
                    {
                        if ((peak.x < filterHoles[y].x - margin && peak.x > filterHoles[y].x + margin) &&
                           (peak.y < filterHoles[y].y - margin && peak.y > filterHoles[y].y + margin) &&
                           (peak.z < filterHoles[y].z - margin && peak.z > filterHoles[y].z + margin))
                        {
                            allow = false;
                        }
                    }

                    foreach (var hole in filterHoles)
                    {
                        if ((hole.x < filterHoles[y].x - margin && hole.x > filterHoles[y].x + margin) &&
                           (hole.y < filterHoles[y].y - margin && hole.y > filterHoles[y].y + margin) &&
                           (hole.z < filterHoles[y].z - margin && hole.z > filterHoles[y].z + margin))
                        {
                            allow = false;
                        }
                    }

                    if (allow)
                    {
                        holes.Add(filterHoles[y]);
                    }
                }
            }
        }
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
