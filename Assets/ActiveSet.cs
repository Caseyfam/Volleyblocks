using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSet : MonoBehaviour
{
    public GameObject redPrefab, bluePrefab, greenPrefab, yellowPrefab, redDrivePrefab, blueDrivePrefab, greenDrivePrefab, yellowDrivePrefab;

    public bool isCPU = false;

    [HideInInspector]
    public bool canMovePieces = false;

    [HideInInspector]
    public GameObject bottomTile, topTile;

    //[HideInInspector]
    public Block bottomBlock, topBlock; // bottom = center, top = dangling

    [HideInInspector]
    public string orientation = "UP";

    private bool bottomIsDrive = false;

    [HideInInspector]
    public int bottomRow, bottomColumn, topRow, topColumn;

    private int blockNum = 0;

    [HideInInspector]
    public bool canManualLock = false;

    private string[] tileNames;

    List<GameObject> modifiedRandomizer;
    Board board;

    UnityEngine.Random random = new UnityEngine.Random();

    public void Reset()
    {
        canMovePieces = false;
        bottomTile = null; topTile = null; bottomBlock = null; topBlock = null;
        orientation = "UP";
        bottomIsDrive = false;
        blockNum = 0;
        canManualLock = false;
        if (isCPU)
        {
            GetComponent<AI>().Reset();
        }

        modifiedRandomizer = new List<GameObject> { redPrefab, redPrefab, redPrefab, bluePrefab, bluePrefab, bluePrefab, greenPrefab, greenPrefab, greenPrefab, yellowPrefab, yellowPrefab, yellowPrefab, redDrivePrefab, blueDrivePrefab, greenDrivePrefab, yellowDrivePrefab };
        CreateActiveSet();
    }

    void Awake()
    {
        board = GetComponent<Board>();
    }

    public string[] SelectRandomBlock()
    {
        if (modifiedRandomizer.Count <= 0)
        {
            modifiedRandomizer = new List<GameObject> { redPrefab, redPrefab, redPrefab, bluePrefab, bluePrefab, bluePrefab, greenPrefab, greenPrefab, greenPrefab, yellowPrefab, yellowPrefab, yellowPrefab, redDrivePrefab, blueDrivePrefab, greenDrivePrefab, yellowDrivePrefab };
        }

        string[] returnTileNames = new string[2];
        int firstIndex = Random.Range(0, modifiedRandomizer.Count);
        returnTileNames[0] = modifiedRandomizer[firstIndex].name;
        modifiedRandomizer.RemoveAt(firstIndex);
        int secondIndex = Random.Range(0, modifiedRandomizer.Count);
        returnTileNames[1] = modifiedRandomizer[secondIndex].name;
        modifiedRandomizer.RemoveAt(secondIndex);

        return returnTileNames;
    }

    public void CreateActiveSet()
    {
        tileNames = SelectRandomBlock();

        CreateActiveSet(tileNames[0], tileNames[1]);
    }

    public void CreateActiveSet(string block1, string block2)
    {
        if (!GetComponent<GameOver>().gameOver)
        {
            bottomIsDrive = false;
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
                GetComponent<AI>().ResetTurn();
            }

            StopAllCoroutines();
            StartCoroutine(BlockFall(0.8f));
        }
    }

    void SetTilePosition(int bottomRow, int bottomColumn, int topRow, int topColumn, Vector3 bottomAddition, Vector3 topAddition, string direction, bool instant)
    {
        switch (orientation)
        {
            case "UP":
                bottomBlock.SetBlockPosition(bottomRow, bottomColumn, bottomAddition, instant);
                topBlock.SetBlockPosition(topRow, topColumn, topAddition, instant);
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

        topBlock.CalculateNeighbors();
        bottomBlock.CalculateNeighbors();
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
    void Update()
    {
        if (canMovePieces)
        {
            if (!isCPU)
            {
                QuickDrop();
                ManualLock();
                if (Input.GetMouseButtonDown(0))
                {
                    RotateActiveSet("COUNTERCLOCKWISE");
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    RotateActiveSet("CLOCKWISE");
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    MoveActiveSet("RIGHT");
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    MoveActiveSet("LEFT");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    MoveActiveSet("DOWN");
                }
            }
        }
    }

    IEnumerator BlockFall(float time)
    {
        yield return new WaitForSeconds(time);

        switch (orientation)
        {
            case "UP":
                if (!CheckIfOverlapping(bottomRow + 1, bottomColumn))
                {
                    SetTilePosition(bottomRow + 1, bottomColumn, topRow + 1, topColumn, new Vector3(0f, -0.8f), new Vector3(0f, -0.8f), true);
                }
                else // Target block is overlapping
                {
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
        StartCoroutine(BlockFall(0.8f));
    }

    void QuickDrop()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LockBlocks(true);
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
        if (canManualLock && Input.GetKeyDown(KeyCode.S))
        {
            LockBlocks(true);
        }
    }

    public void LockBlocks(bool instant)
    {
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
