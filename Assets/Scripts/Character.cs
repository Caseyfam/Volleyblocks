using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string name;
    public Sprite forwardFace, sideFace, sideWinning, sideLosing, sideDefeated;

    public Character()
    {
        name = "";
        forwardFace = null;
        sideWinning = null;
        sideLosing = null;
        sideDefeated = null;
    }

    public Character (string name, Sprite forwardFace, Sprite sideFace, Sprite sideWinning, Sprite sideLosing, Sprite sideDefeated)
    {
        this.name = name;
        this.forwardFace = forwardFace;
        this.sideWinning = sideWinning;
        this.sideLosing = sideLosing;
        this.sideDefeated = sideDefeated;
    }
}
