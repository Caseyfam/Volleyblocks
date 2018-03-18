using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFloat : MonoBehaviour {

    private Vector3 target;
    private Vector3 resetPosition;
    public float pingPongLength = 1f;
    public float pingPongHeight = 0.01f;
    public float xTravel = 0.1f;
    public float maxDistanceSteps = 1f;

	// Use this for initialization
	void Start ()
    {
        if (Random.Range(0,2) == 1)
        {
            pingPongLength = -pingPongLength;
        }
        pingPongHeight = Random.Range(0, pingPongHeight);
        resetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        target = transform.position + new Vector3(xTravel, Mathf.Sin(Time.time * pingPongLength) * pingPongHeight, 0f);
        transform.position = Vector3.MoveTowards(transform.position, target, 1f);

        if (transform.position.x >= 12f)
        {
            transform.position = new Vector3(-12f, transform.position.y, transform.position.z);
        }
	}

}
