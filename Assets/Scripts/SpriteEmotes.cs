using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEmotes : MonoBehaviour {

    public enum Direction { LEFT, RIGHT };
    public Direction spritePosition;

    private bool isMoving = false;
    private bool isShaking = false;
    private float moveSpeed = 1f;
    private float shakeRange = 1f;

    private float originalX, originalY;
    private Vector3 originalScale;
    private Vector3 target;

	// Use this for initialization
	void Awake ()
    {
	    if (spritePosition == Direction.LEFT)
        {
            originalX = -5f;
        }
        else
        {
            originalX = 5f;
        }
        originalY = transform.position.y;
        originalScale = transform.localScale;
	}

    private void Start()
    {
        //Shake(Random.Range(1, 6), Random.Range(0.1f, 0.3f));
    }

    // Update is called once per frame
    void Update ()
    {
		if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
            if (transform.position == target)
            {
                isMoving = false;
                transform.localScale = originalScale;

            }
        }

        if (isShaking)
        {
            transform.position = new Vector3(Random.Range(originalX - shakeRange, originalX + shakeRange), Random.Range(originalY - shakeRange, originalY + shakeRange));
        }
    }

    public void EnterStageSide(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        isMoving = true;
        if (spritePosition == Direction.LEFT)
        {
            transform.position = new Vector3(-15f, originalY);
        }
        else
        {
            transform.position = new Vector3(15f, originalY);
        }
        target = new Vector3(originalX, originalY);
    }

    public void ExitStageSide(float moveSpeed, bool flip)
    {
        if (flip)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
        this.moveSpeed = moveSpeed;
        isMoving = true;
        if (spritePosition == Direction.LEFT)
        {
            target = new Vector3(-15f, originalY);
        }
        else
        {
            target = new Vector3(15f, originalY);
        }
    }

    public void ExitStageOppositeSide(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        isMoving = true;
        if (spritePosition == Direction.LEFT)
        {
            target = new Vector3(15f, originalY);
        }
        else
        {
            target = new Vector3(-15f, originalY);
        }
    }

    public void ExitStageBottom(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        isMoving = true;
        target = new Vector3(originalX, originalY - 15f);
    }

    public void EnterStageBottom(float moveSpeed)
    {
        if (spritePosition == Direction.LEFT)
        {
            transform.position = new Vector3(originalX, originalY - 15f);
        }
        else
        {
            transform.position = new Vector3(originalX, originalY - 15f);
        }
        this.moveSpeed = moveSpeed;
        isMoving = true;
        target = new Vector3(originalX, originalY);
    }

    public void Shake(float time, float shakeRange)
    {
        this.shakeRange = shakeRange;
        StartCoroutine(ShakeTimer(time));
    }

    IEnumerator ShakeTimer(float time)
    {
        isShaking = true;
        yield return new WaitForSeconds(time);
        isShaking = false;
    }
}
