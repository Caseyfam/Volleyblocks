using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour {

    public int pointsToGame = 3;
    public int gamesToWin = 3;
    private int leftPoints = 0, leftGames = 0, rightPoints = 0, rightGames = 0;
    public TextMesh lPointsText, lGamesText, rPointsText, rGamesText;

    bool matchComplete = false;

    public void PlayerLost (Board board)
    {
        if (board.Equals(GetComponent<BoardsInPlay>().rightBoard))
        {
            leftPoints++;
            lPointsText.text = leftPoints.ToString();
            CheckIfGame(leftPoints, "RIGHT");
        }
        else
        {
            rightPoints++;
            rPointsText.text = rightPoints.ToString();
            CheckIfGame(rightPoints, "LEFT");
        }
        GetComponent<RunningGame>().SetMatchComplete(matchComplete);
        GetComponent<RunningGame>().SetRunningGameOver();
    }

    void CheckIfGame(int points, string position)
    {
        if (position.Equals("RIGHT"))
        {
            if (points >= pointsToGame)
            {
                points = 0;
                leftGames++;
                lGamesText.text = leftGames.ToString();
                lPointsText.text = "0";
                leftPoints = 0;
                CheckIfWinner(leftGames, position);
            }
        }
        else
        {
            if (points >= pointsToGame)
            {
                points = 0;
                rightGames++;
                rGamesText.text = rightGames.ToString();
                rPointsText.text = "0";
                rightPoints = 0;
                CheckIfWinner(rightGames, position);
            }
        }
    }

    private string playerWon;

    void CheckIfWinner(int games, string position)
    {
        if (position.Equals("RIGHT"))
        {
            if (games >= gamesToWin)
            {
                Debug.Log("LEFT BOARD WON THE SET");
                playerWon = "won";
                matchComplete = true;
                ReturnToOverworld();
            }
        }
        else
        {
            if (games >= gamesToWin)
            {
                Debug.Log("RIGHT BOARD WON THE SET");
                playerWon = "lost";
                matchComplete = true;
                ReturnToOverworld();
            }
        }
    }

    private void Update()
    {
        // Debug winner decider
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            Debug.Log("LEFT BOARD WON THE SET");
            playerWon = "won";
            matchComplete = true;
            ReturnToOverworld();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.Log("RIGHT BOARD WON THE SET");
            playerWon = "lost";
            matchComplete = true;
            ReturnToOverworld();
        }
    }

    void ReturnToOverworld()
    {
        try
        {
            GameObject.Find("PassedObject").GetComponent<PassedAI>().playerWon = playerWon;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        catch
        {
            Debug.Log("Should exit to overworld if PassedObject exists");
        }
    }
}
