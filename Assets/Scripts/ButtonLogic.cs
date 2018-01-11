using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour {

    private string players = "Player VS CPU";
    private string cpuDifficulty = "Easy";
    private float turnLength = 0.3f;

    private int gamesCount = 3;
    private int setsCount = 3;

    public UnityEngine.UI.Text playersText, cpuText, gamesText, setsText;

    public void StartButton()
    {
        // Need to pass AI params here
        GetComponent<LoadBattle>().LoadNewBattle(players, turnLength, gamesCount, setsCount);
    }

    public void PlayersButton()
    {
        switch (players)
        {
            case "Player VS CPU":
                players = "Player VS Player";
                break;
            case "Player VS Player":
                players = "CPU VS CPU";
                break;
            case "CPU VS CPU":
            default:
                players = "Player VS CPU";
                break;
        }
        playersText.text = players;
    }

    public void DifficultyButton()
    {
        switch (cpuDifficulty)
        {
            case "Easy":
                cpuDifficulty = "Medium";
                turnLength = 0.2f;
                break;
            case "Medium":
                cpuDifficulty = "Hard";
                turnLength = 0.1f;
                break;
            case "Hard":
                cpuDifficulty = "Impossible";
                turnLength = 0.0f;
                break;
            case "Impossible":
            default:
                cpuDifficulty = "Easy";
                turnLength = 0.3f;
                break;
        }
        cpuText.text = cpuDifficulty;
    }

    public void GamesButton()
    {
        gamesCount++;
        if (gamesCount > 15)
        {
            gamesCount = 1;
        }

        gamesText.text = "Games: First to " + gamesCount;
    }

    public void SetsButton()
    {
        setsCount++;
        if (setsCount > 5)
        {
            setsCount = 1;
        }

        setsText.text = "Sets: First to " + setsCount;
    }
}
