using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreen : MonoBehaviour {

    public GameObject resultsGraphics, textBacking, storyElements, password, continueButton, retryButton;

    public SpriteRenderer winnerSprite;
    public UnityEngine.UI.Text gloatText;
    bool isStory = false;

    private string winPassword, losePassword;

    BoardsInPlay boardsInPlay;

    void Awake()
    {
        boardsInPlay = GetComponent<BoardsInPlay>();
    }
    public UnityEngine.EventSystems.EventSystem eventSystem;
	public void ResultsSetup(string playerWon, bool isStory)
    {

        this.isStory = isStory;
        string winningBoardName = "";

        GameObject passedObject;
        try
        {
            passedObject = GameObject.Find("PassedObject");
            winPassword = passedObject.GetComponent<Passed>().winPassword;
            losePassword = passedObject.GetComponent<Passed>().losePassword;
        }
        catch
        {
            passedObject = null;
            winPassword = "DEBUGWIN";
            losePassword = "DEBUGLOSE";
        }
        bool cpuLost = false;
        if (playerWon.Equals("won"))
        {
            if (!isStory)
            {
                winnerSprite.sprite = boardsInPlay.leftBoard.sideWin;
                if (passedObject != null)
                {
                    winningBoardName = passedObject.GetComponent<SelectedCharacters>().leftBoardName;
                }
            }
            else
            {
                winnerSprite.sprite = boardsInPlay.rightBoard.sideDefeat;
                if (passedObject != null)
                {
                    winningBoardName = passedObject.GetComponent<SelectedCharacters>().rightBoardName;
                    cpuLost = true;
                }
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
        if (!cpuLost)
        {
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
        }
        else
        {
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
                    winningDialogue = "WHEW.... GAH.... I need a break....";
                    break;
                case "hatty":
                    winningDialogue = "Egad! My streak is ruined!";
                    break;
                case "girl":
                    winningDialogue = "But... how...?";
                    break;
                case "prof":
                    winningDialogue = "This is wrong! How could my calculations be so imprecise!";
                    break;
                case "sensei":
                    winningDialogue = "Hm. The student surpasses the master.";
                    break;
                case "debug":
                default:
                    winningDialogue = "This is DEBUG dialogue because PassedObject does not exist.";
                    break;
            }
        }
       
        gloatText.text = winningDialogue;
        textBacking.SetActive(true);
        storyElements.SetActive(true);
        passedObject.SetActive(true);
        resultsGraphics.SetActive(true);
        if (!isStory)
        {
            password.SetActive(false);
            continueButton.SetActive(false);
            retryButton.SetActive(true);

            eventSystem.SetSelectedGameObject(retryButton);
            retryButton.GetComponent<UnityEngine.UI.Button>().OnSelect(null);
        }
        else
        {
            eventSystem.SetSelectedGameObject(continueButton);
            continueButton.GetComponent<UnityEngine.UI.Button>().OnSelect(null);

            if (playerWon.Equals("won"))
            {
                continueButton.SetActive(true);
                retryButton.SetActive(false);
                password.GetComponentInChildren<UnityEngine.UI.Text>().text = "Password: " + winPassword;
            }
            else
            {
                retryButton.SetActive(true);
                continueButton.SetActive(false);
                password.GetComponentInChildren<UnityEngine.UI.Text>().text = "Password: " + losePassword;
            }
            password.SetActive(true);
        }
    }
}
