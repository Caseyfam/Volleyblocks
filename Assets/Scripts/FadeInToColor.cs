using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInToColor : MonoBehaviour {

    public Color targetColor;
    public float fadeStep = 0.01f;
    private bool isFadingIn = false;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	void OnEnable()
    {
        isFadingIn = true;
    }

    void Update()
    {
        if (isFadingIn)
        {
            sr.color = new Color(Mathf.MoveTowards(sr.color.r, targetColor.r, fadeStep), Mathf.MoveTowards(sr.color.g, targetColor.g, fadeStep), Mathf.MoveTowards(sr.color.b, targetColor.b, fadeStep), Mathf.MoveTowards(sr.color.a, targetColor.a, fadeStep));
            if (sr.color == targetColor)
            {
                isFadingIn = false;
            }
        }
    }
}
