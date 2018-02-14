using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGame : MonoBehaviour {

    public bool runningGame = true;

    bool matchComplete = false;

    public Ball ball;
    public CameraTilt camTilt;
    public UnityEngine.UI.Image fadeCurtain;

    public void SetMatchComplete(bool flag)
    {
        matchComplete = flag;
    }

    public void SetRunningGameOver()
    {
        runningGame = false;

        // Pause board and reset
        StartCoroutine(RestartGame(2f));
    }

    IEnumerator RestartGame (float time)
    {
        yield return new WaitForSeconds(time);
        if (!matchComplete)
        {
            isFading = true;
            StartCoroutine(WaitToFadeBlack(1f));
        }
    }
    IEnumerator WaitToFadeBlack(float time)
    {
        yield return new WaitForSeconds(time);
        // If is match / set point, interject here!
        isFading = false;
        StartCoroutine(WaitToFadeWhite(0.5f));
    }
    IEnumerator WaitToFadeWhite(float time)
    {
        yield return new WaitForSeconds(time);
        runningGame = true;
        GetComponent<BoardsInPlay>().rightBoard.Reset();
        GetComponent<BoardsInPlay>().leftBoard.Reset();
        ball.Start();
        camTilt.Reset();
    }

    bool isFading = false;
    private float fadeSteps = 0.05f;
    private void Update()
    {
        if (isFading)
        {
            if (fadeCurtain.color != new Color(0f, 0f, 0f, 255f))
            {
                fadeCurtain.color = new Color(Mathf.MoveTowards(fadeCurtain.color.r, 0, fadeSteps), Mathf.MoveTowards(fadeCurtain.color.g, 0, fadeSteps), Mathf.MoveTowards(fadeCurtain.color.b, 0, fadeSteps), Mathf.MoveTowards(fadeCurtain.color.a, 255f, fadeSteps));
            }
        }
        else
        {
            if (fadeCurtain.color != new Color(0f, 0f, 0f, 0f))
            {
                fadeCurtain.color = new Color(Mathf.MoveTowards(fadeCurtain.color.r, 0, fadeSteps), Mathf.MoveTowards(fadeCurtain.color.g, 0, fadeSteps), Mathf.MoveTowards(fadeCurtain.color.b, 0, fadeSteps), Mathf.MoveTowards(fadeCurtain.color.a, 0, fadeSteps));
            }
        }
    }
}
