using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitEmote : MonoBehaviour
{
    public bool onRight = false;
    private Vector3 originalPos;
    private Vector3 leftTarget, rightTarget;
    private Vector3 target;

    private float stepSpeed = 0.2f;
    private float waitTime = 2f;

    private bool emoting = false;
    private bool exiting = false;

    private void Awake()
    {
        originalPos = transform.position;
        leftTarget = new Vector3(-6f, originalPos.y, originalPos.z);
        rightTarget = new Vector3(6f, originalPos.y, originalPos.z);
        if (onRight)
        {
            target = rightTarget;
        }
        else
        {
            target = leftTarget;
        }
    }
	
	void Update ()
    {
		if (emoting)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, stepSpeed);
            if (transform.position == target)
            {
                emoting = false;
                StartCoroutine(PausePortrait(waitTime));
            }
        }
        if (exiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, stepSpeed);
            if (transform.position == originalPos)
            {
                exiting = false;
            }
        }
	}

    IEnumerator PausePortrait(float time)
    {
        yield return new WaitForSeconds(time);
        exiting = true;
    }

    public void Emote(Sprite emoteSprite, float stepSpeed, float waitTime)
    {
        GetComponent<SpriteRenderer>().sprite = emoteSprite;
        this.stepSpeed = stepSpeed;
        this.waitTime = waitTime;
        emoting = true;
    }
}
