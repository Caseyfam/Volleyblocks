using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedAI : MonoBehaviour {

    public static PassedAI instance;

    public bool useOldAI = false;
    public float turnLength = 0f;
    public string playerWon = "";
    // Change playerWon to a 3 state
    // NPC will react if null, if true, and if false

    public void StoreValues (bool use, float turn)
    {
        useOldAI = use;
        turnLength = turn;
    }

	// Use this for initialization
	void Awake ()
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
