using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MLAgents;

public class Gene : MonoBehaviour
{
    public List<List<Mutation>> mutations;
    private List<List<GameObject>> parts;
    private List<GameObject> allParts;
    private List<Vector3> partsFull;

    AgentTrainBehaviour aTBehaviour;

    INoiseFilter[] noiseFilters;

    public void initParts(int numParts, List<Transform> agentParts)
    {
        PostGene postGene = transform.gameObject.AddComponent<PostGene>() as PostGene;

        // INIT GENE LIST //
        ////////////////////
        mutations = new List<List<Mutation>>();
        parts = new List<List<GameObject>>();
        allParts = new List<GameObject>();
        partsFull = new List<Vector3>();
        ////////////////////

        List<Vector3> sides = new List<Vector3>{
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(0f, -1f, 0f),
            new Vector3(0f, 0f, -1f)
        };

        //////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////INIT BASE PARTS///////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        /// 1ST PART ///
        parts.Add(new List<GameObject>());
        parts[0].Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        allParts.Add(parts[0][0]);
        parts[0][0].transform.parent = gameObject.transform;
        parts[0][0].transform.localPosition = new Vector3(0f, 0f, 0f);
        partsFull.Add(parts[0][0].transform.position);
        parts[0][0].AddComponent<Rigidbody>();
        parts[0][0].GetComponent<Rigidbody>().mass = 100f;
        //////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////
        /////////////////// Iterate for each new part of the morphology //////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////
        for (int y = 1; y < 55; y++)
        {
            parts.Add(new List<GameObject>());
            for (int i = 0; i < parts[y-1].Count; i++)
            {
                for (int z = 0; z < 6; z++)
                {
                    if(sides[z] != -parts[y - 1][i].transform.localPosition) {
                        bool trigger = true;
                        foreach (var item in partsFull)
                        {
                            if(parts[y - 1][i].transform.position + sides[z] == item) {
                                trigger = false;
                            }
                        }

                        if(trigger) {
                            if(Random.Range(0f, 1f) > 0.75f) {
                                partsFull.Add(parts[y - 1][i].transform.position + sides[z]);
                                parts[y].Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                                parts[y][parts[y].Count - 1].transform.parent = parts[y - 1][i].transform;
                                parts[y][parts[y].Count - 1].transform.localPosition = sides[z];
                                allParts.Add(parts[y][parts[y].Count - 1]);
                                //parts[y][parts[y].Count - 1].AddComponent<Rigidbody>();
                                //parts[y][parts[y].Count - 1].GetComponent<Rigidbody>().mass = 100f;

                                //parts[y][parts[y].Count - 1].GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                                initJoint(parts[y][parts[y].Count - 1], parts[y - 1][i], sides[z]);
                            }
                        }
                     }
                 }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////

        AddAgentPart(allParts);
        ////Post data to Api
        //StartCoroutine(postGene.requestAgent(this));
    }


    private void initRotation(GameObject part, Vector3 max, Vector3 min)
    {
        //part.transform.localPosition = min - max;
        //part.transform.LookAt(parts[0][0].transform);
        //part.transform.localPosition = min  max;
    }

    private void initJoint(GameObject part, GameObject connectedBody, Vector3 jointAnchor)
    {
        ConfigurableJoint cj = part.transform.gameObject.AddComponent<ConfigurableJoint>();
        ///////////////////////
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
        //cj.anchor = jointAnchor;
        //cj.axis = partCoOrd.jointAxis;
        // Configurable Joint Angular Limit
        // Important to have 0 of bounciness
        cj.angularYLimit = new SoftJointLimit() {bounciness = 0f };
        cj.highAngularXLimit = new SoftJointLimit() {bounciness = 0f };
        cj.lowAngularXLimit = new SoftJointLimit() { bounciness = 0f };
        part.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private void AddAgentPart(List<GameObject> agentParts)
	{
        GameObject brain = GameObject.Find("/Academy/CrawlerBrain1");
        brain.GetComponent<Brain>().brainParameters.vectorActionSize = agentParts.Count * 14 + 3;
        brain.GetComponent<Brain>().brainParameters.vectorObservationSize = agentParts.Count * 14 + 3;
        aTBehaviour = transform.gameObject.GetComponent<AgentTrainBehaviour>();
        aTBehaviour.initPart = agentParts[0].transform;
        for (int i = 1; i < agentParts.Count; i++)
        {
            aTBehaviour.parts.Add(agentParts[i].transform);
        }
    }
}
