using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveOscillate : MonoBehaviour {

    Camera cam;
    float fieldOfView = 60;
    public float addendum = 5f;
    public float maxDistanceDelta = 0.01f;
    bool upwards = true;

	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (!cam.orthographic)
        {
            if (upwards)
            {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fieldOfView + addendum, maxDistanceDelta);
                if (cam.fieldOfView == fieldOfView + addendum)
                {
                    upwards = false;
                }
            }
            else
            {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fieldOfView - addendum, maxDistanceDelta);
                if (cam.fieldOfView == fieldOfView - addendum)
                {
                    upwards = true;
                }
            }
        }	
	}
}
