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
                        break;
                    case "CLOCKWISE":
                        activeSet.RotateActiveSet("CLOCKWISE");
                        break;
                    case "RIGHT":
                        activeSet.MoveActiveSet("RIGHT");
                        break;
                    case "LEFT":
                        activeSet.MoveActiveSet("LEFT");
                        break;
                    default:
                        break;
                }
                StartCoroutine(TurnWait(turnWaitTime));
            }
            
            else //Crappy quickDrop code that bugs out
            {
                activeSet.LockBlocks(true);
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

    // ONLY FOR OLD AI DEBUG PURPOSES
    bool foundConnectingPos = false;
    string calcOrientation = "UP";

    void ConnectingPosCalc(int[] blockPos, int rowAdd1, int colAdd1, int rowAdd2, int colAdd2, string blockColor)
    {
        if (board.boardBlocks[blockPos[0] + rowAdd1, blockPos[1] + colAdd1] && board.boardBlocks[blockPos[0] + rowAdd2, blockPos[1] + colAdd2])
        {
            // If right block color equal to bottom block color equal to bottom block color
            if (board.boardBlocks[blockPos[0] + rowAdd1, blockPos[1] + colAdd1].GetComponent<Block>().blockColor == activeSet.bottomBlock.blockColor &&
                board.boardBlocks[blockPos[0] + rowAdd2, blockPos[1] + colAdd2].GetComponent<Block>().blockColor == activeSet.bottomBlock.blockColor &&
                board.boardBlocks[blockPos[0] + rowAdd1, blockPos[1] + colAdd1].GetComponent<Block>().blockColor == board.boardBlocks[blockPos[0] + rowAdd2, blockPos[1] + colAdd2].GetComponent<Block>().blockColor)
            {
                topDestination = blockPos;
                // Set orientation
                foundConnectingPos = true;
            }
        }
    }
    // END ONLY FOR OLD AI DEBUG PURPOSES

    void CalcDesiredDestination()
    {
        if (useOldAI) // For fun debugging purposes
        {
            List<int[]> openCoords = new List<int[]>();
            openCoords = ReturnOpenCoords();
            bool foundPos = false;
            foundConnectingPos = false;
            calcOrientation = "UP";
            int maxChain = 0;
            int[] tempDestination = new int[2];
            foreach (int[] blockPos in openCoords)
            {
                if (!foundConnectingPos)
                {
                    if (blockPos[0] + 1 <= 11 && blockPos[0] - 1 >= 0 && blockPos[1] - 1 >= 0 && blockPos[1] + 1 <= 5)
                    {
                        ConnectingPosCalc(blockPos, 0, 1, 1, 0, activeSet.bottomBlock.blockColor);
                        ConnectingPosCalc(blockPos, 0, -1, 1, 0, activeSet.bottomBlock.blockColor);
                        ConnectingPosCalc(blockPos, 0, -1, 0, 1, activeSet.bottomBlock.blockColor);
                        ConnectingPosCalc(blockPos, -1, 1, 0, 0, activeSet.topBlock.blockColor);
                        ConnectingPosCalc(blockPos, -1, -1, 0, 0, activeSet.topBlock.blockColor);
                        ConnectingPosCalc(blockPos, -1, -1, -1, 1, activeSet.topBlock.blockColor);
                    }
                }
            }
            if (!foundConnectingPos)
            {
                foreach (int[] blockPos in openCoords)
                {
                    if (blockPos[0] + 1 <= 11 && blockPos[0] >= 1 && blockPos[1] >= 0 && blockPos[1] <= 5)
                    {
                        if (blockPos[1] + 1 <= 5 && board.boardBlocks[blockPos[0], blockPos[1] + 1] != null)
                        {
                            if (board.boardBlocks[blockPos[0], blockPos[1] + 1].GetComponent<Block>().blockColor == activeSet.bottomBlock.blockColor)
                            {
                                if (board.boardBlocks[blockPos[0], blockPos[1] + 1].GetComponent<ChainInfo>().chainLength >= maxChain)
                                {
                                    maxChain = board.boardBlocks[blockPos[0], blockPos[1] + 1].GetComponent<ChainInfo>().chainLength;
                                    topDestination = blockPos;
                                    foundPos = true;
                                    calcOrientation = "UP";
                                }
                            }
                        }
                        if (blockPos[1] - 1 >= 0)
                        {
                            if (board.boardBlocks[blockPos[0], blockPos[1] - 1] != null)
                            {
                                if (board.boardBlocks[blockPos[0], blockPos[1] - 1].GetComponent<Block>().blockColor == activeSet.bottomBlock.blockColor)
                                {
                                    if (board.boardBlocks[blockPos[0], blockPos[1] - 1].GetComponent<ChainInfo>().chainLength >= maxChain)
                                    {
                                        maxChain = board.boardBlocks[blockPos[0], blockPos[1] - 1].GetComponent<ChainInfo>().chainLength;
                                        topDestination = blockPos;
                                        foundPos = true;
                                        calcOrientation = "UP";
                                    }
                                }
                            }
                        }
                        if (board.boardBlocks[blockPos[0] + 1, blockPos[1]].GetComponent<Block>().blockColor == activeSet.bottomBlock.blockColor)
                        {
                            if (board.boardBlocks[blockPos[0] + 1, blockPos[1]].GetComponent<ChainInfo>().chainLength > maxChain)
                            {
                                maxChain = board.boardBlocks[blockPos[0] + 1, blockPos[1]].GetComponent<ChainInfo>().chainLength;
                                topDestination = blockPos;
                                foundPos = true;
                                calcOrientation = "UP";
                            }
                        }
                        if (board.boardBlocks[blockPos[0] + 1, blockPos[1]].GetComponent<Block>().blockColor == activeSet.topBlock.blockColor)
                        {
                            if (board.boardBlocks[blockPos[0] + 1, blockPos[1]].GetComponent<ChainInfo>().chainLength > maxChain)
                            {
                                maxChain = board.boardBlocks[blockPos[0] + 1, blockPos[1]].GetComponent<ChainInfo>().chainLength;
                                topDestination = blockPos;
                                foundPos = true;
                                calcOrientation = "DOWN";
                            }
                        }
                        if (blockPos[1] + 1 <= 5)
                        {
                            if (board.boardBlocks[blockPos[0], blockPos[1] + 1] != null)
                            {
                                if (board.boardBlocks[blockPos[0], blockPos[1] + 1].GetComponent<Block>().blockColor == activeSet.topBlock.blockColor)
                                {
                                    if (board.boardBlocks[blockPos[0], blockPos[1] + 1].GetComponent<ChainInfo>().chainLength >= maxChain)
                                    {
                                        maxChain = board.boardBlocks[blockPos[0], blockPos[1] + 1].GetComponent<ChainInfo>().chainLength;
                                        topDestination = blockPos;
                                        foundPos = true;
                                        calcOrientation = "DOWN";
                                    }
                                }
                            }
                        }
                        if (blockPos[1] - 1 >= 0)
                        {
                            if (board.boardBlocks[blockPos[0], blockPos[1] - 1] != null)
                            {
                                if (board.boardBlocks[blockPos[0], blockPos[1] - 1].GetComponent<Block>().blockColor == activeSet.topBlock.blockColor)
                                {
                                    if (board.boardBlocks[blockPos[0], blockPos[1] - 1].GetComponent<ChainInfo>().chainLength >= maxChain)
                                    {
                                        maxChain = board.boardBlocks[blockPos[0], blockPos[1] - 1].GetComponent<ChainInfo>().chainLength;
                                        topDestination = blockPos;
                                        foundPos = true;
                                        calcOrientation = "DOWN";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!foundPos && !foundConnectingPos)
            {
                topDestination[0] = 0; // Who cares
                topDestination[1] = Random.Range(0, 6);
                int[] lowestDestination = new int[2];
                int lowestHeight = 0;
                foreach (int[] blockPos in openCoords)
                {
                    if (blockPos[0] > lowestHeight)
                    {
                        lowestHeight = blockPos[0];
                        lowestDestination = blockPos;
                    }
                }
                topDestination = lowestDestination;
                switch (Random.Range(0, 4))
                {
                    case 0:
                        calcOrientation = "LEFT";
                        break;
                    case 1:
                        calcOrientation = "RIGHT";
                        break;
                    case 2:
                        calcOrientation = "UP";
                        break;
                    case 3:
                        calcOrientation = "DOWN";
                        break;
                    default:
                        break;
                }
            }
            BasicMoveSetter(topDestination, calcOrientation);
        }
        else // Better, new AI! Woo
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
                            tempBoardBlocks[blockPos[0] - 1, blockPos[1]] = activeSet.bottomBlock.gameObject;
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
                    //Debug.Log(localChainLength + " " + maxOrientation + " " + maxBlockPos[0] + "," + maxBlockPos[1]);
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
