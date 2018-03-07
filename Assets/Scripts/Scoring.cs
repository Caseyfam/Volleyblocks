using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour {

    public int gamesToSet = 3;
    public int setsToWin = 3;
    private int leftGames = 0, leftSets = 0, rightGames = 0, rightSets = 0;

    private Passed passedObject;
    private RunningGame runningGame;

    public Scoreboard scoreboard;

    bool matchComplete = false;

    private void Start()
    {
        runningGame = GetComponent<RunningGame>();
        try
        {
            passedObject = GameObject.Find("PassedObject").GetComponent<Passed>();
            gamesToSet = passedObject.gamesCount;
            setsToWin = passedObject.setCount;
        }
        catch
        {
            Debug.LogError("Couldn't get Passed");
            gamesToSet = 1;
            setsToWin = 1;
        }
    }

    public void PlayerLost (Board board)
    {
        if (board.Equals(GetComponent<BoardsInPlay>().rightBoard))
        {
            leftGames++;
            scoreboard.SetLeftGames(leftGames);
            CheckIfGame(leftGames, "RIGHT");
        }
        else
        {
            rightGames++;
            scoreboard.SetRightGames(rightGames);
            CheckIfGame(rightGames, "LEFT");
        }
        bool matchPoint = false;
        bool setPoint = false;
        if (leftGames == gamesToSet - 1 || rightGames == gamesToSet - 1)
        {
            if (leftSets == setsToWin - 1)
            {
                // Match point
                matchPoint = true;
                setPoint = false;
            }
            else if (rightSets == setsToWin - 1)
            {
                // Match point
                matchPoint = true;
                setPoint = false;
            }
            else
            {
                // Set point only
                setPoint = true;
                matchPoint = false;
            }
        }

        runningGame.SetMatchComplete(matchComplete);
        runningGame.SetRunningGameOver(setPoint, matchPoint);
    }

    void CheckIfGame(int points, string position)
    {
        if (position.Equals("RIGHT"))
        {
            if (points >= gamesToSet)
            {
                points = 0;
                leftSets++;
                scoreboard.SetLeftSets(leftSets);
                leftGames = 0;
                rightGames = 0;
                scoreboard.SetLeftGames(leftGames);
                scoreboard.SetRightGames(rightGames);
                CheckIfWinner(leftSets, position);
            }
        }
        else
        {
            if (points >= gamesToSet)
            {
                points = 0;
                rightSets++;
                scoreboard.SetRightSets(rightSets);
                rightGames = 0;
                leftGames = 0;
                scoreboard.SetRightGames(rightGames);
                scoreboard.SetLeftGames(leftGames);
                CheckIfWinner(rightSets, position);
            }
        }
    }

    private string playerWon;

    void CheckIfWinner(int games, string position)
    {
        if (position.Equals("RIGHT"))
        {
            if (games >= setsToWin)
            {
                Debug.Log("LEFT BOARD WON THE SET");
                playerWon = "won";
                matchComplete = true;
                ResultsScreen();
            }
        }
        else
        {
            if (games >= setsToWin)
            {
                Debug.Log("RIGHT BOARD WON THE SET");
                playerWon = "lost";
                matchComplete = true;
                ResultsScreen();
            }
        }
    }

    void ResultsScreen()
    {
        runningGame.SetMatchComplete(true);
        try
        {
            GetComponent<ResultsScreen>().ResultsSetup(playerWon, passedObject.isStory);
        }
        catch
        {
            GetComponent<ResultsScreen>().ResultsSetup(playerWon, false);
        }
        if (passedObject != null)
        {
            if (playerWon == "won" && passedObject.isStory)
            {
                passedObject.storyIndex++;
            }
        }
    }
}
