﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MLAgents;

public class Gene : MonoBehaviour
{
    public List<List<Mutation>> mutations;
    private List<List<GameObject>> parts;
    private List<List<ProcShape>> shapes;
    private List<List<PartCoOrd>> partCoOrds;
    private List<List<int>> axis;

    private List<List<float>> energy;

    AgentTrainBehaviour aTBehaviour;

    INoiseFilter[] noiseFilters;

    public void initParts(int numParts, List<Transform> agentParts)
    {
        PostGene postGene = transform.gameObject.AddComponent<PostGene>() as PostGene;

        // INIT GENE LIST //
        ////////////////////
        axis = new List<List<int>>();
        energy = new List<List<float>>();
        mutations = new List<List<Mutation>>();
        parts = new List<List<GameObject>>();
        partCoOrds = new List<List<PartCoOrd>>();
        shapes = new List<List<ProcShape>>();
        ////////////////////



        //////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////INIT BASE PARTS///////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        /// 1ST PART ///
        axis.Add(new List<int>());
        energy.Add(new List<float>());
        parts.Add(new List<GameObject>());
        shapes.Add(new List<ProcShape>());
        partCoOrds.Add(new List<PartCoOrd>());
        mutations.Add(new List<Mutation>());
        energy[0].Add(1);
        axis[0].Add(6);
        mutations[0].Add(new Mutation("base", 0, axis[0][0], energy[0][0], mutations[0]));
        basePart(parts[0], mutations[0][0], shapes[0], partCoOrds[0], axis[0][0]);

        /// 2ND PART ///
        //axis.Add(new List<int>());
        //energy.Add(new List<float>());
        //parts.Add(new List<GameObject>());
        //shapes.Add(new List<ProcShape>());
        //partCoOrds.Add(new List<PartCoOrd>());
        //mutations.Add(new List<Mutation>());
        //energy[1].Add(1);
        //axis[1].Add(5);
        //mutations[1].Add(new Mutation("follow", 1, axis[1][0], energy[1][0], mutations[0])); 
        //basePart(parts[1], mutations[1][0], shapes[1], partCoOrds[1], axis[1][0]);
        //initRotation(parts[1][0], partCoOrds[0][0].positionMin[0], partCoOrds[1][0].positionMax[0], axis[1][0]);
        //initJoint(parts[1][0], parts[0][0], partCoOrds[1][0].verticeZMax, partCoOrds[1][0], mutations[1][0].angularYLimit, mutations[1][0].highAngularXLimit, mutations[1][0].lowAngularXLimit);

      
        //////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////
        /////////////////// Iterate for each new part of the morphology //////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////
        //for (int i = 1; i < numParts; i++)
        //{
        //    for (int y = 0; y < parts.Count; y++)
        //    {
        //        energy[y].Add(1);
        //        float divisionChance = Random.Range(0f, 1f);
        //        if (divisionChance > energy[y][i] / 2)
        //        {
         
        //            mutations[y].Add(new Mutation("division", i, 3, 0.5f, mutations[y]));
        //            newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], 3, i, false);
        //            newDuplicatePart(energy, mutations, shapes, partCoOrds, 4, i, y);
       
        //        } else {
        //            mutations[y].Add(new Mutation("follow", i, axis[y][i - 1], energy[y][i], mutations[y]));
        //            axis[y].Add(axis[y][i - 1]);
        //            newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], axis[y][i], i, false);
        //        }
        //    }
        //}
        ///////////////////////////////////////////////////////////////////////////////////////

        //AddAgentPart(parts, agentParts, numParts);
        ////Post data to Api
        //StartCoroutine(postGene.requestAgent(this));
	}

	private void basePart(List<GameObject> parts, Mutation mutation, List<ProcShape> shapes, List<PartCoOrd> partCoOrds, int axis)
	{
		/////////////////////////////// INIT FIRST BODY PART///////////////////////////////////
		parts.Add(new GameObject());
        // Add Collider in the same layer group.
        parts[0].layer = 8;
        parts[0].transform.parent = transform;

        ////////////////////////////////// NEW PROC SHAPE //////////////////////////////////////
        shapes.Add(initProcShape(parts[0], mutation.resolution, mutation.radius, mutation.noiseLayersParams));

		/////////////////////////////// GET PROC SHAPE COORD ///////////////////////////////////
		partCoOrds.Add(new PartCoOrd(parts[0], shapes[0], new Vector3(0f, 0f, 0f), axis));

		///////////////////////////////SET LOCAL ROTATION TO 0 //////////////////////////////////
		parts[0].transform.localRotation = Quaternion.identity;

		///////////////////////////// INIT JOINT AND COLLIDER //////////////////////////////////
		// InitJointObject(parts[0], partCoOrds[0].verticeAxisMax);
        Rigidbody rigidBody = parts[0].gameObject.AddComponent<Rigidbody>();
		// New collider 
		initCollider(parts[0]);

	}

	private void newPart(List<GameObject> parts, Mutation mutation, List<ProcShape> shapes, List<PartCoOrd> partCoOrds, int axis, int i, bool divided)
	{
        ////////////////////////////////// NEW GAMEOBJECT //////////////////////////////////////
        parts.Add(new GameObject());
        if(divided){
            parts[i].transform.parent = parts[i - 2].transform;
        } else {
            parts[i].transform.parent = parts[i - 1].transform;
        }
        // Add Collider in the same layer group.
        parts[i].layer = 8;

        ////////////////////////////////// NEW PROC SHAPE //////////////////////////////////////
        shapes.Add(initProcShape(parts[i], mutation.resolution, mutation.radius, mutation.noiseLayersParams));

        /////////////////////////////// GET PROC SHAPE COORD ///////////////////////////////////
        partCoOrds.Add(new PartCoOrd(parts[i], shapes[i], new Vector3(0f, 0f, 0f), axis));

        //////////////////////////// NEW ROTATION WITH PROC COORD/s//////////////////////////////
        //if (divided)
        //{
        //    initRotation(parts[i], partCoOrds[i - 2], partCoOrds[i], axis);
        //}
        //else
        //{
        initRotation(parts[i], partCoOrds[i - 1].positionMin[1], partCoOrds[i].positionMax[1], axis);
        //}

        //

		// New collider 
		initCollider(parts[i]);
	}

    private ProcShape initProcShape(GameObject part, int resolution, Vector3 radius, List<NoiseLayerParams>  noiseLayersParams)
    {
		ProcShape shape = part.AddComponent<ProcShape>() as ProcShape;
        shape.Initialize();
        shape.resolution = resolution;
        shape.shapeSettings.planetRadius = radius;
        for (int i = 0; i < shape.shapeSettings.noiseLayers.Length; i++)
        {
			shape.shapeSettings.noiseLayers[i].noiseSettings.strength = noiseLayersParams[i].strength;
			shape.shapeSettings.noiseLayers[i].noiseSettings.numLayers = noiseLayersParams[i].numLayers;
			shape.shapeSettings.noiseLayers[i].noiseSettings.baseRoughness = noiseLayersParams[i].baseRoughness;
			shape.shapeSettings.noiseLayers[i].noiseSettings.roughness = noiseLayersParams[i].roughness;
			shape.shapeSettings.noiseLayers[i].noiseSettings.persistence = noiseLayersParams[i].persistence;
            shape.shapeSettings.noiseLayers[i].noiseSettings.center = noiseLayersParams[i].centre;
            shape.shapeSettings.noiseLayers[i].noiseSettings.filterType = NoiseSettings.FilterType.Ridgid;
            shape.shapeSettings.noiseLayers[i].useFirstLayerAsMask = noiseLayersParams[i].useFirstLayerAsMask;
        }
        shape.UpdateSettings(shape.shapeSettings);
        shape.GenerateMesh();
        return shape;
    }

    private void initRotation(GameObject part, Vector3 min, Vector3 max, int axis)
    {
        part.transform.localPosition = min - max;   
	}

    private void initJoint(GameObject part, GameObject connectedBody, Vector3 jointAnchor, PartCoOrd partCoOrd, float angularYLimit, float highAngularXLimit, float lowAngularXlimit)
	{
        ConfigurableJoint cj = part.transform.gameObject.AddComponent<ConfigurableJoint>();
        // Configurable Joint Motion 
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;
		// Configurable Joint Angular Mortion
		cj.angularXMotion = ConfigurableJointMotion.Limited;
        cj.angularYMotion = ConfigurableJointMotion.Limited;
        cj.angularZMotion = ConfigurableJointMotion.Locked;
		// Configurable Joint Connected Body AND Anchor settings
		cj.connectedBody = connectedBody.gameObject.GetComponent<Rigidbody>();
        cj.rotationDriveMode = RotationDriveMode.Slerp;
        cj.anchor = jointAnchor;
        cj.axis = partCoOrd.jointAxis;
		// Configurable Joint Angular Limit
        // Important to have 0 of bounciness
        cj.angularYLimit = new SoftJointLimit() { limit = angularYLimit, bounciness = 0f };
        cj.highAngularXLimit = new SoftJointLimit() { limit = highAngularXLimit, bounciness = 0f };
        cj.lowAngularXLimit = new SoftJointLimit() { limit = lowAngularXlimit, bounciness = 0f };
        part.gameObject.GetComponent<Rigidbody>().useGravity = true;
	}

	private void initCollider(GameObject part)
	{
        AgentTrainBehaviour AgentTM = GetComponent<AgentTrainBehaviour>();
		part.AddComponent<GroundContact>().agent = AgentTM;
		part.AddComponent<TargetContact>().agent = AgentTM;
        foreach (Transform subPart in part.transform)
        {
            MeshCollider meshCollider = part.transform.gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = subPart.gameObject.GetComponent<MeshFilter>().mesh;
            meshCollider.convex = true;
            meshCollider.inflateMesh = true;
            // add collider to the same layer so that they don't collide 
            subPart.gameObject.layer = 8;
        }
	}

	private void AddAgentPart(List<GameObject> parts, List<Transform> agentParts, int partNb)
	{
        aTBehaviour = transform.gameObject.GetComponent<AgentTrainBehaviour>();
        aTBehaviour.initPart = parts[0].transform;
        for (int i = 0; i < partNb - 1; i++)
        {
            aTBehaviour.parts.Add(parts[1].transform);
        }
    }

    private void newDuplicatePart(List<List<float>> energy, List<List<Mutation>> mutations, List<List<ProcShape>> shapes, List<List<PartCoOrd>> partCoOrds, int axis, int i, int y)
    {
        int z = i + 1;
        energy[y].Add(0.5f);
        mutations[y].Add(new Mutation("follow", z, axis, energy[y][z], mutations[y]));
        newPart(parts[y], mutations[y][z], shapes[y], partCoOrds[y], axis, z, true);

        // ADD PARALLELISM ACCORDING TO THE AXIS
        if(axis == 1 || axis == 2) {
            parts[y][i].transform.localScale += new Vector3(-2f, 0f, 0f);
        } else if (axis == 3 || axis == 4) {
            parts[y][i].transform.localScale += new Vector3(0f, -2f, 0f);
        } else {
            parts[y][i].transform.localScale += new Vector3(0f, 0f, -2f);
        }
    }
}
