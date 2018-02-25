using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLogic : MonoBehaviour
{
    public SpriteEmotes leftSprite, rightSprite;
    public UnityEngine.UI.Text mainText;

    private float defaultLetterWait = 0.03f;
    private LoadBattle loadBattle;
    private SelectedCharacters selectedChars;
    private SelectedScene selectedScene;

    [System.Serializable]
    public class Scenes
    {
        public GameObject beachScene, shrineScene, foyerScene;
    }

    [System.Serializable]
    public class Sprites
    {
        public Sprite buffFront, buffSide, buffWin, buffLose, buffDefeat, girlFront, girlSide, girlWin, girlLose, girlDefeat;
        public Sprite hattyFront, hattySide, hattyWin, hattyLose, hattyDefeat, profFront, profSide, profWin, profLose, profDefeat;
        public Sprite senseiFront, senseiSide, senseiWin, senseiLose, senseiDefeat, rawBuffFront, rawBuffSide, rawBuffWin, rawBuffLose, rawBuffDefeat;
        public Sprite rawGirlFront, rawGirlSide, rawGirlWin, rawGirlLose, rawGirlDefeat, rawHattyFront, rawHattySide, rawHattyWin, rawHattyLose, rawHattyDefeat;
        public Sprite rawProfFront, rawProfSide, rawProfWin, rawProfLose, rawProfDefeat, rawSenseiFront, rawSenseiSide, rawSenseiWin, rawSenseiLose, rawSenseiDefeat;
    }

    public Sprites sprites = new Sprites();
    public Scenes scenes = new Scenes();

    private float defaultFontSize = 26f;
    private bool dialogueDoneDisplaying = true;

    private bool textCrawling = false;
    private bool sectionComplete = true;

    private GameObject passedObject;

    private int globalSceneIndex = 0;

    void Awake()
    {
        loadBattle = GetComponent<LoadBattle>();
        try
        {
            passedObject = GameObject.Find("PassedObject");
            selectedChars = passedObject.GetComponent<SelectedCharacters>();
            selectedScene = passedObject.GetComponent<SelectedScene>();
            globalSceneIndex = passedObject.GetComponent<Passed>().storyIndex;
        }
        catch
        {
            Debug.LogError("Could not find PassedObject. Did you not load from Menu?");
        }
        //globalSceneIndex = 20; // DEBUG
    }

	void Update ()
    {
        if (sectionComplete)
        {
            switch (globalSceneIndex)
            {
                default:
                case 0:
                    rightSprite.ToggleVisibility();
                    scenes.beachScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlSide);
                    leftSprite.EnterStageSide(1f);
                    DisplayDialogue("Ok, I'll be careful. Bye everyone!", defaultLetterWait);
                    break;
                case 1:
                    DisplayDialogue("All my years of training have prepared me for this moment, to take down the 4 volleyblock masters standing in my way!", defaultLetterWait);
                    break;
                case 2:
                    DisplayDialogue("I can't even believe my parents even let me compete in the volleyblock regional champion competition...", defaultLetterWait);
                    break;
                case 3:
                    DisplayDialogue("To think that I need to take on the masters of the sport! The elusive 4 who have been hidden from the public eye.", defaultLetterWait);
                    leftSprite.SetSprite(sprites.girlLose);
                    break;
                case 4:
                    DisplayDialogue("What secrets could they be keeping, what techniques have they mastered?", defaultLetterWait);
                    break;
                case 5:
                    DisplayDialogue("I have to know, I have to learn! I need to be the best block / circle / weird cross stacker there ever was!", defaultLetterWait);
                    leftSprite.SetSprite(sprites.girlWin);
                    break;
                case 6:
                    DisplayDialogue("Alright, the only lead I have is that Buff Beach Bobby is the first master, and he's never lost a match in his life...", defaultLetterWait);
                    break;
                case 7:
                    DisplayDialogue("Mom, Dad, Little Jimmy, I won't let you guys down. Let's go!", defaultLetterWait);
                    leftSprite.SetSprite(sprites.girlSide);
                    break;
                case 8:
                    scenes.beachScene.SetActive(true);
                    rightSprite.ToggleVisibility();
                    leftSprite.ToggleVisibility();
                    leftSprite.SetSprite(sprites.girlSide);
                    rightSprite.SetSprite(sprites.buffWin);
                    rightSprite.EnterStageSide(1f);
                    rightSprite.SetSprite(sprites.buffLose);
                    DisplayDialogue("HRAH, HUT, HRAH, GRAHHHHHHHHHHHHH", 0.1f);
                    break;
                case 9:
                    DisplayDialogue("Whew, what a workout. Volleyblocks totally makes you work, huh?", defaultLetterWait);
                    rightSprite.SetSprite(sprites.buffWin);
                    break;
                case 10:
                    leftSprite.ToggleVisibility();
                    leftSprite.EnterStageSide(1f);
                    DisplayDialogue("BUFF BEACH BOBBY!", defaultLetterWait);
                    break;
                case 11:
                    DisplayDialogue("Huh? Who the heck are you?", defaultLetterWait);
                    rightSprite.SetSprite(sprites.buffSide);
                    break;
                case 12:
                    DisplayDialogue("I challenge you to a duel for your badge! I'm going to prove that I have what it takes to be the volleyblock regional champion!", defaultLetterWait);
                    leftSprite.SetSprite(sprites.girlWin);
                    break;
                case 13:
                    DisplayDialogue(". . .", 0.6f);
                    break;
                case 14:
                    DisplayDialogue("HA HOOOOOOOOOOOO", defaultLetterWait);
                    rightSprite.Shake(1f, 0.2f);
                    rightSprite.SetSprite(sprites.buffWin);
                    leftSprite.SetSprite(sprites.girlLose);
                    break;
                case 15:
                    DisplayDialogue("FINALLY, SOMEONE THINKS THEY'RE TOUGH ENOUGH TO FACE ME?", defaultLetterWait);
                    break;
                case 16:
                    DisplayDialogue("HA HOOOOOOOOOOOO", defaultLetterWait);
                    rightSprite.Shake(1f, 0.2f);
                    break;
                case 17:
                    DisplayDialogue("OKAY KID, I'LL HUMOR YOU. LET'S GO.", defaultLetterWait);
                    rightSprite.SetSprite(sprites.buffSide);
                    leftSprite.SetSprite(sprites.girlSide);
                    break;
                case 18:
                    DisplayDialogue("PROVE TO ME YOU HAVE WHAT IT TAKES TO BE A VOLLEYBLOCK MASTER!", defaultLetterWait);
                    break;
                case 19:
                    selectedChars.SetLeftBoard("girl");
                    selectedChars.SetRightBoard("buff");
                    selectedScene.SetSceneName("beach");
                    passedObject.GetComponent<Passed>().StorePasswords("BUFFDOWN", "BUFFDUDE");
                    loadBattle.LoadNewBattle("Player VS CPU", 5f, 1, 1, globalSceneIndex, true);
                    break;
                case 20:
                    leftSprite.SetSprite(sprites.girlLose);
                    rightSprite.SetSprite(sprites.buffLose);
                    scenes.beachScene.SetActive(true);
                    DisplayDialogue("Whewww... wow..... That was pretty good kid....", defaultLetterWait);
                    break;
                case 21:
                    DisplayDialogue("Wow... outta breath here.... easy there Bobby...", defaultLetterWait);
                    break;
                case 22:
                    DisplayDialogue("Uh, are you going to be ok?", defaultLetterWait);
                    break;
                case 23:
                    rightSprite.SetSprite(sprites.buffWin);
                    DisplayDialogue("YES. WHEW! WOW. What a battle, what a thrill!", defaultLetterWait);
                    break;
                case 24:
                    DisplayDialogue("That isn't something you can get by just training alone. I've been waiting for this day!", defaultLetterWait);
                    break;
                case 25:
                    rightSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("Let me join you on your quest!", defaultLetterWait);
                    break;
                case 26:
                    leftSprite.SetSprite(sprites.girlWin);
                    DisplayDialogue("Huh?", defaultLetterWait);
                    break;
                case 27:
                    DisplayDialogue("Please! I have a feeling that following you will lead me to the strongest warriors and masters of volleyblocks!", defaultLetterWait);
                    break;
                case 28:
                    DisplayDialogue("There's only so much you can learn from protein drinks and long runs on the beach!", defaultLetterWait);
                    break;
                case 29:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("Well, I guess it couldn't hurt. Alright, you can tag along.", defaultLetterWait);
                    break;
                case 30:
                    // Shake
                    rightSprite.SetSprite(sprites.buffWin);
                    rightSprite.Shake(1f, 0.2f);
                    DisplayDialogue("HURRAH!", defaultLetterWait);
                    break;
                case 31:
                    rightSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("Ahem, thank you. Now, considering how elusive us masters are supposed to be, I doubt you thought of who you'd be fighting next?", defaultLetterWait);
                    break;
                case 32:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Well, now that you mention it... no.", defaultLetterWait);
                    break;
                case 33:
                    DisplayDialogue("Every master holds the name of the next. It is our most closely guarded secret.", defaultLetterWait);
                    break;
                case 34:
                    DisplayDialogue("Since I have lost, I guess I have no choice but to reveal this to you.", defaultLetterWait);
                    break;
                case 35:
                    rightSprite.SetSprite(sprites.buffWin);
                    DisplayDialogue("The name of your next opponent is a man by the name of... Sir Kensington!", defaultLetterWait);
                    break; // Fade / new scene / exit?
                case 36:
                    rightSprite.Shake(1f, 0.2f);
                    DisplayDialogue("LET'S GO! I WANT TO SEE A GOOD PACE ON OUR WAY THERE!", defaultLetterWait);
                    rightSprite.ExitStageSide(1f, false);
                    break;
                case 37:
                    leftSprite.Shake(1f, 0.2f);
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("EHHHHHH?!", defaultLetterWait);
                    leftSprite.ExitStageOppositeSide(1f);
                    break;
                case 38:
                    scenes.beachScene.SetActive(false);
                    scenes.foyerScene.SetActive(true);
                    leftSprite.EnterStageSide(1f);
                    rightSprite.EnterStageSide(1f);
                    rightSprite.SetSprite(null);
                    leftSprite.SetSprite(sprites.girlWin);
                    DisplayDialogue("Thank you very much. We'll wait here.", defaultLetterWait);
                    break;
                case 39:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("Wow, he actually has a butler? Who even has a butler nowadays?", defaultLetterWait);
                    break;
                case 40:
                    leftSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("I should get me one of those. Could bring me a clean towel after my morning swims...", defaultLetterWait);
                    break;
                case 41:
                    DisplayDialogue("AH, THAT IS THE FORMIDABLE FOE WHO STANDS BEFORE ME?", defaultLetterWait);
                    break;
                case 42:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Huh?", defaultLetterWait);
                    break;
                case 43:
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("HO HO. WELCOME TO MY HUMBLE ABODE!", defaultLetterWait);
                    break;
                case 44:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("The name's Kensington, but that's Sir Kensington to you!", defaultLetterWait);
                    break;
                case 45:
                    DisplayDialogue("That is, of course, if you can't prove your mettle, child!", defaultLetterWait);
                    break;
                case 46:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("Do you take me for a volleyblock chump?", defaultLetterWait);
                    break;
                case 47:
                    DisplayDialogue("While looks can be deceiving, they can also reveal deep truths!", defaultLetterWait);
                    break;
                case 48:
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("I have no doubt that this will be an entertaining match, but it will be a match you shall lose!", defaultLetterWait);
                    break; 
                case 49: // BATTLE
                    selectedChars.SetLeftBoard("girl");
                    selectedChars.SetRightBoard("hatty");
                    selectedScene.SetSceneName("foyer");
                    passedObject.GetComponent<Passed>().StorePasswords("RICHDOWN", "RICHDUDE");
                    loadBattle.LoadNewBattle("Player VS CPU", 5f, 1, 1, globalSceneIndex, true);
                    break;
                case 50:
                    leftSprite.SetSprite(sprites.girlSide);
                    rightSprite.SetSprite(sprites.hattyLose);
                    scenes.foyerScene.SetActive(true);
                    DisplayDialogue("How exquisite!", defaultLetterWait);
                    break;
                case 51:
                    leftSprite.Shake(1f, 0.2f);
                    DisplayDialogue("HYAH!", defaultLetterWait);
                    break;
                case 52:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("How divine!", defaultLetterWait);
                    break;
                case 53:
                    leftSprite.SetSprite(sprites.girlWin);
                    leftSprite.Shake(1f, 0.2f);
                    DisplayDialogue("HRAH!", defaultLetterWait);
                    break;
                case 54:
                    rightSprite.SetSprite(sprites.hattyDefeat);
                    DisplayDialogue("I GIVE!", defaultLetterWait);
                    break;
                case 55:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("What an excellent performance miss. I'm truly taken aback.", defaultLetterWait);
                    break;
                case 56:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("Why thank you. You weren't too bad yourself.", defaultLetterWait);
                    break;
                case 57:
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("Your words humble me, but I lack in many areas. Being brought up with wealth as my ally has made me worse for the wear.", defaultLetterWait);
                    break;
                case 58:
                    rightSprite.SetSprite(sprites.hattyDefeat);
                    DisplayDialogue("I've been coddled in these niceities for far too long...", defaultLetterWait);
                    break;
                case 59:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("But look at you! Clawing your way from the bottom to achieve your goals! Truly inspiring.", defaultLetterWait);
                    break;
                case 60:
                    leftSprite.SetSprite(sprites.girlWin);
                    DisplayDialogue("Well, I guess you could put it like that if you really wanted to...", defaultLetterWait);
                    break;
                case 61:
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("And I shall! Now, let me accompany you on your quest!", defaultLetterWait);
                    break;
                case 62:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Wait what.", defaultLetterWait);
                    break;
                case 63:
                    DisplayDialogue("You think you can make it out on the road?", defaultLetterWait);
                    break;
                case 64:
                    leftSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("I'm pretty well trained and even I'm experiencing some blisters from all this walking!", defaultLetterWait);
                    break;
                case 65:
                    DisplayDialogue("Nonsense companion. No, I want to push my very self to the limits as I lead you to your next opponent.", defaultLetterWait);
                    break;
                case 66:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("And who might that be?", defaultLetterWait);
                    break;
                case 67:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("Your next opponent should not be taken lightly.", defaultLetterWait);
                    break;
                case 68:
                    DisplayDialogue("She's the maiden of science, the dynamic discoverer...", defaultLetterWait);
                    break;
                case 69:
                    rightSprite.SetSprite(sprites.hattyFront);
                    DisplayDialogue("The one and only... Professor Sylvia!", defaultLetterWait);
                    break;
            }
        }
        if (!textCrawling)
        {
            if (Input.GetButtonDown("Submit"))
            {
                sectionComplete = true;
                globalSceneIndex++;
            }
        }
        else
        {
            if (Input.GetButtonDown("Submit"))
            {
                StopAllCoroutines();
                mainText.text = desiredText;
                textCrawling = false;
            }
        }
    }

    private string currentText;
    private string desiredText;

    void DisplayDialogue(string text, float letterWaitTime)
    {
        desiredText = text;
        sectionComplete = false;
        textCrawling = true;
        currentText = "";
        StartCoroutine(SlowAddText(letterWaitTime, -1, text));
    }

    IEnumerator SlowAddText(float time, int index, string text)
    {
        index++;
        yield return new WaitForSeconds(time);
        if (index < text.Length)
        {
            currentText += text[index];
            mainText.text = currentText;
            StartCoroutine(SlowAddText(time, index, text));
        }
        else
        {
            textCrawling = false;
            //globalSceneIndex++;
        }
    }
}
