using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public Sprite[] buffRawSprites;
    public Sprite[] hattyRawSprites;

    // Want to change this later so each match doesn't re-store characters
    private void Awake()
    {
        Character hattyRaw = new Character("hattyRaw", hattyRawSprites[0], hattyRawSprites[1], hattyRawSprites[2], hattyRawSprites[3], hattyRawSprites[4]);
        Character buffRaw = new Character("buffRaw", buffRawSprites[0], buffRawSprites[1], buffRawSprites[2], buffRawSprites[3], buffRawSprites[4]);
    }

    public List<Character> storedCharacters = new List<Character>();

    public void AddCharacter(Character newEntry)
    {
        storedCharacters.Add(newEntry);
    }

    public Character RetrieveCharacter(string name)
    {
        foreach (Character character in storedCharacters)
        {
            if (character.name == name)
            {
                return character;
            }
        }
        return null;
    }
}

public class Character : MonoBehaviour
{
    public new string name;
    public Sprite forwardFace, sideFace, sideWinning, sideLosing, sideDefeated;



    public Character(string name, Sprite forwardFace, Sprite sideFace, Sprite sideWinning, Sprite sideLosing, Sprite sideDefeated)
    {
        this.name = name;
        this.forwardFace = forwardFace;
        this.sideWinning = sideWinning;
        this.sideLosing = sideLosing;
        this.sideDefeated = sideDefeated;

        GetComponent<Characters>().AddCharacter(this);
    }
}



