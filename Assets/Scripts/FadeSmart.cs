using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class FadeSmart : MonoBehaviour {

    UnityEngine.UI.Image thisImage;
    private bool isFading = false;
    private float alphaFadeSteps = 0.1f;
	
    void Awake()
    {
        thisImage = GetComponent<UnityEngine.UI.Image>();
    }

	// Update is called once per frame
	void Update ()
    {
	    if (isFading)
        {
            thisImage.color = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b, Mathf.MoveTowards(thisImage.color.a, 255f, alphaFadeSteps));
        }	
        else
        {
            thisImage.color = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b, Mathf.MoveTowards(thisImage.color.a, 0f, alphaFadeSteps));
        }
    }

    public IEnumerator StartFade(float time)
    {
        isFading = true;
        yield return new WaitForSeconds(time);
        isFading = false;
    }

    public void StartFadeNoEnd()
    {
        isFading = true;
    }

    public void StartFadeNoEnd(float newAlphaFadeSteps)
    {
        alphaFadeSteps = newAlphaFadeSteps;
        StartFadeNoEnd();
    }
}
