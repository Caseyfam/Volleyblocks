using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacters : MonoBehaviour {

    public string leftBoardName = "", rightBoardName = "";

    // frontIdle, sideIdle, sideWin, sideLose, sideDefeat
    public Sprite[] buffRaw;
    public Sprite[] hattyRaw;
    public Sprite[] girlRaw;
    public Sprite[] profRaw;
    public Sprite[] senseiRaw;
    //-------------------------
    public Sprite[] buff;
    public Sprite[] hatty;
    public Sprite[] girl;
    public Sprite[] prof;
    public Sprite[] sensei;

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
            case "girlRaw":
                return girlRaw;
            case "profRaw":
                return profRaw;
            case "senseiRaw":
                return senseiRaw;
            case "buff":
                return buff;
            case "hatty":
                return hatty;
            case "girl":
                return girl;
            case "prof":
                return prof;
            case "sensei":
                return sensei;
        }
    }
	
	
}
