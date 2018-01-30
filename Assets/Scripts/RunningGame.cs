using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGame : MonoBehaviour {

    public bool runningGame = true;

    bool matchComplete = false;

    public Ball ball;
    public CameraTilt camTilt;

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
            runningGame = true;
            GetComponent<BoardsInPlay>().rightBoard.Reset();
            GetComponent<BoardsInPlay>().leftBoard.Reset();
            ball.Start();
            camTilt.Reset();
        }
        else
        {
            // Exit
        }

    }
}
