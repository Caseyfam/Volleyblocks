using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour {

    private bool isScaling = false;
    public Vector3 targetScale;
    public float stepSpeed;

	void Update ()
    {
		if (isScaling)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, stepSpeed);
            if (transform.localScale.Equals(targetScale))
            {
                isScaling = false;
            }
        }
	}

    void OnEnable()
    {
        isScaling = true;
    }
}
