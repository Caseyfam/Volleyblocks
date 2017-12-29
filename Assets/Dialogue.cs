using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    UnityEngine.UI.Text testText;

    private string uniqueIdentifier = "NPC1";

    public bool loadsIntoBattle = false;
    public string[] entryLines;
    public string[] wonLines;
    public string[] lostLines;
    public string[] defeatedLines;
    private string[] lines;

    private bool defeated = false;
    
    private bool checkForInput = false;
    private int lineIndex = 0;

    GameObject player;

    private void Start()
    {
        defeated = GameObject.Find("PassedObject").GetComponent<PassedDefeatedAI>().GetDefeatedVal(uniqueIdentifier);
        UpdateDialogue();
    }

    private void UpdateDialogue()
    {
        testText = GameObject.Find("Canvas").GetComponentInChildren<UnityEngine.UI.Text>();
        testText.text = "";

        if (defeated)
        {
            lines = defeatedLines;
        }
        else
        {
            if (GameObject.Find("PassedObject").GetComponent<PassedAI>().playerWon.Equals("lost"))
            {
                lines = lostLines;
            }
            else if (GameObject.Find("PassedObject").GetComponent<PassedAI>().playerWon.Equals("won"))
            {
                lines = wonLines;
                if (!defeated)
                {
                    GameObject.Find("PassedObject").GetComponent<PassedDefeatedAI>().StoreDefeated(uniqueIdentifier);
                }
                defeated = true;
            }
            else
            {
                lines = entryLines;
            }
        }

        //StartDialogue();
    }

    private void Update()
    {
        if (checkForInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (lineIndex != lines.Length - 1)
                {
                    lineIndex++;
                    testText.text = lines[lineIndex];
                }
                else
                {
                    lineIndex = -1;
                    testText.text = "";
                    checkForInput = false;
                    if (!defeated && loadsIntoBattle && GameObject.Find("PassedObject").GetComponent<PassedAI>().playerWon == "not set")
                    {
                        GetComponent<LoadBattle>().LoadNewBattle(player);
                    }
                    else
                    {
                        // Make player able to move
                        // Add state where NPC is always defeated now and
                        // won't instigate an attack
                        // Reset playerWon variable to be something like "fully defeated"

                        player.GetComponent<OverworldMovement>().canMove = true;
                        GameObject.Find("PassedObject").GetComponent<PassedAI>().playerWon = "not set";
                        GameObject.Find("PassedObject").GetComponent<PassedOverworld>().Clear();
                    }
                }
            }
        }
    }

    public void StartDialogue()
    {
        testText.text = lines[0];
        lineIndex = 0;
        checkForInput = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.tag.Equals("Player"))
        {
            UpdateDialogue();
            other.GetComponent<OverworldMovement>().canMove = false;
            player = other.gameObject;
            GetComponent<Dialogue>().StartDialogue();
        }
    }
}
