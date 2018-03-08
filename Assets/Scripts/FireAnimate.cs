using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimate : MonoBehaviour {

    public Sprite[] fire;

    public float waitTime;
    public bool isRandom = false;

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
        if (isRandom)
        {
            currentIndex = Random.Range(0, fire.Length - 1);
        }
        else
        {
            currentIndex++;
        }
        if (currentIndex >= fire.Length)
        {
            currentIndex = 0;
        }
        thisRenderer.sprite = fire[currentIndex];
        StartCoroutine(Animate(waitTime));
    }
}
