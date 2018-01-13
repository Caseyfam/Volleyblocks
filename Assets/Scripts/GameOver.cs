using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    Board board;
    //[HideInInspector]
    public bool gameOver = false;

	// Use this for initialization
	void Start ()
    {
        board = GetComponent<Board>();
	}

    public void CheckForGameOver()
    {
        for (int j = 0; j < 6; j++) // Columns
        {
            if (board.boardBlocks[0, j] != null)
            {
                if (GameObject.Find("GameLogic").GetComponent<RunningGame>().runningGame)
                {
                    SetGameOver(false);
                }
            }
        }
    }

    public void SetGameOver(bool won)
    {
        GameObject.Find("GameLogic").GetComponent<RunningGame>().SetRunningGameOver();
        if (won)
        {

        }
        else
        {
            if (!GameObject.Find("GameLogic").GetComponent<BoardsInPlay>().leftBoard.Equals(board))
            {
                GameObject.Find("GameLogic").GetComponent<BoardsInPlay>().leftBoard.GetComponent<GameOver>().SetGameOver(true);
            }
            else if (!GameObject.Find("GameLogic").GetComponent<BoardsInPlay>().rightBoard.Equals(board))
            {
                GameObject.Find("GameLogic").GetComponent<BoardsInPlay>().rightBoard.GetComponent<GameOver>().SetGameOver(true);
            }
            Debug.Log(board.name + " lost with " + board.GetPoints());
            board.GetComponent<ExplodeOnLoss>().ExplodeBoard();
            GameObject.Find("GameLogic").GetComponent<Scoring>().PlayerLost(board);
            GameObject.Find("GameLogic").GetComponent<RunningGame>().SetRunningGameOver();
        }
        
        gameOver = true;
        if (GetComponent<AI>() != null)
        {
            GetComponent<AI>().aiCanMove = false;
        }
    }
}
