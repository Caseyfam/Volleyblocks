using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimate : MonoBehaviour {

    SpriteRenderer sr;
    Coroutine updateSprite;

    public Sprite[] sprites;
    public float waitTime;

    private int spriteIndex = 0;
	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        updateSprite = StartCoroutine(WaitToUpdateSprite(waitTime));
	}
	

    public void SetAnimating(bool val)
    {
        if (val)
        {
            updateSprite = StartCoroutine(WaitToUpdateSprite(waitTime));
        }
        else
        {
            StopCoroutine(updateSprite);
        }
    }

    IEnumerator WaitToUpdateSprite(float time)
    {
        yield return new WaitForSeconds(time);
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        sr.sprite = sprites[spriteIndex];
        StartCoroutine(WaitToUpdateSprite(time));
    }
}
