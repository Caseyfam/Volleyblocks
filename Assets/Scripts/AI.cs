using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public bool useOldAI = false;

    public float turnWaitTime = 0.1f;

    Board board;
    ActiveSet activeSet;

    int[] topDestination = new int[2]; // row, column;
    int[] bottomDestination = new int[2];

    List<string> moves = new List<string>();

    [HideInInspector]
    public bool aiCanMove = false;

    public void Reset()
    {
        aiCanMove = false;
        moves = new List<string>();
        topDestination = new int[2];
        bottomDestination = new int[2];
    }

    // Use this for initialization
    void Awake ()
    {
        board = GetComponent<Board>();
        activeSet = GetComponent<ActiveSet>();
	}

    private void Start()
    {
        try
        {
            useOldAI = GameObject.Find("PassedObject").GetComponent<Passed>().useOldAI;
            turnWaitTime = GameObject.Find("PassedObject").GetComponent<Passed>().turnLength;
        }
        catch
        {

        }
    }

    bool locking = false;

    // Update is called once per frame
    void Update ()
    {
        if (activeSet.canMovePieces && aiCanMove)
        {
            if (moves.Count > 0)
            {
                switch (moves[0])
                {
                    case "COUNTERCLOCKWISE":
                        activeSet.RotateActiveSet("COUNTERCLOCKWISE");
                        activeSet.updateHighlight = true;
                        break;
                    case "CLOCKWISE":
                        activeSet.RotateActiveSet("CLOCKWISE");
                        activeSet.updateHighlight = true;
                        break;
                    case "RIGHT":
                        activeSet.MoveActiveSet("RIGHT");
                        activeSet.updateHighlight = true;
                        break;
                    case "LEFT":
                        activeSet.MoveActiveSet("LEFT");
                        activeSet.updateHighlight = true;
                        break;
                    default:
                        break;
                }
                StartCoroutine(TurnWait(turnWaitTime));
            }
            
            else //Crappy quickDrop code that bugs out
            {
                if (!locking)
                {
                    StartCoroutine(WaitToLock(turnWaitTime));
                    activeSet.updateHighlight = true;
                }
            }
            
            
            // Check if rotations done and in right column before dropping
            
           
        }

        // Take top block and try and put it connected to same color
        // Check bottom block and try to put it somewhere it doesn't interfere
        // If this can't be done, do the reverse (switch blocks)
    }

    public void ResetTurn()
    {
        aiCanMove = false;
        moves.Clear();
        CalcDesiredDestination();
        aiCanMove = true;
    }

    public int localChainLength = 0;
    string currentOrientation;
    int maxChainLength = 0;
    string maxOrientation = "UP";
    int[] maxBlockPos = new int[] { 0, 0 };

    void CalcDesiredDestination()
    {
        if (!useOldAI) // Should really get rid of this
        {
            List<int[]> openCoords = new List<int[]>();
            openCoords = ReturnOpenCoords();
            maxChainLength = 0;
            foreach (int[] blockPos in openCoords)
            {
                for (int i = 0; i < 4; i++)
                {
                    // Bottom block = core block
                    // Top block = dangle block
                    localChainLength = 0;
                    GameObject[,] tempBoardBlocks = new GameObject[12, 6];
                    System.Array.Copy(GetComponent<Board>().boardBlocks, tempBoardBlocks, GetComponent<Board>().boardBlocks.GetLength(0) * GetComponent<Board>().boardBlocks.GetLength(1));
                    switch (i)
                    {
                        case 0: // UP
                                // Make a temp Board with added blocks
                                // Pass in to chainCalc
                            tempBoardBlocks[blockPos[0], blockPos[1]] = activeSet.bottomBlock.gameObject;
                            try
                            {
                                tempBoardBlocks[blockPos[0] - 1, blockPos[1]] = activeSet.topBlock.gameObject;
                            }
                            catch
                            {
                                // Do nothing because if the AI chose this move then they have lost.
                            }
                            currentOrientation = "UP";
                            break;
                        case 1: // DOWN
                            try
                            {
                                tempBoardBlocks[blockPos[0] - 1, blockPos[1]] = activeSet.bottomBlock.gameObject;
                            }
                            catch
                            {

                            }
                            tempBoardBlocks[blockPos[0], blockPos[1]] = activeSet.topBlock.gameObject;
                            currentOrientation = "DOWN";
                            break;
                        case 2: // LEFT
                            if (blockPos[1] == 0)
                            {
                                tempBoardBlocks[blockPos[0], blockPos[1] + 1] = activeSet.bottomBlock.gameObject;
                                tempBoardBlocks[blockPos[0], blockPos[1]] = activeSet.topBlock.gameObject;
                            }
                            else
                            {
                                tempBoardBlocks[blockPos[0], blockPos[1]] = activeSet.bottomBlock.gameObject;
                                tempBoardBlocks[blockPos[0], blockPos[1] - 1] = activeSet.topBlock.gameObject;
                            }
                            currentOrientation = "LEFT";
                            break;
                        case 3: // RIGHT
                            if (blockPos[1] == 5)
                            {
                                tempBoardBlocks[blockPos[0], blockPos[1] - 1] = activeSet.bottomBlock.gameObject;
                                tempBoardBlocks[blockPos[0], blockPos[1]] = activeSet.topBlock.gameObject;
                            }
                            else
                            {
                                tempBoardBlocks[blockPos[0], blockPos[1]] = activeSet.bottomBlock.gameObject;
                                tempBoardBlocks[blockPos[0], blockPos[1] + 1] = activeSet.topBlock.gameObject;
                            }
                            currentOrientation = "RIGHT";
                            break;
                    }
                    tempBoardBlocks[blockPos[0], blockPos[1]].GetComponent<ChainInfo>().ChainCalculation(this, tempBoardBlocks[blockPos[0], blockPos[1]].GetComponent<Block>(), tempBoardBlocks, blockPos);
                    if (localChainLength > maxChainLength)
                    {
                        maxOrientation = currentOrientation;
                        maxBlockPos = blockPos;
                        maxChainLength = localChainLength;
                    }
                    else if (localChainLength == maxChainLength)
                    {
                        if (blockPos[0] > maxBlockPos[0])
                        {
                            maxOrientation = currentOrientation;
                            maxBlockPos = blockPos;
                            maxChainLength = localChainLength;
                        }
                    }

                    for (int k = 11; k >= 0; k--) // Start from bottom row
                    {
                        for (int j = 0; j < 6; j++) // Columns
                        {
                            if (tempBoardBlocks[k, j] != null)
                            {
                                tempBoardBlocks[k, j].GetComponent<ChainInfo>().wasHit = false;
                            }
                        }
                    }
                }
            }
            BasicMoveSetter(maxBlockPos, maxOrientation);
        }
    }

    List<int[]> ReturnOpenCoords()
    {
        List<int[]> returnList = new List<int[]>();
        for (int i = 0; i < 6; i++)
        {
            for (int j = 11; j >= 0; j--)
            {
                if (!board.boardBools[j,i])
                {
                    int[] tempCoord = new int[2]; // row, column
                    tempCoord[0] = j;
                    tempCoord[1] = i;
                    returnList.Add(tempCoord);
                    break;
                }
            }
        }
        return returnList;
    }

    void BasicMoveSetter(int[] coord, string orientation)
    {
        switch (orientation)
        {
            case "UP":
                break;
            case "DOWN":
                moves.Add("CLOCKWISE");
                moves.Add("CLOCKWISE");
                break;
            case "LEFT":
                moves.Add("COUNTERCLOCKWISE");
                break;
            case "RIGHT":
                moves.Add("CLOCKWISE");
                break;
            default:
                break;
        }

        if (coord[1] < 2) // column check
        {
            for (int i = 2; i > coord[1]; i--)
            {
                moves.Add("LEFT");
            }
        }
        else if (coord[1] > 2)
        {
            for (int i = 2; i < coord[1]; i++)
            {
                moves.Add("RIGHT");
            }
        }
    }

    IEnumerator WaitToLock(float time)
    {
        locking = true;
        yield return new WaitForSeconds(time);
        activeSet.LockBlocks(true);
        locking = false;
    }

    IEnumerator TurnWait(float time)
    {
        aiCanMove = false;
        yield return new WaitForSeconds(time);

        if (moves.Count > 0)
        {
            moves.RemoveAt(0);
        }

        aiCanMove = true;
    }

}
