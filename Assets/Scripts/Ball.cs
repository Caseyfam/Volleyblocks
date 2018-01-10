using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    bool determinedFirstMove = false;
    Board leftBoard, rightBoard;
    int currentPointLead;
    int previousPointLead = 0;

    private string currentDirection = "NONE";

    public float maxHeight = 3.33f;
    public float minHeight = -2.33f;

    [HideInInspector]
    public bool serveDone = false;

    private bool servePause = false;

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
        transform.position = new Vector3(0f, -2.64f, 10f);
        transform.localScale = new Vector3(6.197968f, 6.197968f, 6.197968f);
        currentDirection = "NONE";
        GetComponent<SpriteRenderer>().color = Color.white;
        servePause = false;
        serveDone = false;
        GameObject.Find("ReadySign").GetComponent<ReadySign>().Reset();
        StartCoroutine(ServePause(0.7f));
    }

    IEnumerator ServePause(float time)
    {
        yield return new WaitForSeconds(time);
        servePause = true;
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
                    previousPointLead = currentPointLead;
                    currentPointLead = attackedBoard.GetPoints();

                    if (currentPointLead >= previousPointLead + 20)
                    {
                        // Super emote
                        attackedBoard.EmoteBoard("sideWin");
                    }
                    else
                    {
                        // Normal emote
                        attackedBoard.EmoteBoard("sideIdle");
                    }

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
        if (serveDone)
        {
            transform.localScale = new Vector3(10 - Mathf.Abs(transform.position.x), 10 - Mathf.Abs(transform.position.x), 0f);
        }
        else
        {
            if (servePause)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 0.63f, 10f), 0.07f);
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(10 - Mathf.Abs(transform.position.x), 10 - Mathf.Abs(transform.position.x), 0f), 0.2f);
                if (transform.position == new Vector3(0f, 0.63f, 10f))
                {
                    serveDone = true;
                }
            }
        }

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
