using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    bool determinedFirstMove = false;
    Board leftBoard, rightBoard;
    int currentPointLead;

    void Awake()
    {
        leftBoard = GameObject.Find("GameLogic").GetComponent<BoardsInPlay>().leftBoard;
        rightBoard = GameObject.Find("GameLogic").GetComponent<BoardsInPlay>().rightBoard;
    }

    void Reset()
    {
        currentPointLead = 0;
        GetComponentInChildren<TextMesh>().text = currentPointLead.ToString();
        determinedFirstMove = false;
        GetComponent<Animator>().Play("Sit");
        GetComponent<Animator>().SetBool("LeftHitFirst", false);
        GetComponent<Animator>().SetBool("RightHitFirst", false);
    }

	// Use this for initialization
	public void Start ()
    {
        Reset();
        GetComponent<Animator>().speed = 0.5f;
	}
	
	public void SetRightBallPoints()
    {
        //Debug.Log("Setting right");
        if (GameObject.Find("GameLogic").GetComponent<RunningGame>().runningGame)
        {
            CheckIfLost("LEFT");
        }
        SetBallPoints(rightBoard);
    }

    public void SetLeftBallPoints()
    {
        //Debug.Log("Setting left");
        if (GameObject.Find("GameLogic").GetComponent<RunningGame>().runningGame)
        {
            CheckIfLost("RIGHT");
        }
        SetBallPoints(leftBoard);
    }

    void CheckIfLost(string boardPosition)
    {
        if (GameObject.Find("GameLogic").GetComponent<RunningGame>().runningGame)
        {
            if (boardPosition == "LEFT")
            {
                // If points less than ball points, you lose
                if (rightBoard)
                {
                    if (rightBoard.GetPoints() < currentPointLead)
                    {
                        rightBoard.GetComponent<GameOver>().SetGameOver(false);
                        GetComponent<Animator>().speed = 0;
                    }
                }
            }
            else if (boardPosition == "RIGHT")
            {
                if (leftBoard)
                {
                    if (leftBoard.GetPoints() < currentPointLead)
                    {
                        leftBoard.GetComponent<GameOver>().SetGameOver(false);
                        GetComponent<Animator>().speed = 0;
                    }
                }
            }
        }
    }

    void SetBallPoints(Board attackedBoard)
    {
        if (attackedBoard)
        {
            if (!attackedBoard.GetComponent<GameOver>().gameOver)
            {
                if (attackedBoard.GetPoints() >= currentPointLead)
                {
                    //Debug.Log(attackedBoard.GetPoints());
                    currentPointLead = attackedBoard.GetPoints();
                    GetComponentInChildren<TextMesh>().text = currentPointLead.ToString();

                    // Call color change here
                    GetComponent<SpriteRenderer>().color = attackedBoard.GetComponent<Board>().currentColor;

                    attackedBoard.ResetPoints();
                    // anim.Play other volley
                }
            }
            else
            {
                GetComponent<Animator>().speed = 0;
            }
        }
    }

    public void SetFirstMove(string direction, Board attackingBoard)
    {
        if (!determinedFirstMove)
        {
            SetBallPoints(attackingBoard);
            switch (direction)
            {
                case "LEFT": // Aim to the left
                    GetComponent<Animator>().SetBool("RightHitFirst", true);
                    break;
                case "RIGHT": // Aim to the right
                    GetComponent<Animator>().SetBool("LeftHitFirst", true);
                    break;
            }
            determinedFirstMove = true;
        }
    }
}
