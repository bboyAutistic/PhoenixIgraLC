using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LisenerRetro : MonoBehaviour {
    public Transform target;
	
	void Update () {
        if (target == null)
        {
            target=GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = target.position;
	}
}
