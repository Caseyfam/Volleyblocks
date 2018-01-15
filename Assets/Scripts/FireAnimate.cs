using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimate : MonoBehaviour {

    public Sprite[] fire;

    public float waitTime;

    private int currentIndex = 0;
    private SpriteRenderer thisRenderer;
	// Use this for initialization
	void Awake()
    {
        thisRenderer = GetComponent<SpriteRenderer>();
	}
	
    void Start()
    {
        StartCoroutine(Animate(waitTime));
    }

	IEnumerator Animate(float time)
    {
        yield return new WaitForSeconds(time);
        currentIndex++;
        if (currentIndex >= fire.Length)
        {
            currentIndex = 0;
        }
        thisRenderer.sprite = fire[currentIndex];
        StartCoroutine(Animate(waitTime));
    }
}
