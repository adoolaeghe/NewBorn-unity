    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CrawlerMorphologyAgent : Agent
{
 //   [Header("Target To Walk Towards")]
 //   [Space(10)]
 //   public Transform target;
 //   public Transform crAgentbody;
 //   public Transform ground;
 //   public Transform parent;
 //   public bool detectTargets;
 //   public bool respawnTargetWhenTouched;
 //   public float targetSpawnRadius;

 //   public bool hasFallen;


 //   [Header("Joint Settings")] [Space(10)] JointDriveController jdController;
 //   Vector3 dirToTarget;
	//float facingDot;
	//public Vector3 ogPosition;
 //   float movingTowardsDot;

 //   public float[] actionList;

 //   CrawlerAgent crAgent;

 //   public override void InitializeAgent()
 //   {
 //       actionList = new float[]{ -1f, -0.5f, 0.5f, 1f };
 //       jdController = GetComponent<JointDriveController>();
 //       crAgent = GetComponent<CrawlerAgent>();
 //       crAgent.setUpAgentLegs();
 //       hasFallen = false;
 //   }

 //   public override void CollectObservations()
 //   {
 //       // Forward & up to help with orientation
 //       AddVectorObs(hasFallen ? 1 : 0);
 //   }


 //   public override void AgentAction(float[] vectorAction, string textAction)
 //   {
	//	if (Vector3.Dot(crAgent.currentBody.up, Vector3.down) > 0)
	//	{
	//		AddReward(-1f);
 //           Done();
	//	}

 //       AddReward(0.1f);

	//	foreach (var leg in crAgent.legList)
	//	{
	//		Destroy(leg.parent.gameObject);
	//	}

	//	if (crAgent.currentBody != null)
	//	{
	//		Destroy(crAgent.currentBody.gameObject);
	//	}
	//	dirToTarget = target.position - crAgent.currentBody.position;
	//	int i = 0;

	//	// NEW MORPHOLOGY VECTOR
	//	actionList = vectorAction;
 //       hasFallen = false;
	//	crAgent.setUpAgentLegs();
	//}

}
