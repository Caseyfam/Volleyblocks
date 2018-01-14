using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacters : MonoBehaviour {

    private string leftBoardName = "";
    private string rightBoardName = "";

    // frontIdle, sideIdle, sideWin, sideLose, sideDefeat
    public Sprite[] buffRaw;
    public Sprite[] hattyRaw;

    public void SetLeftBoard(string name)
    {
        leftBoardName = name;
    }

    public void SetRightBoard(string name)
    {
        rightBoardName = name;
    }

	public Sprite[] ReturnSprites(string orientation)
    {
        string nameToReturn = "";
        if (orientation.Equals("LEFT"))
        {
            nameToReturn = leftBoardName;
        }
        else
        {
            nameToReturn = rightBoardName;
        }

        switch (nameToReturn)
        {
            default:
            case "buffRaw":
                return buffRaw;
            case "hattyRaw":
                return hattyRaw;
        }
    }
	
	
}
