using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public Sprite[] buffRawSprites;
    public Sprite[] hattyRawSprites;
    public List<Character> storedCharacters;

    // Want to change this later so each match doesn't re-store characters
    public void Awake()
    {
        storedCharacters = new List<Character>();
        Character hattyRaw = new Character("hattyRaw", hattyRawSprites[0], hattyRawSprites[1], hattyRawSprites[2], hattyRawSprites[3], hattyRawSprites[4]);
        AddCharacter(hattyRaw);
    }

    public void AddCharacter(Character newEntry)
    {
        storedCharacters.Add(newEntry);
    }

    public Character RetrieveCharacter(string name)
    {
        bool found = false;
        Character returnChar = new Character();
        foreach (Character character in storedCharacters)
        {
            if (character.name.Equals(name))
            {
                returnChar = character;
                found = true;
            }
        }
        if (found)
        {
            return returnChar;
        }
        else
        {
            Debug.LogError("Null Character returned");
            return null;
        }
    }
}
