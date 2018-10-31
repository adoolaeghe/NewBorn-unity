using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MLAgents;

public class Gene : MonoBehaviour
{
    private List<List<GameObject>> Germs;
    private List<GameObject> Cells;
    private List<Vector3> CellPositions;

    AgentTrainBehaviour aTBehaviour;
    
    public void initGerms(int numGerms, float threshold)
    {
        PostGene postGene = transform.gameObject.AddComponent<PostGene>() as PostGene;

        // INIT GENE LIST //
        ////////////////////
        Germs = new List<List<GameObject>>();
        Cells = new List<GameObject>();
        CellPositions = new List<Vector3>();
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
        ////////////////////////////////INIT BASE GERMS///////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////
        /// 1ST CELL ///
        // init objedt shape
        Germs.Add(new List<GameObject>()); 
        Germs[0].Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        // init position according to parent
        Germs[0][0].transform.parent = gameObject.transform;
        Germs[0][0].transform.localPosition = new Vector3(0f, 0f, 0f);
        // init rigid body and mass
        Germs[0][0].AddComponent<Rigidbody>();
        Germs[0][0].GetComponent<Rigidbody>().mass = 1f;
        // store cell gameobject and position
        Cells.Add(Germs[0][0]);
        CellPositions.Add(Germs[0][0].transform.localPosition);
        //////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////
        /////////////////// Iterate for each new part of the morphology //////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////
        Debug.Log(numGerms);
        for (int y = 1; y < numGerms; y++)
        {
            Debug.Log(y);
            int prevCount = Germs[y - 1].Count;
            Germs.Add(new List<GameObject>());

            //////////////////////////////////////////////////
            /// ITERATE FOR EACH PREVIOUS GERM CELL NUMBER ///
            for (int i = 0; i < prevCount; i++)
            {
                //////////////////////////////////////////////////
                ////////// ITERATE FOR EACH CELL SIDES ///////////
                for (int z = 0; z < sides.Count; z++)
                {

                    //if(sides[z] != -Germs[y - 1][i].transform.localPosition) {
                        bool isValid = true;
                        Vector3 cellPosition = Germs[y - 1][i].transform.position + sides[z];
                        Debug.Log(cellPosition);

                        foreach (var position in CellPositions)
                        {
                            if(cellPosition == position) {
                                isValid = !isValid;
                            }
                        }
                      
                        if(isValid) {
                            if(Random.Range(0f, 1f) > threshold) {
                                // init default shape
                                Germs[y].Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                                GameObject cell = Germs[y][Germs[y].Count - 1];
                                // init position according to parent
                                cell.transform.parent = Germs[y - 1][i].transform;
                                cell.transform.localPosition = sides[z];
                                // init rigidbody with mass
                                cell.AddComponent<Rigidbody>();
                                cell.GetComponent<Rigidbody>().mass = 1f;
                                // init joint
                                initJoint(cell, Germs[y - 1][i], sides[z]);
                                // store cell
                                Cells.Add(cell);
                                CellPositions.Add(cellPosition);
                            }
                        }
                     //}
                 }
            }

            foreach(var cell in Cells) {
                cell.transform.parent = transform;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////

        AddAgentPart(Cells);
        ////Post data to Api
        //StartCoroutine(postGene.requestAgent(this));
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
        cj.angularYLimit = new SoftJointLimit() {limit = 40f, bounciness = 0f };
        cj.highAngularXLimit = new SoftJointLimit() { limit = 90f, bounciness = 0f };
        cj.lowAngularXLimit = new SoftJointLimit() { limit = 0f, bounciness = 0f };
        part.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private void AddAgentPart(List<GameObject> Cells)
	{
        aTBehaviour = transform.gameObject.GetComponent<AgentTrainBehaviour>();
        aTBehaviour.initPart = Cells[0].transform;
        for (int i = 1; i < Cells.Count; i++)
        {
            aTBehaviour.parts.Add(Cells[i].transform);
        }
    }
}
