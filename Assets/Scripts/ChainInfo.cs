using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainInfo : MonoBehaviour {

    public bool wasHit = false;
    public int chainLength = 0;
	
    public void ChainCalculation(Board board, Block thisBlock)
    {
        board.localChainLength++;
        wasHit = true;
        board.modifiedChains.Add(this);

        if (thisBlock.above != null && thisBlock.above.GetComponent<Block>().blockColor == thisBlock.blockColor && !thisBlock.above.GetComponent<ChainInfo>().wasHit)
        {
            thisBlock.above.GetComponent<ChainInfo>().ChainCalculation(board, thisBlock.above.GetComponent<Block>());
        }
        if (thisBlock.below != null && thisBlock.below.GetComponent<Block>().blockColor == thisBlock.blockColor && !thisBlock.below.GetComponent<ChainInfo>().wasHit)
        {
            thisBlock.below.GetComponent<ChainInfo>().ChainCalculation(board, thisBlock.below.GetComponent<Block>());
        }
        if (thisBlock.right != null && thisBlock.right.GetComponent<Block>().blockColor == thisBlock.blockColor && !thisBlock.right.GetComponent<ChainInfo>().wasHit)
        {
            thisBlock.right.GetComponent<ChainInfo>().ChainCalculation(board, thisBlock.right.GetComponent<Block>());
        }
        if (thisBlock.left != null && thisBlock.left.GetComponent<Block>().blockColor == thisBlock.blockColor && !thisBlock.left.GetComponent<ChainInfo>().wasHit)
        {
            thisBlock.left.GetComponent<ChainInfo>().ChainCalculation(board, thisBlock.left.GetComponent<Block>());
        }
    }

    public void ChainCalculation(AI ai, Block thisBlock, GameObject[,] boardBlocks, int[] blockPos)
    {
        ai.localChainLength++;
        wasHit = true;

        if (blockPos[0] + 1 <= 11)
        {
            ChainCalculationHelper(boardBlocks[blockPos[0] + 1, blockPos[1]], ai, thisBlock, boardBlocks, blockPos);
        }
        if (blockPos[0] - 1 >= 0)
        {
            ChainCalculationHelper(boardBlocks[blockPos[0] - 1, blockPos[1]], ai, thisBlock, boardBlocks, blockPos);
        }
        if (blockPos[1] - 1 >= 0)
        {
            ChainCalculationHelper(boardBlocks[blockPos[0], blockPos[1] - 1], ai, thisBlock, boardBlocks, blockPos);
        }
        if (blockPos[1] + 1 <= 5)
        {
            ChainCalculationHelper(boardBlocks[blockPos[0], blockPos[1] + 1], ai, thisBlock, boardBlocks, blockPos);
        }
            
    }

    void ChainCalculationHelper(GameObject nextBlock, AI ai, Block thisBlock, GameObject[,] boardBlocks, int[] blockPos)
    {
        if (nextBlock != null && nextBlock.GetComponent<Block>().blockColor == thisBlock.blockColor && !nextBlock.GetComponent<ChainInfo>().wasHit)
        {
            nextBlock.GetComponent<ChainInfo>().ChainCalculation(ai, nextBlock.GetComponent<Block>(), boardBlocks, blockPos);
        }
    }
}
