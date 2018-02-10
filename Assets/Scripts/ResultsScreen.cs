using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreen : MonoBehaviour {

    public GameObject resultsGraphics, resultsCanvas, password;

    public SpriteRenderer winnerSprite;
    public UnityEngine.UI.Text gloatText;
    bool isStory = false;

    BoardsInPlay boardsInPlay;

    void Awake()
    {
        boardsInPlay = GetComponent<BoardsInPlay>();
    }

	public void ResultsSetup(string playerWon, bool isStory)
    {
        this.isStory = isStory;
        string winningBoardName = "";

        GameObject passedObject;
        try
        {
            passedObject = GameObject.Find("PassedObject");
        }
        catch
        {
            passedObject = null;
        }

        if (playerWon.Equals("won"))
        {
            winnerSprite.sprite = boardsInPlay.leftBoard.sideWin;
            if (passedObject != null)
            {
                winningBoardName = passedObject.GetComponent<SelectedCharacters>().leftBoardName;
            }
        }
        else
        {
            winnerSprite.sprite = boardsInPlay.rightBoard.sideWin;
            if (passedObject != null)
            {
                winningBoardName = passedObject.GetComponent<SelectedCharacters>().rightBoardName;
            }
        }
        if (passedObject == null)
        {
            winningBoardName = "debug";
        }

        string winningDialogue = "";
        switch (winningBoardName)
        {
            case "buffRaw":
                winningDialogue = "A̴̳̼̞͈̺̝͔̦͓̝̣̫̩̝͑̋̀̌͋͛̊̓̈́͜ ̶̠͖́̉̈́N̷̹̄̇̉̑͗͆̽̇͌͠͠ ̸̨̧̛̩̱̳̺̥̀̃́̂̒͋̾̐̎́̋ͅĢ̸̡͔͍̱̪͎̱̥̦̣̫̘͒̂͗̑̾͒̍̚ ̸̢̢͔̙̥͈̦̪͓͓̹͔͚͑̇̃̋̀̏̃̀́͌͒̕͠ͅE̸̼̠͍̜̩̙͙̫͍̝̮͌̒̀̏͑̌̈́ ̵͉͙͕̪̻̣̫̰̐̃͋̔̈́͋̓̂̐̀̂͠R̷̨̭̠̬̫̺͆̊̌͂̈́̕͜";
                break;
            case "hattyRaw":
                winningDialogue = "R̶̞̭̰̰̻̘̯̘̂̅̿ ̶̢̝̰͚̺̣̹̭͊̓̓͆̂͘͘͘͜Ḙ̴̡̘̝̟̫̼̟̰̼̻͋̂̆͊̐͒͐̈́͒̋͗̍̎̕͘ͅͅ ̵̛͙́̓̎̉̉̈́̍́Ģ̴̻̪̯̘͍̫͕͙̩͐̓̀̌̈̈͝ ̵̙͆͂̑̐̿̍͒R̸̜̖̯͇̥̱̩̻̉̍̊͐̋͊ ̸̢͖̲̫̖͚̖̬̠͊͐̿̔̓̆̈́͌̆̕É̵̛͉̱̤͎̘̜̙͖͆͊̚͜ ̶̨̭̗̻̗̠͇̳̝̲̟̔̔̅̈́͝ͅŢ̶̧͗͛͐̇̅̀̀̆̽̍̓̿͂̔̇";
                break;
            case "girlRaw":
                winningDialogue = "Ȩ̵̜̩̪͚͎͌̍̀̒͜ͅ ̴̖͔̤̍̋̄̌͌̍̏̊̉̕̕M̶̢͉͕͚͕̪̭̫̻̥̈́̂͌̀̃̏͗̆̋̓̀͜ ̸̨̨̟̥̤̩͎͉̩̼̹̰̱͑͑̅P̵̢̱̻̈́̈́̿͑͛͌̿͆̆̾̐̕̕ ̵̲͊T̵̖̥̦̞͖͎̈́͋̈̑́̎̋͐̿́̊͘͠͠ ̷̛͈͚̼͕͕̟͕̇́̓̈́̽̽Ì̷̢̢̻̮͍̭̰̯͚̮́̑̈̒͂̈́̆̓ͅ ̸̨̢̰̪̮̤͇̟̝̗̼̥̘̖̇͜N̷̢̬̫͚̱̟̬̘̳̘̲͌̐̍̑̐̑̀̅̕̚̕͠ ̶͙̥̺̬̺̓̌̎̐̏̓͑͂̚̕͜Ė̵̤͙̈́ ̵̛̠̰̞̤͉̝͕̜̜͉̘̾̎̾́͑̊͗̓̾̌̈́͘Ş̶͎͍̟̦̠̻̇̆̒̒ ̷̧̨̡̭̫͉̥͇̜̻̰̜͙͑̍̓̀̐͆͗́̋̍̅͐͜͠͝S̸̢̡̛͓̘̜̬̮̣̥͇͍̗͔̳͑̄̃̾͗̌̋̋͆͗͒̀̏͠";
                break;
            case "profRaw":
                winningDialogue = "F̸̼͓̥̋̈́͊̽̑̎̀͜ ̸̬͇̞͊̇͋̎͋̎̉͘͜͝E̷̢̞̳̞̼̮͖̣̝͉͐̍̓̆͜ ̸̦͇̠̼̻̂͑͂͆́̕͜A̴͈̩͉̤̝̪̩̞̠͆̏̃̀́̄́̓́̐̍͝ ̴̢̬̹̪̣̮̞̠̘̖̈́̇͐̄R̵̡̧͖͚̬̝̣͓̙̺̬̠̺̥̄̉͒́̓̎͒͆́͘͠͝";
                break;
            case "senseiRaw":
                winningDialogue = "S̴̱̲̪̹̮̃̆͛̈́̄̃̇́̕͠ ̴̯͎̱̭̩̠̬̜̜̬̟̲̬̮͆̆͑̆͜Ą̵̰̱̗̆ ̴̮̣̳͌̓̇͆̔̍̀̉̀̍̉̍͒̚̕D̷͍̊͊͂̀̓̔͗͘͝ ̷͎͎̠̘͖̞̥̦̝͎̤̮̘́͑̍̓̔͘͜ͅÑ̵̢̠̝̮̎̏̋́̆͐͐̆̃̐̿͝͠͝ ̵̼̖̞̩̈́̍E̶̘̙͈̙̣̔̌̒̋͒̈́̀̒̋͐̃̑̈́͠͠ ̸̧̡̟̝̻̝̪͙̜̼̗̹̱͐̈́́̈́̀͐͂̒̾͒͊͜ͅŞ̵̘̲̒̽ ̸̡̭͔̺͔͔̠̼̘̖͈͚͕̗͆ͅŞ̶̧̘͉͖̬̼̝̘̦̻̤̰̙̅̿";
                break;
            case "buff":
                winningDialogue = "You call that a match? I didn't even break a sweat!";
                break;
            case "hatty":
                winningDialogue = "You disappoint me. I thought we were going to have a ball!";
                break;
            case "girl":
                winningDialogue = "Nothing will stop me from becoming the Volleyblock champion! Not even you!";
                break;
            case "prof":
                winningDialogue = "It would seem my hypothesis was correct. Your failure was inevitable.";
                break;
            case "sensei":
                winningDialogue = "I can sense your anger. Patience and practice shall lead you to great rewards.";
                break;
            case "debug":
            default:
                winningDialogue = "This is DEBUG dialogue because PassedObject does not exist.";
                break;
        }
        gloatText.text = winningDialogue;
        resultsCanvas.SetActive(true);
        resultsGraphics.SetActive(true);
        if (!isStory)
        {
            password.SetActive(false);
        }
    }
}
