﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSet : MonoBehaviour
{
    public GameObject redPrefab, bluePrefab, greenPrefab, yellowPrefab, redDrivePrefab, blueDrivePrefab, greenDrivePrefab, yellowDrivePrefab;
    public float blockFallSpeed = 0.8f; // Should get this from some global object maybe

    public bool isCPU = false;

    [HideInInspector]
    public bool canMovePieces = false;

    [HideInInspector]
    public GameObject bottomTile, topTile;

    //[HideInInspector]
    public Block bottomBlock, topBlock; // bottom = center, top = dangling

    [HideInInspector]
    public string orientation = "UP";

    [HideInInspector]
    public int bottomRow, bottomColumn, topRow, topColumn;

    private int blockNum = 0;

    [HideInInspector]
    public bool canManualLock = false;

    private string[] tileNames;

    List<GameObject> modifiedRandomizer;
    Board board;

    // Privately held variables previously obtained with GetComponent
    FallHighlight fallHighlight;
    AI ai;

    public void Reset()
    {
        canMovePieces = false;
        bottomTile = null; topTile = null; bottomBlock = null; topBlock = null;
        orientation = "UP";
        blockNum = 0;
        canManualLock = false;
        if (isCPU)
        {
            GetComponent<AI>().Reset();
        }

        ResetRandomizer();
        //CreateActiveSet();

        // Throw in start whistle function here
    }

    private int randomCounter = 0;
    void ResetRandomizer()
    {
        modifiedRandomizer = new List<GameObject> { redPrefab, redPrefab, redPrefab, bluePrefab, bluePrefab, bluePrefab, greenPrefab, greenPrefab, greenPrefab, yellowPrefab, yellowPrefab, yellowPrefab, redDrivePrefab, blueDrivePrefab, greenDrivePrefab, yellowDrivePrefab };
        randomCounter = 0;
    }

    void Awake()
    {
        fallHighlight = GetComponent<FallHighlight>();
        board = GetComponent<Board>();
        if (isCPU)
        {
            ai = GetComponent<AI>();
        }
        Reset();
    }

    public string[] SelectRandomBlock()
    {
        if (GetComponent<PredeterminedBlocks>())
        {
            return GetComponent<PredeterminedBlocks>().GetNextBlock();
        }
        else
        {
            if (modifiedRandomizer.Count <= 0)
            {
                modifiedRandomizer = new List<GameObject> { redPrefab, redPrefab, redPrefab, bluePrefab, bluePrefab, bluePrefab, greenPrefab, greenPrefab, greenPrefab, yellowPrefab, yellowPrefab, yellowPrefab, redDrivePrefab, blueDrivePrefab, greenDrivePrefab, yellowDrivePrefab };
            }

            string[] returnTileNames = new string[2];
            int firstIndex = Random.Range(0, modifiedRandomizer.Count);
            returnTileNames[0] = modifiedRandomizer[firstIndex].name;
            modifiedRandomizer.RemoveAt(firstIndex);

            returnTileNames = DetermineSecondBlock(returnTileNames);

            return returnTileNames;
        }
    }

    string[] DetermineSecondBlock(string[] returnTileNames)
    {
        bool shouldLoop = false;
        randomCounter++;
        if (randomCounter >= 5)
        {
            ResetRandomizer();
        }
        int secondIndex = Random.Range(0, modifiedRandomizer.Count);
        switch (returnTileNames[0])
        {
            default:
            case "Red":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "RedDrive");
                break;
            case "Yellow":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "YellowDrive");
                break;
            case "Green":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "GreenDrive");
                break;
            case "Blue":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "BlueDrive");
                break;
            case "RedDrive":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "Red");
                break;
            case "YellowDrive":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "Yellow");
                break;
            case "GreenDrive":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "Green");
                break;
            case "BlueDrive":
                shouldLoop = (modifiedRandomizer[secondIndex].name == "Blue");
                break;
        }

        if (shouldLoop)
        {
            return DetermineSecondBlock(returnTileNames);
        }
        else
        {
            returnTileNames[1] = modifiedRandomizer[secondIndex].name;
            modifiedRandomizer.RemoveAt(secondIndex);

            return returnTileNames;
        }
    }

    public void CreateActiveSet()
    {
        tileNames = SelectRandomBlock();

        CreateActiveSet(tileNames[0], tileNames[1]);
        updateHighlight = true;
    }

    public void CreateActiveSet(string block1, string block2)
    {
        if (!GetComponent<GameOver>().gameOver)
        {
            canManualLock = false;
            orientation = "UP";
            bottomTile = (GameObject)Instantiate(Resources.Load(block1));
            bottomTile.GetComponent<Block>().board = board;
            bottomTile.transform.parent = gameObject.transform;
            bottomTile.transform.localPosition = new Vector2(1.6f, 0f);
            bottomTile.name = "Block" + blockNum;
            blockNum++;
            bottomBlock = bottomTile.GetComponent<Block>();
            bottomRow = 0;
            bottomColumn = 2;
            bottomBlock.row = 0;
            bottomBlock.column = 2;
            topTile = (GameObject)Instantiate(Resources.Load(block2));
            topTile.GetComponent<Block>().board = board;
            topTile.transform.parent = gameObject.transform;
            topTile.transform.localPosition = new Vector2(1.6f, +0.8f);
            topTile.name = "Block" + blockNum;
            blockNum++;
            topBlock = topTile.GetComponent<Block>();
            topRow = -1;
            topColumn = 2;
            topBlock.row = -1;
            topBlock.column = 2;

            canMovePieces = true;

            if (isCPU)
            {
                ai.ResetTurn();
            }

            StopAllCoroutines();
            StartCoroutine(BlockFall(blockFallSpeed));
        }
    }

    void SetTilePosition(int bottomRow, int bottomColumn, int topRow, int topColumn, Vector3 bottomAddition, Vector3 topAddition, string direction, bool instant)
    {
        switch (orientation)
        {
            case "UP":
                try
                {
                    bottomBlock.SetBlockPosition(bottomRow, bottomColumn, bottomAddition, instant);
                    topBlock.SetBlockPosition(topRow, topColumn, topAddition, instant);
                }
                catch
                {

                }
                break;
            case "LEFT":
                if (direction.Equals("RIGHT"))
                {
                    bottomBlock.SetBlockPosition(bottomRow, bottomColumn, bottomAddition, instant);
                    topBlock.SetBlockPosition(topRow, topColumn, topAddition, instant);
                }
                else
                {
                    topBlock.SetBlockPosition(topRow, topColumn, topAddition, instant);
                    bottomBlock.SetBlockPosition(bottomRow, bottomColumn, bottomAddition, instant);     
                }
                break;
            case "RIGHT":
                if (direction.Equals("RIGHT"))
                {
                    topBlock.SetBlockPosition(topRow, topColumn, topAddition, instant);
                    bottomBlock.SetBlockPosition(bottomRow, bottomColumn, bottomAddition, instant);
                }
                else
                {
                    bottomBlock.SetBlockPosition(bottomRow, bottomColumn, bottomAddition, instant);
                    topBlock.SetBlockPosition(topRow, topColumn, topAddition, instant);
                }
                break;
            case "DOWN":
            default:
                topBlock.SetBlockPosition(topRow, topColumn, topAddition, instant);
                bottomBlock.SetBlockPosition(bottomRow, bottomColumn, bottomAddition, instant);
                break;
        }
        
        this.bottomRow = bottomRow;
        this.bottomColumn = bottomColumn;
        this.topRow = topRow;
        this.topColumn = topColumn;

        try
        {
            topBlock.CalculateNeighbors();
            bottomBlock.CalculateNeighbors();
        }
        catch
        {

        }
    }

    void SetTilePosition(int bottomRow, int bottomColumn, int topRow, int topColumn, Vector3 bottomAddition, Vector3 topAddition, bool instant)
    {
        SetTilePosition(bottomRow, bottomColumn, topRow, topColumn, bottomAddition, topAddition, "RIGHT", instant);
    }

    public void MoveActiveSet(string direction)
    {
        if (direction.Equals("RIGHT", System.StringComparison.InvariantCultureIgnoreCase))
        {
            switch (orientation)
            {
                case "UP":
                case "DOWN":
                    if (!CheckIfOverlapping(topRow, topColumn + 1) && !CheckIfOverlapping(bottomRow, bottomColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn + 1, topRow, topColumn + 1, new Vector3(0.8f, 0f), new Vector3(0.8f, 0f), direction, true);
                    }
                    break;
                case "LEFT":
                    if (!CheckIfOverlapping(bottomRow, bottomColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn + 1, topRow, topColumn + 1, new Vector3(0.8f, 0f), new Vector3(0.8f, 0f), direction, true);
                    }
                    break;
                case "RIGHT":
                    if (!CheckIfOverlapping(topRow, topColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn + 1, topRow, topColumn + 1, new Vector3(0.8f, 0f), new Vector3(0.8f, 0f), direction, true);
                    }
                    break;
            }
        }
        else if (direction.Equals("LEFT", System.StringComparison.InvariantCultureIgnoreCase))
        {
            switch (orientation)
            {
                case "UP":
                case "DOWN":
                    if (!CheckIfOverlapping(topRow, topColumn - 1) && !CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn - 1, topRow, topColumn - 1, new Vector3(-0.8f, 0f), new Vector3(-0.8f, 0f), direction, true);
                    }
                    break;
                case "LEFT":
                    if (!CheckIfOverlapping(topRow, topColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn - 1, topRow, topColumn - 1, new Vector3(-0.8f, 0f), new Vector3(-0.8f, 0f), direction, true);
                    }
                    break;
                case "RIGHT":
                    if (!CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn - 1, topRow, topColumn - 1, new Vector3(-0.8f, 0f), new Vector3(-0.8f, 0f), direction, true);
                    }
                    break;
            }
        }
        else if (direction.Equals("DOWN", System.StringComparison.InvariantCultureIgnoreCase))
        {
            if (topRow < 12 && bottomRow < 12)
            {
                switch (orientation)
                {
                    case "UP":
                        if (!CheckIfOverlapping(bottomRow + 1, bottomColumn))
                        {
                            SetTilePosition(bottomRow + 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, -0.8f), true);
                        }
                        break;
                    case "DOWN":
                        if (!CheckIfOverlapping(topRow + 1, topColumn))
                        {
                            SetTilePosition(bottomRow + 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, -0.8f), true);
                        }
                        break;
                    case "LEFT":
                    case "RIGHT":
                        if (!CheckIfOverlapping(topRow + 1, topColumn) && !CheckIfOverlapping(bottomRow + 1, bottomColumn))
                        {
                            SetTilePosition(bottomRow + 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, -0.8f), true);
                        }
                        break;
                }
            }
        }
    }

    public void RotateActiveSet(string direction)
    {
        if (direction.Equals("CLOCKWISE", System.StringComparison.InvariantCultureIgnoreCase))
        {
            switch (orientation)
            {
                case "UP": // to RIGHT
                    if (CheckIfOverlapping(bottomRow, bottomColumn + 1) && CheckIfOverlapping(bottomRow + 1, bottomColumn) && CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow - 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, 0.8f), new Vector3(0f, -0.8f), true);
                        orientation = "DOWN";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn + 1) && CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow + 2, topColumn, Vector3.zero, new Vector3(0f, -1.6f), true);
                        orientation = "DOWN";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn - 1, topRow + 1, topColumn, new Vector3(-0.8f, 0f), new Vector3(0f, -0.8f), true);
                        orientation = "RIGHT";
                    }
                    else
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow + 1, topColumn + 1, Vector3.zero, new Vector3(0.8f, -0.8f), true);
                        orientation = "RIGHT";
                    }
                    break;
                case "RIGHT": // to DOWN
                    if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn) && CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn + 1, topRow, topColumn + 1, new Vector3(0.8f, 0f), new Vector3(-0.8f, 0f), true);
                        orientation = "LEFT";
                    }
                    else if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow, topColumn - 2, Vector3.zero, new Vector3(-1.6f, 0f), true);
                        orientation = "LEFT";
                    }
                    else if (CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow - 1, bottomColumn, topRow, topColumn - 1, new Vector3(0f, 0.8f), new Vector3(-0.8f, 0f), true);
                        orientation = "DOWN";
                    }
                    else
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow + 1, topColumn - 1, Vector3.zero, new Vector3(-0.8f, -0.8f), true);
                        orientation = "DOWN";
                    }
                    break;
                case "DOWN": // to LEFT
                    if (CheckIfOverlapping(bottomRow, bottomColumn - 1) && CheckIfOverlapping(bottomRow, bottomColumn + 1) && CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow + 1, bottomColumn, topRow - 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, 0.8f), true);
                        orientation = "UP";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn - 1) && CheckIfOverlapping(bottomRow, bottomColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow - 2, topColumn, Vector3.zero, new Vector3(0f, 1.6f), true);
                        orientation = "UP";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn + 1, topRow - 1, topColumn, new Vector3(0.8f, 0f), new Vector3(0f, 0.8f), true);
                        orientation = "LEFT";
                    }
                    else
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow - 1, topColumn - 1, Vector3.zero, new Vector3(-0.8f, 0.8f), true);
                        orientation = "LEFT";
                    }
                    break;
                case "LEFT": // to UP
                    if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn) && CheckIfOverlapping(bottomRow, bottomColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn - 1, topRow, topColumn + 1, new Vector3(-0.8f, 0f), new Vector3(0.8f, 0f), true);
                        orientation = "RIGHT";
                    }
                    else if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow, topColumn - 2, Vector3.zero, new Vector3(1.6f, 0f), true);
                        orientation = "RIGHT";
                    }
                    else if (CheckIfOverlapping(bottomRow - 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow + 1, bottomColumn, topRow, topColumn + 1, new Vector3(0f, -0.8f), new Vector3(0.8f, 0f), true);
                        orientation = "UP";
                    }
                    else
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow - 1, topColumn + 1, Vector3.zero, new Vector3(0.8f, 0.8f), true);
                        orientation = "UP";
                    }
                    break;
            }
        }
        else if (direction.Equals("COUNTERCLOCKWISE", System.StringComparison.InvariantCultureIgnoreCase))
        {
            switch (orientation)
            {
                case "UP": // to LEFT
                    if (CheckIfOverlapping(bottomRow, bottomColumn + 1) && CheckIfOverlapping(bottomRow, bottomColumn - 1) && CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow - 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, 0.8f), new Vector3(0f, -0.8f), true);
                        orientation = "DOWN";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn + 1) && CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow + 1, topColumn, Vector3.zero, new Vector3(0f, -1.6f), true);
                        orientation = "DOWN";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn + 1, topRow + 1, topColumn, new Vector3(0.8f, 0f), new Vector3(0f, -0.8f), true);
                        orientation = "LEFT";
                    }
                    else
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow + 1, topColumn - 1, Vector3.zero, new Vector3(-0.8f, -0.8f), true);
                        orientation = "LEFT";
                    }
                    break;
                case "LEFT": // to DOWN
                    if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn) && CheckIfOverlapping(bottomRow, bottomColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn - 1, topRow, topColumn + 1, new Vector3(-0.8f, 0f), new Vector3(0.8f, 0f), true);
                        orientation = "RIGHT";
                    }
                    else if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow, topColumn + 2, Vector3.zero, new Vector3(1.6f, 0f), true);
                        orientation = "RIGHT";
                    }
                    else if (CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow - 1, bottomColumn, topRow, topColumn + 1, new Vector3(0f, 0.8f), new Vector3(0.8f, 0f), true);
                        orientation = "DOWN";
                    }
                    else 
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow + 1, topColumn + 1, Vector3.zero, new Vector3(0.8f, -0.8f), true);
                        orientation = "DOWN";
                    }
                    break;
                case "DOWN": // to RIGHT
                    if (CheckIfOverlapping(bottomRow, bottomColumn + 1) && CheckIfOverlapping(bottomRow, bottomColumn - 1) && CheckIfOverlapping(bottomRow - 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow + 1, bottomColumn, topRow - 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, 0.8f), true);
                        orientation = "UP";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn + 1) && CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow - 2, topColumn, Vector3.zero, new Vector3(0f, 1.6f), true);
                        orientation = "UP";
                    }
                    else if (CheckIfOverlapping(bottomRow, bottomColumn + 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn - 1, topRow - 1, topColumn, new Vector3(-0.8f, 0f), new Vector3(0f, 0.8f), true);
                        orientation = "RIGHT";
                    }
                    else
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow - 1, topColumn + 1, Vector3.zero, new Vector3(0.8f, 0.8f), true);
                        orientation = "RIGHT";
                    }
                    break;
                case "RIGHT": // to UP
                    if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn) && CheckIfOverlapping(bottomRow, bottomColumn - 1))
                    {
                        SetTilePosition(bottomRow, bottomColumn + 1, topRow, topColumn - 1, new Vector3(0.8f, 0f), new Vector3(-0.8f, 0f), true);
                        orientation = "LEFT";
                    }
                    else if (CheckIfOverlapping(bottomRow - 1, bottomColumn) && CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow, topColumn - 2, Vector3.zero, new Vector3(-1.6f, 0f), true);
                        orientation = "LEFT";
                    }
                    else if (CheckIfOverlapping(bottomRow - 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow + 1, bottomColumn, topRow, topColumn - 1, new Vector3(0f, -0.8f), new Vector3(-0.8f, 0f), true);
                        orientation = "UP";
                    }
                    else
                    {
                        SetTilePosition(bottomRow, bottomColumn, topRow - 1, topColumn - 1, Vector3.zero, new Vector3(-0.8f, 0.8f), true);
                        orientation = "UP";
                    }
                    break;
            }
        }
    }

    bool CheckIfOverlapping(int row, int column)
    {
        try
        {
            if ((row <= 0 && column >= 0) && (row <= 0 && column <= 5))
            {
                return false;
            }
            else
            {
                return board.boardBools[row, column];
            }
        }
        catch
        {
            if ((row <= 0 && column >= 0) && (row <= 0 && column <= 5))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    // Update is called once per frame
    [HideInInspector]
    public bool updateHighlight = false;
    private string vertInput, horInput, cclkInput, clkInput;

    public void SetInputs(string axisVertical, string axisHorizontal, string counterClockwise, string clockwise)
    {
        vertInput = axisVertical;
        horInput = axisHorizontal;
        cclkInput = counterClockwise;
        clkInput = clockwise;
    }

    void Update()
    {
        if (canMovePieces)
        {
            if (!isCPU && !GetComponent<GameOver>().gameOver)
            {
                if (Input.GetAxisRaw(vertInput) > 0.3f)
                {
                    if (!axisLocked)
                    {
                        LockBlocks(true);
                        updateHighlight = true;
                        PrepareAxisWait("Vertical");
                    }
                }
                if (Input.GetAxisRaw(vertInput) < -0.3f)
                {
                    ManualLock();
                }

                if (Input.GetButtonDown(cclkInput)) // was counterclockwise1
                {
                    RotateActiveSet("COUNTERCLOCKWISE");
                    updateHighlight = true;
                }
                else if (Input.GetButtonDown(clkInput)) // was clockwise1
                {
                    RotateActiveSet("CLOCKWISE");
                    updateHighlight = true;
                }
                if (Input.GetAxisRaw(horInput) > 0.3f)
                {
                    if (!axisLocked)
                    {
                        MoveActiveSet("RIGHT");
                        updateHighlight = true;
                        PrepareAxisWait(horInput);
                    }
                }
                else if (Input.GetAxisRaw(horInput) < -0.3f)
                {
                    if (!axisLocked)
                    {
                        MoveActiveSet("LEFT");
                        updateHighlight = true;
                        PrepareAxisWait(horInput);
                    }
                }
                if (Input.GetAxisRaw(vertInput) < -0.3f)
                {
                    if (!axisLocked)
                    {
                        MoveActiveSet("DOWN");
                        PrepareAxisWait(vertInput);
                    }
                }
            }
        }
        if (updateHighlight)
        {
            fallHighlight.UpdateHighlight(topTile, bottomTile, orientation);
            updateHighlight = false;
        }
        if (axisLocked && prevAxis != null)
        {
            if (Input.GetAxisRaw(prevAxis) == 0)
            {
                axisLocked = false;
                prevAxis = null;
            }
        }
    }

    private bool axisLocked = false;
    private bool downLock = false;
    private float timeToGo;
    private string prevAxis;
    void PrepareAxisWait(string newAxis)
    {
        prevAxis = newAxis;
        axisLocked = true;
        timeToGo = Time.fixedTime + 0.2f;
    }

    void FixedUpdate()
    {
        if (axisLocked && Time.fixedTime >= timeToGo)
        {
            Debug.Log("Down unlocked");
            downLock = false;
            axisLocked = false;
        }
    }

    void ManualLock()
    {
        switch (orientation)
        {
            case "UP":
                if (CheckIfOverlapping(bottomRow + 1, bottomColumn))
                {
                    canManualLock = true;
                }
                else
                {
                    canManualLock = false;
                }
                break;
            case "DOWN":
                if (CheckIfOverlapping(topRow + 1, topColumn))
                {
                    canManualLock = true;
                }
                else
                {
                    canManualLock = false;
                }
                break;
            case "LEFT":
            case "RIGHT":
                if (CheckIfOverlapping(topRow + 1, topColumn) || CheckIfOverlapping(bottomRow + 1, bottomColumn))
                {
                    canManualLock = true;
                }
                else
                {
                    canManualLock = false;
                }
                break;
        }
        if (canManualLock)
        {
            LockBlocks(true);
        }
    }

    IEnumerator BlockFall(float time)
    {
        yield return new WaitForSeconds(time);

        if (!downLock)
        {
            switch (orientation)
            {
                case "UP":
                    if (!CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        //Debug.Log("FALLING");
                        SetTilePosition(bottomRow + 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, -0.8f), true);
                    }
                    else // Target block is overlapping
                    {
                        //Debug.Log("LOCKING NOW");
                        yield return new WaitForSeconds(1f);
                        LockBlocks(true);
                    }
                    break;
                case "DOWN":
                    if (!CheckIfOverlapping(topRow + 1, topColumn))
                    {
                        SetTilePosition(bottomRow + 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, -0.8f), true);
                    }
                    else // Target block is overlapping
                    {
                        yield return new WaitForSeconds(1f);
                        LockBlocks(true);
                    }
                    break;
                case "LEFT":
                case "RIGHT":
                    if (!CheckIfOverlapping(topRow + 1, topColumn) && !CheckIfOverlapping(bottomRow + 1, bottomColumn))
                    {
                        SetTilePosition(bottomRow + 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, -0.8f), true);
                    }
                    else // Target block is overllaping
                    {
                        yield return new WaitForSeconds(1f);
                        LockBlocks(true);
                    }
                    break;
            }
            StopAllCoroutines();
            StartCoroutine(BlockFall(blockFallSpeed));
        }
    }

    public void LockBlocks(bool instant)
    {
        // Bug: LockBlocks during mid-gravity fall causes glitched block placement
        //      Really more of a GravityOnBlocks bug

        canMovePieces = false;
        GravityOnBlocks(instant);
        StopAllCoroutines();

        board.CleanBoard();
        if (isCPU)
        {
            board.CalcChainLengths();
        }

        GetComponent<GameOver>().CheckForGameOver();

        // Ideally, we would have board clean the board
        // and time it appropriately so that all drives are cleared
        // in a delayed avalanche. Once that's all done, 
        // make calls in board to do this bottom stuff like CreateActiveSet();

    }

    void GravityOnBlocks(bool instant)
    {
        int gravTopRow = topRow;
        int gravBottomRow = bottomRow;
        //Debug.Log("topRow: " + topRow + " bottomRow: " + bottomRow);
        switch (orientation)
        {
            case "UP":
                for (int i = bottomRow; i < 12; i++)
                {
                    if (!CheckIfOverlapping(gravBottomRow + 1, bottomColumn))
                    {
                        gravBottomRow = i;
                    }
                }
                gravTopRow = gravBottomRow - 1;
                SetTilePosition(gravBottomRow, bottomColumn, gravTopRow, topColumn, new Vector3(0f, -0.8f * (gravBottomRow - bottomRow)), new Vector3(0f, -0.8f * (gravTopRow - topRow)), instant);
                break;
            case "DOWN":
                for (int i = topRow; i < 12; i++)
                {
                    if (!CheckIfOverlapping(gravTopRow + 1, topColumn))
                    {
                        gravTopRow = i;
                    }
                }
                gravBottomRow = gravTopRow - 1;
                SetTilePosition(gravBottomRow, bottomColumn, gravTopRow, topColumn, new Vector3(0f, -0.8f * (gravBottomRow - bottomRow)), new Vector3(0f, -0.8f * (gravTopRow - topRow)), instant);
                break;
            case "LEFT":
            case "RIGHT":
                for (int i = topRow; i < 12; i++)
                {
                    if (!CheckIfOverlapping(gravTopRow + 1, topColumn))
                    {
                        gravTopRow = i;
                    }
                }
                for (int i = bottomRow; i < 12; i++)
                {
                    if (!CheckIfOverlapping(gravBottomRow + 1, bottomColumn))
                    {
                        gravBottomRow = i;
                    }
                }
                SetTilePosition(gravBottomRow, bottomColumn, gravTopRow, topColumn, new Vector3(0f, -0.8f * (gravBottomRow - bottomRow)), new Vector3(0f, -0.8f * (gravTopRow - topRow)), instant);
                break;
        }
    }
}
