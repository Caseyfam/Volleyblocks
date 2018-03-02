using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passed : MonoBehaviour {

    public static Passed instance;

    public string playersInPlay = "Player VS CPU";
    public float turnLength = 0f;
    public int setCount = 3;
    public int gamesCount = 3;
    public int storyIndex = 0;
    public bool isStory = false;
    public bool mainMenuCutsceneSkipped = false;
    public string winPassword, losePassword;

    public void StoreValues (string players, float turn, int games, int sets)
    {
        playersInPlay = players;
        turnLength = turn;
        gamesCount = games;
        setCount = sets;
    }

    public void StoreValues(string players, float turn, int games, int sets, int index)
    {
        StoreValues(players, turn, games, sets);
        storyIndex = index;
    }

    public void StoreValues(string players, float turn, int games, int sets, int index, bool isStory)
    {
        StoreValues(players, turn, games, sets, index);
        this.isStory = isStory;
    }

    public void StorePasswords(string winPassword, string losePassword)
    {
        this.winPassword = winPassword;
        this.losePassword = losePassword;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }
}
