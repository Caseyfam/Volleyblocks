using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public int row;
    public int column;
    public string blockColor;
    public bool isDrive = false;
    public bool willBeDestroyed = false;
    public bool isBusy = false;

    public GameObject above, right, below, left;

    private Vector3 target;
    private bool isMoving = false;
    private bool isDestroying = false;

    public Board board;

	public void SetRow(int row)
    {
        this.row = row;
    }

    public void SetColumn(int column)
    {
        this.column = column;
    }

    bool CheckIfOverlapping(int row, int column)
    {
        try
        {
            return board.boardBools[row, column];
        }
        catch
        {
            return true;
        }
    }

   public void SetBlockPosition(int row, int column, Vector3 addition, bool instant)
    {
        if (!isBusy)
        {
            try
            {
                board.boardBlocks[this.row, this.column] = null;
                board.boardBools[this.row, this.column] = false;
            }
            catch
            {

            }

            this.row = row;
            this.column = column;

            try
            {
                if (instant && gravityActive)
                {
                    isMoving = false;
                    StartCoroutine(WaitToInstantFall(addition, 0.1f));
                }
                else if (instant)
                {
                    isMoving = false;
                    transform.position += addition;
                }
                else
                {
                    target = transform.position + addition;
                    isMoving = true;
                    isBusy = true;
                }
            }
            catch
            {

            }

            try
            {
                board.boardBlocks[row, column] = gameObject;
                board.boardBools[row, column] = true;
            }
            catch
            {

            }

            CalculateNeighbors();
        }
    }

    IEnumerator WaitToInstantFall(Vector3 addition, float time)
    {
        yield return new WaitForSeconds(time);
        transform.position += addition;
        gravityActive = false;
    }

    public void SetBlockPosition(int row, int column, Vector3 addition)
    {
        SetBlockPosition(row, column, addition, true);
    }

    void Update()
    {
        if (isDestroying)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.5f);
            if (transform.localScale == Vector3.zero)
            {
                Destroy(gameObject);
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
            if (transform.position == target)
            {
                isMoving = false;
                isBusy = false;
            }
        }
    }

    public void SetNeighborsToBeDestroyed()
    {
        if (isDrive)
        {
            if (above != null && above.GetComponent<Block>().blockColor == blockColor)
            {
                willBeDestroyed = true;
                if (above.GetComponent<Block>().isDrive)
                {
                    above.GetComponent<Block>().isDrive = false;
                }
                above.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
            if (below != null && below.GetComponent<Block>().blockColor == blockColor)
            {
                willBeDestroyed = true;
                if (below.GetComponent<Block>().isDrive)
                {
                    below.GetComponent<Block>().isDrive = false;
                }
                below.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
            if (right != null && right.GetComponent<Block>().blockColor == blockColor)
            {
                willBeDestroyed = true;
                if (right.GetComponent<Block>().isDrive)
                {
                    right.GetComponent<Block>().isDrive = false;
                }
                right.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
            if (left != null && left.GetComponent<Block>().blockColor == blockColor)
            {
                willBeDestroyed = true;
                if (left.GetComponent<Block>().isDrive)
                {
                    left.GetComponent<Block>().isDrive = false;
                }
                left.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
        }
        else
        {
            willBeDestroyed = true;

            if (above != null && above.GetComponent<Block>().blockColor == blockColor && !above.GetComponent<Block>().willBeDestroyed)
            {
                above.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
            if (below != null && below.GetComponent<Block>().blockColor == blockColor && !below.GetComponent<Block>().willBeDestroyed)
            {
                below.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
            if (right != null && right.GetComponent<Block>().blockColor == blockColor && !right.GetComponent<Block>().willBeDestroyed)
            {
                right.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
            if (left != null && left.GetComponent<Block>().blockColor == blockColor && !left.GetComponent<Block>().willBeDestroyed)
            {
                left.GetComponent<Block>().SetNeighborsToBeDestroyed();
            }
        }
    }

    private bool gravityActive = false;

    public void GravityOnBlock() // Used in Board.Clean
    {
        gravityActive = true;
        int gravRow = row;
        for (int i = row; i < 12; i++)
        {
            if(!CheckIfOverlapping(gravRow + 1, column))
            {
                gravRow = i;
            }
        }
        SetBlockPosition(gravRow, column, new Vector3(0f, -0.8f * (gravRow - row)), true);
        // Non-instant blockFall vs. instant
        //SetBlockPosition(gravRow, column, new Vector3(0f, -0.8f * (gravRow - row)), true);
    }


    public void CalculateNeighbors()
    {
        above = board.ReturnAbove(row, column);
        below = board.ReturnBelow(row, column);
        right = board.ReturnRight(row, column);
        left = board.ReturnLeft(row, column);
    }

    public void DeleteBlock()
    {
        //board.coordinatesToDelete.Add(new int[] { row, column });
        //board.blocksToDelete.Add(gameObject);
        board.boardBools[row, column] = false;
        board.boardBlocks[row, column] = null;
        board.blocksDestroyedCount++;

        GameObject newParticle = (GameObject)Instantiate(Resources.Load("DestroyedBlockParticle"));
        newParticle.transform.position = transform.position;
        newParticle.transform.parent = transform.parent.transform.parent.transform;
        GameObject wallToPass;
        switch (Random.Range(0,2))
        {
            case 0:
                wallToPass = transform.parent.GetComponent<Board>().leftWall.gameObject;
                break;
            case 1:
            default:
                 wallToPass = transform.parent.GetComponent<Board>().rightWall.gameObject;
                break;
        }
        newParticle.GetComponent<BlockDestroyParticle>().CreateParticle(blockColor, wallToPass);

        isDestroying = true;
        //Debug.Log("Added " + row + " " + column);
    }
}
