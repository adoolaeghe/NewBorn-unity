using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLAgents
{

    public class CameraFollow : MonoBehaviour
    {

        public Transform target;
        Vector3 offset;

        // Use this for initialization
        void Start()
        {
            //target = GameObject.Find("Init(Clone)").transform;
            offset = gameObject.transform.position - target.position;
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.position = target.position + offset;
            Vector3 newPosition = new Vector3(target.position.x, target.position.y,
                target.position.z);
			Quaternion newRotation = Quaternion.Euler(target.rotation.x, target.rotation.y + 90,
				target.rotation.z);
            gameObject.transform.position = newPosition + offset;
            //gameObject.transform.rotation = newRotation;
        }
    }
}
