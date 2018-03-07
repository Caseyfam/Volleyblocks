using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class FadeOnLoad : MonoBehaviour {
    UnityEngine.UI.Image fadeCurtain;

    private bool fadingIn = false;
    private const float alphaFadeSteps = 0.1f;

    void Awake()
    {
        fadeCurtain = GetComponent<UnityEngine.UI.Image>();
        StartCoroutine(FadeInPause(0.5f));
    }

    IEnumerator FadeInPause(float time)
    {
        yield return new WaitForSeconds(time);
        fadingIn = true;
    }

	void Update ()
    {
		if (fadingIn)
        {
            fadeCurtain.color = new Color(fadeCurtain.color.r, fadeCurtain.color.g, fadeCurtain.color.b, Mathf.MoveTowards(fadeCurtain.color.a, 0f, alphaFadeSteps));
            if (fadeCurtain.color.a == 0f)
            {
                Destroy(this);
            }
        }
	}
}
