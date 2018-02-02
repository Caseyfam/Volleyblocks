using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectLogic : MonoBehaviour {

    SelectedCharacters charStorage;
    GameObject menu;
    public GameObject p1Arrow, p2Arrow;

    private int p1Selection, p2Selection;

    private void Awake()
    {
        menu = gameObject;
        charStorage = GameObject.Find("PassedObject").GetComponent<SelectedCharacters>();  
    }
	
	void Update ()
    {
	    if (menu.activeSelf)
        {
            MoveCursor(p1Arrow, ref p1Selection);
            // If player vs CPU -> One cursor
            // If player vs player -> Two cursors
            // If CPU Vs CPU -> skip character select and load game
            // but first set up random portraits
        }
	}

    void MoveCursor(GameObject arrow, ref int selection)
    {
        // Get right inputs before so we can differentiate players
        if (Input.GetKeyDown(KeyCode.D))
        {
            selection++;
            arrow.transform.position = new Vector3(arrow.transform.position.x + 3.5f, arrow.transform.position.y, 0f);
            if (selection >= 5)
            {
                selection = 0;
                arrow.transform.position = new Vector3(-7f, arrow.transform.position.y, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            selection--;
            arrow.transform.position = new Vector3(arrow.transform.position.x - 3.5f, arrow.transform.position.y, 0f);
            if (selection < 0)
            {
                selection = 4;
                arrow.transform.position = new Vector3(7f, arrow.transform.position.y, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetCharacters(); // Just for testing
        }
    }

    void SetCharacters()
    {
        string p1Name;
        string p2Name;
        switch (p1Selection)
        {
            default:
            case 0:
                p1Name = "raw";
                break;
            case 1:
                p1Name = "girl";
                break;
            case 2:
                p1Name = "hatty";
                break;
            case 3:
                p1Name = "prof";
                break;
            case 4:
                p1Name = "sensei";
                break;
        }
        switch (p2Selection)
        {
            default:
            case 0:
                p2Name = "buff";
                break;
            case 1:
                p2Name = "girl";
                break;
            case 2:
                p2Name = "hatty";
                break;
            case 3:
                p2Name = "prof";
                break;
            case 4:
                p2Name = "sensei";
                break;
        }
        charStorage.SetLeftBoard(p1Name);
        charStorage.SetRightBoard(p2Name);

    }
    // Need to add a new "start" button to character select screen that calls
    // button logic and does the same similar stuff as "start" used to do before
    // going to the character select screen
}
