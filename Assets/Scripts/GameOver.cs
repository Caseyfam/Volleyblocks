using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    Board board;
    //[HideInInspector]
    public bool gameOver = false;
    public RunningGame runningGame;
    public BoardsInPlay boardsInPlay;
    public GameObject gameLogic;
    public Scoring scoring;

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
                if (runningGame.runningGame)
                {
                    SetGameOver(false);
                }
            }
        }
    }

    public void SetGameOver(bool won)
    {
        runningGame.SetRunningGameOver();
        if (won)
        {

        }
        if(!won)
        {
            if (!boardsInPlay.leftBoard.Equals(board))
            {
                boardsInPlay.leftBoard.GetComponent<GameOver>().SetGameOver(true);
            }
            else if (!boardsInPlay.rightBoard.Equals(board))
            {
                boardsInPlay.rightBoard.GetComponent<GameOver>().SetGameOver(true);
            }
            Debug.Log(board.name + " lost with " + board.GetPoints());
            board.GetComponent<ExplodeOnLoss>().ExplodeBoard();
            scoring.PlayerLost(board);
            runningGame.SetRunningGameOver();
        }
        
        gameOver = true;
        if (GetComponent<AI>() != null)
        {
            GetComponent<AI>().aiCanMove = false;
        }
    }
}
