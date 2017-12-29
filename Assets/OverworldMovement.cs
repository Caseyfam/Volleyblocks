using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMovement : MonoBehaviour {

    public float speed = 10f;
    public bool canMove = true;

    private void Start()
    {
        GameObject.Find("PassedObject").GetComponent<PassedOverworld>().ReloadTransforms();
    }

    // Update is called once per frame
    void Update ()
    {
        if (canMove)
        {
            Vector3 playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * speed;
            Vector3.ClampMagnitude(playerMovement, speed);

            GetComponent<Rigidbody>().position += playerMovement * Time.deltaTime;
        }
	}
}
