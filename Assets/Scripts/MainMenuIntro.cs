using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuIntro : MonoBehaviour {

    public UnityEngine.UI.Image flash;
    bool isFlashing = false;
    float flashSteps = 0.1f;

    public GameObject mainMenu, movingBackground;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(WaitToFlash(4f));
	}
	
    void Update()
    {
        if (isFlashing)
        {
            flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, Mathf.MoveTowards(flash.color.a, 255f, flashSteps));
        }
        else
        {
            flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, Mathf.MoveTowards(flash.color.a, 0f, flashSteps));
        }
    }

    IEnumerator WaitToFlash(float time)
    {
        yield return new WaitForSeconds(time);
        isFlashing = true;
        StartCoroutine(WaitToUnflash(0.3f));
    }

    IEnumerator WaitToUnflash(float time)
    {
        yield return new WaitForSeconds(time);
        isFlashing = false;
        mainMenu.SetActive(true);
        movingBackground.SetActive(true);
        StartCoroutine(WaitToDestroy(3f));
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
