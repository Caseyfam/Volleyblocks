using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardLetterEffect : MonoBehaviour {

    private bool isGrowing = false;

    Vector3 target, originalSize;
    private float maxDistanceDelta = 0.1f;

	void Awake ()
    {
        originalSize = transform.localScale;	
	}
	
	void Update ()
    {
		if (isGrowing)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, target, maxDistanceDelta);
            if (transform.localScale == target)
            {
                isGrowing = false;
            }
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originalSize, maxDistanceDelta);
        }
	}

    public void StartGrowing(float targetMultiplier)
    {
        target = originalSize * targetMultiplier;
        isGrowing = true;
    }

    public void StartGrowing(float targetMultiplier, float newDistance)
    {
        maxDistanceDelta = newDistance;
        StartGrowing(targetMultiplier);
    }
}
