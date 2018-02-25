using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTankBob : MonoBehaviour {

    public float direction = 0.1f;
    public float waitTime = 1f;

	// Use this for initialization
	void Awake ()
    {
        StartCoroutine(FlipDir(waitTime));
        waitTime *= 2;
	}

    IEnumerator FlipDir(float time)
    {
        yield return new WaitForSeconds(time);
        direction = -direction;
        StartCoroutine(FlipDir(waitTime));
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + direction, transform.position.z);
    }
}
