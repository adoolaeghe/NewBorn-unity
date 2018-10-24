using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PartCoOrd
{
    public Vector3 positionMax;
    public Vector3 positionMin;
    public Vector3 elevationMin;
    public Vector3 elevationMax;
    public Vector3 heading;
    public Vector3 direction;
    public Vector3 jointAxis;

    public Vector3 verticeAxisMax;
    public Vector3 verticeXMax;
    public Vector3 verticeXMaxB;
    public Vector3 verticeYMax;
    public Vector3 verticeYMaxB;
    public Vector3 verticeZMax;
    public Vector3 verticeZMaxB;

    private float elevationXMax;
    private float elevationXMaxB;
    private float elevationYMax;
    private float elevationYMaxB;
    private float elevationZMax;
    private float elevationZMaxB;
    private float distance;

    public PartCoOrd(GameObject part, ProcShape shape, Vector3 position, int axis)
    {
        part.transform.localPosition = position;
        positionMax = shape.shapeGenerator.positionMax;
        positionMin = shape.shapeGenerator.positionMin;
        elevationMin = shape.shapeGenerator.elevationMinMax.PosMin;
        elevationMax = shape.shapeGenerator.elevationMinMax.PosMax;
        positionMin = shape.shapeGenerator.positionMin;
        heading = (positionMax - positionMin);
        distance = heading.magnitude;
        direction = heading / distance;

        elevationXMax = 0f;
        elevationXMaxB = 0f;
        elevationYMax = 0f;
        elevationYMaxB = 0f;
        elevationZMax = 0f;
        elevationZMaxB = 0f;

        verticeXMax = new Vector3(0f, 0f, 0f);
        verticeXMaxB = new Vector3(0f, 0f, 0f);
        verticeYMax = new Vector3(0f, 0f, 0f);
        verticeYMaxB = new Vector3(0f, 0f, 0f);
        verticeZMax = new Vector3(0f, 0f, 0f);
        verticeZMaxB = new Vector3(0f, 0f, 0f);
        verticeAxisMax = new Vector3(0f, 0f, 0f);
        jointAxis = new Vector3(0f, 0f, 0f);

        foreach (Transform subPart in part.transform)
        {
            foreach (var vertice in subPart.GetComponent<MeshFilter>().mesh.vertices)
            {
                if (elevationXMax > vertice.x)
                {
                    elevationXMax = vertice.x;
                    verticeXMax = vertice;
                }
                if (elevationXMaxB < vertice.x)
                {
                    elevationXMaxB = vertice.x;
                    verticeXMaxB = vertice;
                }
                if (elevationYMax < vertice.y)
                {
                    elevationYMax = vertice.y;
                    verticeYMax = vertice;
                }
                if (elevationYMaxB > vertice.y)
                {
                    elevationYMaxB = vertice.y;
                    verticeYMaxB = vertice;
                }
                if (elevationZMax > vertice.z)
                {
                    elevationZMax = vertice.z;
                    verticeZMax = vertice;
                }
                if (elevationZMaxB < vertice.z)
                {
                    elevationZMaxB = vertice.z;
                    verticeZMaxB = vertice;
                }
            }
        }

        if (axis == 1)
        {
            verticeAxisMax = verticeXMax;
            jointAxis = new Vector3(0f, 0f, -1f);
        }
        else if (axis == 2)
        {
            verticeAxisMax = verticeXMaxB;
            jointAxis = new Vector3(0f, 0f, 1f);
        }
        else if (axis == 3)
        {
            verticeAxisMax = verticeYMax;
            jointAxis = new Vector3(0f, 0f, -1f);
        }
        else if (axis == 4)
        {
            verticeAxisMax = verticeYMaxB;
            jointAxis = new Vector3(1f, 0f, 0f);
        }
        else if (axis == 5)
        {
            verticeAxisMax = verticeZMax;
            jointAxis = new Vector3(1f, 0f, 0f);
        }
        else if (axis == 6)
        {
            verticeAxisMax = verticeZMaxB;
            jointAxis = new Vector3(-1f, 0f, 0f);
        }
    }
}
