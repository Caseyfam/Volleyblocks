using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogRoll : MonoBehaviour {

    public Vector3 leftExit, rightEnter;
    public float rollStep;

	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, leftExit, rollStep);
        if (transform.position == leftExit)
        {
            transform.position = rightEnter;
        }	
	}
}
