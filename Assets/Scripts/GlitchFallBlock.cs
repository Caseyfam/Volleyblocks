using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchFallBlock : MonoBehaviour {

    private Vector3 originalPosition;

    void OnEnable()
    {
        originalPosition = transform.position;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void OnDisable()
    {
        transform.position = originalPosition;
    }

}
