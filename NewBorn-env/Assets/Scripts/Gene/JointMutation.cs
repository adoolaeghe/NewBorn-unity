using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointMutation
{

	public float angularYLimit;
	public float highAngularXLimit;
	public float lowAngularXLimit;

	public JointMutation()
	{
        angularYLimit = 0f;
        highAngularXLimit = 0f;
		lowAngularXLimit = 0f;
	}
}
