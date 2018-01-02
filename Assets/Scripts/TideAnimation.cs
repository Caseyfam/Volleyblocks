using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideAnimation : MonoBehaviour {

    public Vector3 maxPosition, minPosition;
    public float steps = 0.01f;
    private bool goingDown = true;
	
	// Update is called once per frame
	void Update ()
    {
	    if (goingDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, minPosition, steps);
            if (transform.position == minPosition)
            {
                goingDown = false;
            }
        }	
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, maxPosition, steps);
            if (transform.position == maxPosition)
            {
                goingDown = true;
            }
        }
	}
}
