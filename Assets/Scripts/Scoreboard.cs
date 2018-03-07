using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private int leftGames = 0, rightGames = 0;
    private int leftSets = 0, rightSets = 0;

    public SpriteRenderer srLeftGames, srRightGames, srLeftSets, srRightSets;

    public Sprite[] numbers;

    private const float sizeMultiplier = 1.2f;

    public void SetLeftGames(int val)
    {
        leftGames = val;
        UpdateScoreboard(val, srLeftGames);
    }
    public void SetRightGames(int val)
    {
        rightGames = val;
        UpdateScoreboard(val, srRightGames);
    }
    public void SetLeftSets(int val)
    {
        leftSets = val;
        UpdateScoreboard(val, srLeftSets);
    }
    public void SetRightSets(int val)
    {
        rightSets = val;
        UpdateScoreboard(val, srRightSets);
    }

    void UpdateScoreboard(int val, SpriteRenderer desiredSR)
    {
        // Can only have 0-9
        try
        {
            desiredSR.sprite = numbers[val];
            desiredSR.gameObject.GetComponent<ScoreboardLetterEffect>().StartGrowing(1.2f, 0.05f);
        }
        catch
        {
            Debug.LogError("You've accessed a number in Scoreboard.cs outside the range 0 to 9.");
        }
    }

}
