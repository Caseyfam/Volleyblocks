using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEndless : MonoBehaviour {

    public float speed;
    public string direction;
	
	// Update is called once per frame
	void Update ()
    {
        switch (direction)
        {
            default:
            case "up":
                transform.Rotate(Vector3.up, speed * Time.deltaTime);
                break;
            case "down":
                transform.Rotate(Vector3.down, speed * Time.deltaTime);
                break;
            case "left":
                transform.Rotate(Vector3.left, speed * Time.deltaTime);
                break;
            case "right":
                transform.Rotate(Vector3.right, speed * Time.deltaTime);
                break;
        }
	}
}
