using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    bool determinedFirstMove = false;
    Board leftBoard, rightBoard;
    int currentPointLead;

    private string currentDirection = "NONE";

    public float maxHeight = 3.33f;
    public float minHeight = -2.33f;

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
        transform.position = new Vector3(0f, 0.5f, 10f);
        currentDirection = "NONE";
        GetComponent<SpriteRenderer>().color = Color.white;
    }

	// Use this for initialization
	public void Start ()
    {
        Reset();
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
                    currentDirection = "RIGHT";
                    transform.position = new Vector3(6f, 0.5f, 10f);
                    // set ball starting position here
                    break;
                case "RIGHT": // Aim to the right
                    currentDirection = "LEFT";
                    transform.position = new Vector3(-6f, 0.5f, 10f);
                    // set ball starting position here
                    break;
            }
            transform.localScale = new Vector3(10 - Mathf.Abs(transform.position.x), 10 - Mathf.Abs(transform.position.x), 0f);
            determinedFirstMove = true;
        }
    }

    void Update()
    {
        transform.localScale = new Vector3(10 - Mathf.Abs(transform.position.x), 10 - Mathf.Abs(transform.position.x), 0f);

        if (GameObject.Find("GameLogic").GetComponent<RunningGame>().runningGame)
        {
            if (currentDirection.Equals("RIGHT"))
            {
                if (transform.position.x >= 6f)
                {
                    SetRightBallPoints();
                    currentDirection = "LEFT";
                }
                else
                {
                    transform.position += Vector3.right * Time.deltaTime * 2f;
                }
            }
            else if (currentDirection.Equals("LEFT"))
            {
                if (transform.position.x <= -6f)
                {
                    SetLeftBallPoints();
                    currentDirection = "RIGHT";
                }
                else
                {
                    transform.position += Vector3.left * Time.deltaTime * 2f;
                }
            }
            else
            {

            }
        }
    }
}
