using UnityEngine;
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
        axis.Add(new List<int>());
        energy.Add(new List<float>());
        parts.Add(new List<GameObject>());
        shapes.Add(new List<ProcShape>());
        partCoOrds.Add(new List<PartCoOrd>());
        mutations.Add(new List<Mutation>());
        energy[1].Add(1);
        axis[1].Add(5);
        mutations[1].Add(new Mutation("follow", 1, axis[1][0], energy[1][0], mutations[0])); 
        basePart(parts[1], mutations[1][0], shapes[1], partCoOrds[1], axis[1][0]);
        initRotation(parts[1][0], partCoOrds[0][0], partCoOrds[1][0], axis[1][0]);
        initJoint(parts[1][0], parts[0][0], partCoOrds[1][0].verticeZMax, partCoOrds[1][0], mutations[1][0].angularYLimit, mutations[1][0].highAngularXLimit, mutations[1][0].lowAngularXLimit);

      
        //////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////
        /////////////////// Iterate for each new part of the morphology //////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////
        for (int i = 1; i < numParts; i++)
        {
            for (int y = 0; y < parts.Count; y++)
            {
                energy[y].Add(1);
                float divisionChance = Random.Range(0f, 1f);
                if (divisionChance > energy[y][i] / 2)
                {
                    axis[y].Add(axis[y][i - 1]);
                    switch (axis[y][i - 1])
                    {
                        case 1:
                        case 2:
                            if (divisionChance > (energy[y][i] / 1.5))
                            {
                                // AXIS 3 AND 4
                                mutations[y].Add(new Mutation("division", i, 3, 0.5f, mutations[y]));
                                newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], 3, i, false);
                                newDuplicatePart(energy, mutations, shapes, partCoOrds, 4, i, y);
                            } else {
                                // AXIS 5 AND 6
                                mutations[y].Add(new Mutation("division", i, 5, 0.5f, mutations[y]));
                                newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], 5, i, false);
                                newDuplicatePart(energy, mutations, shapes, partCoOrds, 6, i, y);
                            }
                            break;
                        case 3:
                        case 4:
                            if (divisionChance > (energy[y][i] / 1.5))
                            {
                                // AXIS 5 AND 6
                                mutations[y].Add(new Mutation("division", i, 5, 0.5f, mutations[y]));
                                newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], 5, i, false);
                                newDuplicatePart(energy, mutations, shapes, partCoOrds, 6, i, y);
                            } else {
                                // AXIS 1 AND 2
                                mutations[y].Add(new Mutation("division", i, 1, 0.5f, mutations[y]));
                                newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], 1, i, false);
                                newDuplicatePart(energy, mutations, shapes, partCoOrds, 2, i, y);
                            }
                            break;
                        case 5:
                        case 6:
                            if (divisionChance > energy[y][i] / 1.5)
                            {
                                // AXIS 1 AND 2
                                mutations[y].Add(new Mutation("division", i, 1, 0.5f, mutations[y]));
                                newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], 1, i, false);
                                newDuplicatePart(energy, mutations, shapes, partCoOrds, 2, i, y);
                            } else {
                                // AXIS 3 AND 4
                                mutations[y].Add(new Mutation("division", i, 3, 0.5f, mutations[y]));
                                newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], 3, i, false);
                                newDuplicatePart(energy, mutations, shapes, partCoOrds, 4, i, y);
                            }
                            break;
                    }
                } else {
                    mutations[y].Add(new Mutation("follow", i, axis[y][i - 1], energy[y][i], mutations[y]));
                    axis[y].Add(axis[y][i - 1]);
                    newPart(parts[y], mutations[y][i], shapes[y], partCoOrds[y], axis[y][i], i, false);
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////

        //AddAgentPart(parts, agentParts, numParts);
        ////Post data to Api
        //StartCoroutine(postGene.requestAgent(this));
	}

	private void basePart(List<GameObject> parts, Mutation mutation, List<ProcShape> shapes, List<PartCoOrd> partCoOrds, int axis)
	{
		/////////////////////////////// INIT FIRST BODY PART///////////////////////////////////
		parts.Add(new GameObject());
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
		// Add Collider in the same layer group.
		parts[0].layer = 8;
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

        ////////////////////////////////// NEW PROC SHAPE //////////////////////////////////////
        shapes.Add(initProcShape(parts[i], mutation.resolution, mutation.radius, mutation.noiseLayersParams));

        /////////////////////////////// GET PROC SHAPE COORD ///////////////////////////////////
        partCoOrds.Add(new PartCoOrd(parts[i], shapes[i], new Vector3(0f, 0f, 0f), axis));

        //////////////////////////// NEW ROTATION WITH PROC COORD///////////////////////////////
        if (divided)
        {
            initRotation(parts[i], partCoOrds[i - 2], partCoOrds[i], axis);
        }
        else
        {
            initRotation(parts[i], partCoOrds[i - 1], partCoOrds[i], axis);
        }

        // Init Configurable Joint
        //////////////////
        //// TEMPORARY ///
        if (axis == 1)
        {
            initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeXMaxB, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
        }
        else if (axis == 2)
        {
            if(divided){
                initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeXMaxB, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
            } else {
                initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeXMax, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
            }
        }
        else if (axis == 3)
        {
            initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeYMaxB, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
        }
        else if (axis == 4)
        {
            if(divided) {
                initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeYMaxB, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
            } else {
                initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeYMax, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
            }
        }
        else if (axis == 5)
        {
            initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeZMax, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
        }
        else if (axis == 6)
        {
            if(divided){
                initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeZMaxB, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
            } else {
                initJoint(parts[i], parts[i - 1], partCoOrds[i].verticeZMaxB, partCoOrds[i], mutation.angularYLimit, mutation.highAngularXLimit, mutation.lowAngularXLimit);
            }
        }

		// New collider 
		initCollider(parts[i]);
        // Add Collider in the same layer group.
        parts[i].layer = 8;
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

        }
		shape.GenerateMesh();
        return shape;
    }

    private void initRotation(GameObject part, PartCoOrd coOrdA, PartCoOrd coOrdB, int axis)
    {
        // adjust rotation according to a axis
        if(axis == 1) {
            part.transform.localPosition = new Vector3(part.transform.localPosition.x + ((coOrdA.verticeXMaxB.x - coOrdB.verticeXMax.x)/2), part.transform.localPosition.y, part.transform.localPosition.z);    
        } else if (axis == 2) {
            part.transform.localPosition = new Vector3(part.transform.localPosition.x + ((coOrdB.verticeXMax.x - coOrdA.verticeXMaxB.x) / 2), part.transform.localPosition.y, part.transform.localPosition.z); 
        } else if (axis == 3) {
            part.transform.localPosition = new Vector3(part.transform.localPosition.x, part.transform.localPosition.y + ((coOrdA.verticeYMaxB.y - coOrdB.verticeYMax.y) / 2), part.transform.localPosition.z);
		} else if (axis == 4) {
            part.transform.localPosition = new Vector3(part.transform.localPosition.x, part.transform.localPosition.y + ((coOrdB.verticeYMax.y - coOrdA.verticeYMaxB.y) / 2), part.transform.localPosition.z);
		} else if (axis == 5) {
            part.transform.localPosition = new Vector3(part.transform.localPosition.x, part.transform.localPosition.y, part.transform.localPosition.z + ((coOrdA.verticeZMaxB.z - coOrdB.verticeZMax.z) / 2));
		} else if (axis == 6) {
            part.transform.localPosition = new Vector3(part.transform.localPosition.x, part.transform.localPosition.y, part.transform.localPosition.z + ((coOrdB.verticeZMax.z - coOrdA.verticeZMaxB.z) / 2));
        }
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
        cj.angularZMotion = ConfigurableJointMotion.Limited;
		// Configurable Joint Connected Body AND Anchor settings
		cj.connectedBody = connectedBody.gameObject.GetComponent<Rigidbody>();
        cj.rotationDriveMode = RotationDriveMode.Slerp;
        cj.anchor = jointAnchor;
        cj.axis = partCoOrd.jointAxis;
		// Configurable Joint Angular Limit
        cj.angularYLimit = new SoftJointLimit() { limit = angularYLimit, bounciness = 0f };
        cj.highAngularXLimit = new SoftJointLimit() { limit = highAngularXLimit, bounciness = 1f };
        cj.lowAngularXLimit = new SoftJointLimit() { limit = lowAngularXlimit, bounciness = 1f };
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

    public struct PartCoOrd
	{
		public Vector3 positionMax;
        public Vector3 positionMin;
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
}
