using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour {

    public GameObject ball;

	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, ball.transform.position.x / 3f, 0f), 0.01f);
	}

    public void Reset()
    {
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
}
