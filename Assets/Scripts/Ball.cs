using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    bool determinedFirstMove = false;
    Board leftBoard, rightBoard;
    int currentPointLead = 0;
    int previousPointLead = 0;

    private string currentDirection = "NONE";

    public float maxHeight = 3.33f;
    public float minHeight = -2.33f;

    [HideInInspector]
    public bool serveDone = false;

    private bool servePause = false;

    private float rotationSpeed = 1f;

    float ballSpeed = 1f;
    private int randomServeRotation = 0;

    public BoardsInPlay boardsInPlay;
    public RunningGame runningGame;

    void Awake()
    {
        leftBoard = boardsInPlay.leftBoard;
        rightBoard = boardsInPlay.rightBoard;
    }

    void Reset()
    {
        randomServeRotation = Random.Range(0, 2);
        currentPointLead = 0;
        determinedFirstMove = false;
        transform.position = new Vector3(0f, -2.64f, 10f);
        transform.localScale = new Vector3(6.197968f, 6.197968f, 6.197968f);
        currentDirection = "NONE";
        GetComponent<SpriteRenderer>().color = Color.white;
        servePause = false;
        serveDone = false;
        GameObject.Find("ReadySign").GetComponent<ReadySign>().Reset();
        ballSpeed = 1f;
        rotationSpeed = 1f;
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        StartCoroutine(ServePause(0.2f));
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
        if (runningGame.runningGame)
        {
            CheckIfLost("LEFT");
        }
        SetBallPoints(rightBoard);
    }

    public void SetLeftBallPoints()
    {
        //Debug.Log("Setting left");
        if (runningGame.runningGame)
        {
            CheckIfLost("RIGHT");
        }
        SetBallPoints(leftBoard);
    }

    void CheckIfLost(string boardPosition)
    {
        if (runningGame.runningGame)
        {
            if (boardPosition == "LEFT")
            {
                // If points less than ball points, you lose
                if (rightBoard)
                {
                    if (!rightBoard.currentColor.Equals(GetComponent<SpriteRenderer>().color))
                    {
                        if (rightBoard.GetPoints() < currentPointLead)
                        {
                            rightBoard.GetComponent<GameOver>().SetGameOver(false);
                        }
                    }
                    else
                    {
                        Debug.Log("Colors were same");
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
                    Debug.Log(attackedBoard.name + " " + attackedBoard.GetPoints() + " CurrentPointLead: " + currentPointLead);
                    previousPointLead = currentPointLead;
                    currentPointLead = attackedBoard.GetPoints();


                    if (currentPointLead >= previousPointLead + 10)
                    {
                        // Super emote
                        attackedBoard.EmoteBoard("sideWin");
                    }
                    else
                    {
                        // Normal emote
                        switch (Random.Range(0,5))
                        {
                            default:
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                attackedBoard.EmoteBoard("sideIdle");
                                break;
                            case 4:
                                attackedBoard.EmoteBoard("sideLose");
                                break;
                        }
                    }

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
                    currentDirection = "LEFT";
                    //transform.position = new Vector3(6f, 0.5f, 10f);
                    // set ball starting position here
                    break;
                case "RIGHT": // Aim to the right
                    currentDirection = "RIGHT";
                    //transform.position = new Vector3(-6f, 0.5f, 10f);
                    // set ball starting position here
                    break;
            }
            transform.localScale = new Vector3(10 - Mathf.Abs(transform.position.x), 10 - Mathf.Abs(transform.position.x), 0f);
            currentPointLead += 1;
            determinedFirstMove = true;
        }
    }

    void FixedUpdate()
    {
        if (serveDone)
        {
            if (currentDirection == "NONE")
            {
                if (randomServeRotation == 1)
                {
                    rotationSpeed = 10f;
                }
                else
                {
                    rotationSpeed = -10f;
                }
            }
            else
            {
                if (currentDirection == "LEFT")
                {
                    rotationSpeed = currentPointLead * 1.2f;
                }
                else
                {
                    rotationSpeed = currentPointLead * -1.2f;
                }
            }
            transform.localScale = new Vector3(10 - Mathf.Abs(transform.position.x), 10 - Mathf.Abs(transform.position.x), 0f);
            transform.Rotate(0f, 0f, 20f * rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (servePause)
            {
                if (randomServeRotation == 1)
                {
                    rotationSpeed = 10f;
                }
                else
                {
                    rotationSpeed = -10f;
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 0.63f, 10f), 0.07f);
                transform.Rotate(0f, 0f, 20f * rotationSpeed * Time.deltaTime);
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(10 - Mathf.Abs(transform.position.x), 10 - Mathf.Abs(transform.position.x), 0f), 0.2f);
                if (transform.position == new Vector3(0f, 0.63f, 10f))
                {
                    leftBoard.gameObject.GetComponent<ActiveSet>().CreateActiveSet();
                    rightBoard.gameObject.GetComponent<ActiveSet>().CreateActiveSet();
                    serveDone = true;
                }
            }
        }

        if (runningGame.runningGame)
        {
            if (currentDirection.Equals("RIGHT"))
            {
                if (transform.position.x >= 6f)
                {
                    ballSpeed = 2f;
                    SetRightBallPoints();
                    currentDirection = "LEFT";
                }
                else
                {
                    transform.position += Vector3.right * Time.deltaTime * ballSpeed;
                }
            }
            else if (currentDirection.Equals("LEFT"))
            {
                if (transform.position.x <= -6f)
                {
                    ballSpeed = 2f;
                    SetLeftBallPoints();
                    currentDirection = "RIGHT";
                }
                else
                {
                    transform.position += Vector3.left * Time.deltaTime * ballSpeed;
                }
            }
            else
            {

            }
        }
    }
}
