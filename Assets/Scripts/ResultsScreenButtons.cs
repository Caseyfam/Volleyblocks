using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreenButtons : MonoBehaviour {
    public FadeSmart fadeSmart;
    private const float fadeTime = 1f;

    public void StoryRetry()
    {
        StartCoroutine(FadeToScene(fadeTime, 1));
    }

    public void StoryContinue()
    {
        StartCoroutine(FadeToScene(fadeTime, 2));
    }

    public void MainMenu()
    {
        StartCoroutine(FadeToScene(fadeTime, 0));
    }

    IEnumerator FadeToScene(float time, int desiredScene)
    {
        StartCoroutine(fadeSmart.StartFade(time));
        yield return new WaitForSeconds(time);
        UnityEngine.SceneManagement.SceneManager.LoadScene(desiredScene);
    }
}
