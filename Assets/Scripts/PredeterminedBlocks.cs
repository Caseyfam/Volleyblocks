using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Choice { Red, Yellow, Green, Blue };

public class PredeterminedBlocks : MonoBehaviour {
    private int currentIndex = 0;
    public BlockSet[] nextBlocks;

    public string[] GetNextBlock()
    {
        currentIndex++;
        string[] set = new string[2];
        if (currentIndex >= nextBlocks.Length)
        {
            Destroy(this);
            set[0] = "Red";
            set[1] = "Yellow";
        }
        else
        {
            set[0] = nextBlocks[currentIndex].bottomBlock.ToString();
            set[1] = nextBlocks[currentIndex].topBlock.ToString();
            if (nextBlocks[currentIndex].bottomIsDrive)
            {
                set[0] += "Drive";
            }
            if (nextBlocks[currentIndex].topIsDrive)
            {
                set[1] += "Drive";
            }
        }
        return set;
    }
}

[Serializable]
public class BlockSet
{
    public Choice topBlock;
    public Choice bottomBlock;
    public bool topIsDrive;
    public bool bottomIsDrive;
}


