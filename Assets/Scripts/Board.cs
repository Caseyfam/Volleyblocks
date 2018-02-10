using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    private static int rows = 12;
    private static int columns = 6;

    public bool[,] boardBools = new bool[rows, columns];
    public GameObject[,] boardBlocks = new GameObject[rows, columns];

    public int blocksDestroyedCount = 0;
    ActiveSet activeSet;

    private int points = 0;

    bool calculatedFirstHit = false;

    public enum boardPosition { LEFT, RIGHT };

    public boardPosition thisBoardPosition;
    public SpriteRenderer leftWall, rightWall;

    public TextMesh comboText;
    public PortraitEmote portrait;

    [HideInInspector]
    public Sprite frontIdle, sideIdle, sideWin, sideLose, sideDefeat;

    public Ball ball;

    void Awake()
    {
        if (GameObject.Find("PassedObject"))
        {
            if (GameObject.Find("PassedObject").GetComponent<SelectedCharacters>())
            {
                SelectedCharacters selectedChars = GameObject.Find("PassedObject").GetComponent<SelectedCharacters>();
                if (thisBoardPosition == boardPosition.LEFT)
                {
                    Sprite[] charSprites = selectedChars.ReturnSprites("LEFT");
                    SetBoardSprites(charSprites[0], charSprites[1], charSprites[2], charSprites[3], charSprites[4]);
                }
                else
                {
                    Sprite[] charSprites = selectedChars.ReturnSprites("RIGHT");
                    SetBoardSprites(charSprites[0], charSprites[1], charSprites[2], charSprites[3], charSprites[4]);
                }
            }
        }

    }

    public void SetBoardSprites(Sprite frontIdle, Sprite sideIdle, Sprite sideWin, Sprite sideLose, Sprite sideDefeat)
    {
        this.frontIdle = frontIdle;
        this.sideIdle = sideIdle;
        this.sideWin = sideWin;
        this.sideLose = sideLose;
        this.sideDefeat = sideDefeat;
    }

    public void EmoteBoard(string state)
    {
        Sprite selectedSprite;
        switch (state)
        {
            case "frontIdle":
                selectedSprite = frontIdle;
                break;
            case "sideIdle":
                selectedSprite = sideIdle;
                break;
            case "sideWin":
                selectedSprite = sideWin;
                break;
            case "sideLose":
                selectedSprite = sideLose;
                break;
            case "sideDefeat":
                selectedSprite = sideDefeat;
                break;
            default:
                Debug.LogError("EmoteBoard in Board.CS defaulted");
                selectedSprite = sideIdle;
                break;
        }
        portrait.Emote(selectedSprite, 0.8f, 1f);
    }

    public void Reset()
    {
        boardBools = new bool[rows, columns];
        foreach (Transform child in transform)
        {
            if (!child.name.Equals("ColumnHighlight") && !child.name.Equals("LeftWall") && !child.name.Equals("RightWall") && !child.name.Equals("ComboText") && !child.name.Equals("Backing"))
            {
                Destroy(child.gameObject);
            }
        }
        boardBlocks = new GameObject[rows, columns];
        blocksDestroyedCount = 0;
        points = 0;
        calculatedFirstHit = false;
        GetComponent<GameOver>().gameOver = false;
        GetComponent<ActiveSet>().Reset();
        currentColor = Color.white;
    }

    // Use this for initialization
    void Start ()
    {
        Reset();
        activeSet = GetComponent<ActiveSet>();
    }

    public void CalculateFirstHit()
    {
        if (thisBoardPosition == boardPosition.LEFT)
        {
            ball.SetFirstMove("RIGHT", this);
        }
        else
        {
            ball.SetFirstMove("LEFT", this);
        }
    }

    private int comboLength = 1;

    void CleanBoardAlgorithm()
    {
        for (int i = rows - 1; i >= 0; i--) // Start from bottom row
        {
            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i, j] != null)
                {
                    boardBlocks[i, j].GetComponent<Block>().CalculateNeighbors();
                }
            }

            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i, j] != null)
                {
                    if (boardBlocks[i, j].GetComponent<Block>().isDrive)
                    {
                        boardBlocks[i, j].GetComponent<Block>().SetNeighborsToBeDestroyed();
                    }
                }
            }

            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i, j] != null)
                {
                    if (boardBlocks[i, j].GetComponent<Block>().willBeDestroyed)
                    {
                        boardBlocks[i, j].GetComponent<Block>().DeleteBlock();
                    }
                }
            }

            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i, j] != null)
                {
                    boardBlocks[i, j].GetComponent<Block>().GravityOnBlock();
                }
            }
        }
    }

    public void CleanBoard()
    {
        CleanBoardAlgorithm();
        CleanBoardAlgorithm(); // second pass

        StartCoroutine(ComboPause(0.1f));
    }

    IEnumerator ComboPause(float time)
    {
        bool redo = false;

        for (int i = rows - 1; i >= 0; i--) // Start from bottom row
        {
            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i, j] != null)
                {
                    if (boardBlocks[i, j].GetComponent<Block>().isDrive)
                    {
                        Block thisDrive = boardBlocks[i, j].GetComponent<Block>();
                        string thisBlockColor = thisDrive.GetComponent<Block>().blockColor;
                        try
                        {
                            if (thisBlockColor.Equals(thisDrive.above.GetComponent<Block>().blockColor))
                            {
                                redo = true;
                            }
                        }
                        catch
                        {

                        }
                        try
                        {
                            if (thisBlockColor.Equals(thisDrive.below.GetComponent<Block>().blockColor))
                            {
                                redo = true;
                            }
                        }
                        catch
                        {

                        }
                        try
                        {
                            if (thisBlockColor.Equals(thisDrive.left.GetComponent<Block>().blockColor))
                            {
                                redo = true;
                            }
                        }
                        catch
                        {

                        }
                        try
                        {
                            if (thisBlockColor.Equals(thisDrive.right.GetComponent<Block>().blockColor))
                            {
                                redo = true;
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }



        if (redo)
        {
            yield return new WaitForSeconds(time);
            comboLength++;
            comboText.text = "COMBO x" + comboLength;
            CleanBoard();
        }
        else
        {
            // Original, number of blocks matter
            // points += blocksDestroyedCount;

            // New, based on combo length * blocksDestroyed
            points += (blocksDestroyedCount * comboLength);
            comboLength = 1;
            StartCoroutine(WaitToClearComboText(1f));
            ComboColor();
            if (blocksDestroyedCount > 0)
            {
                if (!calculatedFirstHit)
                {
                    CalculateFirstHit();
                    calculatedFirstHit = true;
                }
            }
            blocksDestroyedCount = 0;

            StartCoroutine(CheckToCreateActiveSet());
            // Need to move this so that this only spawns when
            // all moving blocks are done
        }
    }

    IEnumerator CheckToCreateActiveSet()
    {
        bool busy = false;
        for (int i = rows - 1; i >= 0; i--) // Start from bottom row
        {
            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i, j] != null)
                {
                    if (boardBlocks[i, j].GetComponent<Block>().isBusy)
                    {
                        busy = true;
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.01f);
        if (busy)
        {
            StartCoroutine(CheckToCreateActiveSet());
        }
        else
        {
            activeSet.CreateActiveSet();
        }
    }

    IEnumerator WaitToClearComboText(float time)
    {
        yield return new WaitForSeconds(time);
        comboText.text = "";
    }

    Color32 white = new Color32(255, 255, 255, 255);
    Color32 red = new Color32(255, 0, 0, 255);
    Color32 lightRed = new Color32(255, 100, 100, 255);
    Color32 lightestRed = new Color32(255, 175, 175, 255);
    Color32 orange = new Color32(255, 150, 0, 255);
    Color32 lightOrange = new Color32(255, 150, 100, 255);
    Color32 lightestOrange = new Color32(255, 150, 175, 255);
    Color32 yellow = new Color32(255, 255, 0, 255);
    Color32 lightYellow = new Color32(255, 255, 100, 255);
    Color32 lightestYellow = new Color32(255, 255, 175, 255);
    Color32 green = new Color32(0, 255, 0, 255);
    Color32 lightGreen = new Color32(100, 255, 100, 255);
    Color32 lightestGreen = new Color32(100, 255, 175, 255);
    Color32 cyan = new Color32(0, 255, 255, 255);
    Color32 lightCyan = new Color32(100, 255, 255, 255);
    Color32 lightestCyan = new Color32(175, 255, 255, 255);
    Color32 blue = new Color32(0, 0, 255, 255);
    Color32 lightBlue = new Color32(100, 100, 255, 255);
    Color32 lightestBlue = new Color32(175, 175, 255, 255);
    Color32 purple = new Color32(150, 0, 255, 255);
    Color32 lightPurple = new Color32(150, 100, 255, 255);
    Color32 lightestPurple = new Color32(150, 175, 255, 255);
    Color32 black = new Color32(0, 0, 0, 255);

    // Maybe head into light colors for highest values


    public Color32 currentColor;
    void ComboColorCompare(int min, int max, Color32 setColor)
    {
        if (points >= min && points < max)
        {
            currentColor = setColor;
            rightWall.color = setColor;

            // Need to update color strip
            //GameObject.Find("GameLogic").GetComponent<ColorStrip>().UpdateStrip(setColor);
        }
    }

    void ComboColor()
    {
        ComboColorCompare(0, 1, white);
        ComboColorCompare(1, 10, red);
        ComboColorCompare(10, 20, orange);
        ComboColorCompare(20, 30, yellow);
        ComboColorCompare(30, 40, green);
        ComboColorCompare(40, 50, blue);
        ComboColorCompare(50, 60, cyan);
        ComboColorCompare(60, 70, purple);
        ComboColorCompare(70, 80, lightRed);
        ComboColorCompare(80, 90, lightOrange);
        ComboColorCompare(90, 100, lightYellow);
        ComboColorCompare(100, 110, lightGreen);
        ComboColorCompare(110, 120, lightBlue);
        ComboColorCompare(120, 130, lightCyan);
        ComboColorCompare(130, 140, lightPurple);
        ComboColorCompare(140, 150, lightestRed);
        ComboColorCompare(150, 160, lightestOrange);
        ComboColorCompare(160, 170, lightestYellow);
        ComboColorCompare(170, 180, lightestGreen);
        ComboColorCompare(180, 190, lightestBlue);
        ComboColorCompare(190, 200, lightestCyan);
        ComboColorCompare(200, 210, lightestPurple);
        ComboColorCompare(210, 220, black);

        leftWall.color = rightWall.color;
    }

    public int GetPoints()
    {
        //return Mathf.RoundToInt(points / 10) * 10;
        return points;
    }

    public void ResetPoints()
    {
        points = 0;
    }

    void DebugDrawBox(int row, int column)
    {
        GameObject newTile;
        newTile = (GameObject)Instantiate(Resources.Load("Red"));
        newTile.transform.parent = gameObject.transform;
        newTile.transform.localPosition = new Vector2((0.8f * column), -(0.8f * row));
        boardBools[row, column] = true;
    }

    [HideInInspector]
    public int localChainLength;
    [HideInInspector]
    public List<ChainInfo> modifiedChains = new List<ChainInfo>();

    public void CalcChainLengths()
    {
        for (int i = rows - 1; i >= 0; i--) // Start from bottom row
        {
            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i,j] != null)
                {
                    boardBlocks[i, j].GetComponent<Block>().CalculateNeighbors();
                    boardBlocks[i, j].GetComponent<ChainInfo>().chainLength = 0;
                    boardBlocks[i, j].GetComponent<ChainInfo>().wasHit = false;
                }
            }
        }

        for (int i = rows - 1; i >= 0; i--) // Start from bottom row
        {
            for (int j = 0; j < columns; j++) // Columns
            {
                if (boardBlocks[i,j] != null)
                {
                    if (!boardBlocks[i,j].GetComponent<ChainInfo>().wasHit)
                    {
                        modifiedChains.Clear();
                        localChainLength = 0;
                        Block thisBlock = boardBlocks[i, j].GetComponent<Block>();

                        boardBlocks[i, j].GetComponent<ChainInfo>().ChainCalculation(this, thisBlock);

                        foreach (ChainInfo chainScript in modifiedChains)
                        {
                            chainScript.chainLength = localChainLength;
                        }
                    }
                    
                }
            }
        }
    }

    public GameObject ReturnRight(int row, int column)
    {
        if (row >= 0)
        {
            if (column < (columns - 1)  && column >= 0)
            {
                return boardBlocks[row, column + 1];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public GameObject ReturnLeft(int row, int column)
    {
        if (row >= 0)
        {
            if (column > 0 && column <= (columns - 1))
            {
                return boardBlocks[row, column - 1];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public GameObject ReturnBelow(int row, int column)
    {
        if (row != (rows - 1))
        {
            if (column >= 0 && column <= (columns - 1))
            {
                return boardBlocks[row + 1, column];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public GameObject ReturnAbove(int row, int column)
    {
        if (row > 0)
        {
            if (column >= 0 && column <= (columns - 1))
            {
                return boardBlocks[row - 1, column];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
