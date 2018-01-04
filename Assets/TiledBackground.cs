using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour {

    private Vector3 originalPos;
    public float timeToWait = 1f;
    public float speed = 1f;

    void Awake()
    {
        originalPos = transform.position;
        StartCoroutine(WaitToReset(timeToWait));
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position += new Vector3(speed, speed);
	}

    IEnumerator WaitToReset(float time)
    {
        yield return new WaitForSeconds(time);
        transform.position = originalPos;
        StartCoroutine(WaitToReset(timeToWait));
    }
}
