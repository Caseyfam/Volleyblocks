using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoRotate : MonoBehaviour {

    private bool rotate = false;
    public float waitAmount = 4f;
    public float randomRange = 7f;

    public GameObject logo;
    private Vector3 destination;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(WaitToRotate(waitAmount));
        NewRandomDestination();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (rotate)
        {
            logo.transform.rotation = Quaternion.RotateTowards(logo.transform.rotation, Quaternion.Euler(destination), 0.1f);
            if (logo.transform.rotation == Quaternion.Euler(destination))
            {
                NewRandomDestination();
            }
        }	
	}

    private void NewRandomDestination()
    {
        destination = new Vector3(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange));
    }

    IEnumerator WaitToRotate (float time)
    {
        yield return new WaitForSeconds(time);
        rotate = true;
    }
}
