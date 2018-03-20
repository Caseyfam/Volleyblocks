using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLogic : MonoBehaviour
{
    public SpriteEmotes leftSprite, rightSprite;
    public UnityEngine.UI.Text mainText;
    public FadeSmart fadeSmart;
    public Camera localCam;

    private float defaultLetterWait = 0.03f;
    private LoadBattle loadBattle;
    private SelectedCharacters selectedChars;
    private SelectedScene selectedScene;


    private Color32 girlText = new Color(149f / 255, 0f, 0f);
    private Color32 buffText = new Color(65f / 255, 54f / 255, 203f / 255);
    private Color32 hattyText = new Color(92f / 255, 92f / 255, 92f / 255);
    private Color32 profText = new Color(110f / 255, 86f / 255, 186f / 255);
    private Color32 senseiText = new Color(158f / 255, 133f / 255, 66f / 255);

    [System.Serializable]
    public class Scenes
    {
        public GameObject beachScene, shrineScene, foyerScene, labScene, voidScene;
    }

    [System.Serializable]
    public class Sprites
    {
        public Sprite buffFront, buffSide, buffWin, buffLose, buffDefeat, girlFront, girlSide, girlWin, girlLose, girlDefeat;
        public Sprite hattyFront, hattySide, hattyWin, hattyLose, hattyDefeat, profFront, profSide, profWin, profLose, profDefeat;
        public Sprite senseiFront, senseiSide, senseiWin, senseiLose, senseiDefeat, rawBuffFront, rawBuffSide, rawBuffWin, rawBuffLose, rawBuffDefeat;
        public Sprite rawGirlFront, rawGirlSide, rawGirlWin, rawGirlLose, rawGirlDefeat, rawHattyFront, rawHattySide, rawHattyWin, rawHattyLose, rawHattyDefeat;
        public Sprite rawProfFront, rawProfSide, rawProfWin, rawProfLose, rawProfDefeat, rawSenseiFront, rawSenseiSide, rawSenseiWin, rawSenseiLose, rawSenseiDefeat;
        public Sprite meFrontRaw, meFrontColor, meSideRaw, meSideColor, meGlitchRaw, meGlitchColor;
    }

    public Sprites sprites = new Sprites();
    public Scenes scenes = new Scenes();

    private float defaultFontSize = 26f;
    private bool dialogueDoneDisplaying = true;

    private bool textCrawling = false;
    private bool sectionComplete = true;
    private bool canAdvanceText = true;

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
        if (sectionComplete && canAdvanceText)
        {
            switch (globalSceneIndex)
            {
                default:
                case 0:
                    rightSprite.ToggleVisibility();
                    scenes.beachScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlSide);
                    leftSprite.EnterStageSide(1f);
                    DisplayDialogue("Ok, I'll be careful. Bye everyone!", defaultLetterWait, girlText);
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
                    DisplayDialogue("Alright, the only lead I have is that Beach Buff Bobby is the first master, and he's never lost a match in his life...", defaultLetterWait);
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
                    DisplayDialogue("HRAH, HUT, HRAH, GRAHHHHHHHHHHHHH", 0.1f, buffText);
                    break;
                case 9:
                    DisplayDialogue("Whew, what a workout. Volleyblocks totally makes you work, huh?", defaultLetterWait);
                    rightSprite.SetSprite(sprites.buffWin);
                    break;
                case 10:
                    leftSprite.ToggleVisibility();
                    leftSprite.EnterStageSide(1f);
                    DisplayDialogue("BEACH BUFF BOBBY!", defaultLetterWait, girlText);
                    break;
                case 11:
                    DisplayDialogue("Huh? Who the heck are you?", defaultLetterWait, buffText);
                    rightSprite.SetSprite(sprites.buffSide);
                    break;
                case 12:
                    DisplayDialogue("I challenge you to a duel for your badge! I'm going to prove that I have what it takes to be the volleyblock regional champion!", defaultLetterWait, girlText);
                    leftSprite.SetSprite(sprites.girlWin);
                    break;
                case 13:
                    DisplayDialogue(". . .", 0.6f, buffText);
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
                    scenes.beachScene.SetActive(true);
                    break;
                case 18:
                    DisplayDialogue("PROVE TO ME YOU HAVE WHAT IT TAKES TO BE A VOLLEYBLOCK MASTER!", defaultLetterWait);
                    break;
                case 19: // BATTLE 1
                    StartCoroutine(FadeIntoScene(1f, "girl", "buff", "beach", "BUFFDOWN", "BUFFDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 20:
                    leftSprite.SetSprite(sprites.girlLose);
                    rightSprite.SetSprite(sprites.buffLose);
                    scenes.beachScene.SetActive(true);
                    DisplayDialogue("Whewww... wow..... That was pretty good kid....", defaultLetterWait, buffText);
                    break;
                case 21:
                    DisplayDialogue("Wow... outta breath here.... easy there Bobby...", defaultLetterWait);
                    break;
                case 22:
                    DisplayDialogue("Uh, are you going to be ok?", defaultLetterWait, girlText);
                    break;
                case 23:
                    rightSprite.SetSprite(sprites.buffWin);
                    DisplayDialogue("YES. WHEW! WOW. What a battle, what a thrill!", defaultLetterWait, buffText);
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
                    DisplayDialogue("Huh?", defaultLetterWait, girlText);
                    break;
                case 27:
                    DisplayDialogue("Please! I have a feeling that following you will lead me to the strongest warriors and masters of volleyblocks!", defaultLetterWait, buffText);
                    break;
                case 28:
                    DisplayDialogue("There's only so much you can learn from protein drinks and long runs on the beach!", defaultLetterWait);
                    break;
                case 29:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("Well, I guess it couldn't hurt. Alright, you can tag along.", defaultLetterWait, girlText);
                    break;
                case 30:
                    // Shake
                    rightSprite.SetSprite(sprites.buffWin);
                    rightSprite.Shake(1f, 0.2f);
                    DisplayDialogue("HURRAH!", defaultLetterWait, buffText);
                    break;
                case 31:
                    rightSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("Ahem, thank you. Now, considering how elusive us masters are supposed to be, I doubt you thought of who you'd be fighting next?", defaultLetterWait);
                    break;
                case 32:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Well, now that you mention it... no.", defaultLetterWait, girlText);
                    break;
                case 33:
                    DisplayDialogue("Every master holds the name of the next. It is our most closely guarded secret.", defaultLetterWait, buffText);
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
                    DisplayDialogue("EHHHHHH?!", defaultLetterWait, girlText);
                    leftSprite.ExitStageOppositeSide(1f);
                    break;
                case 38:
                    scenes.beachScene.SetActive(false);
                    scenes.foyerScene.SetActive(true);
                    leftSprite.EnterStageSide(1f);
                    rightSprite.EnterStageSide(1f);
                    rightSprite.SetSprite(null);
                    leftSprite.SetSprite(sprites.girlWin);
                    DisplayDialogue("Thank you very much. We'll wait here.", defaultLetterWait, girlText);
                    break;
                case 39:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("Wow, he actually has a butler? Who even has a butler nowadays?", defaultLetterWait);
                    break;
                case 40:
                    leftSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("I should get me one of those. Could bring me a clean towel after my morning swims...", defaultLetterWait, buffText);
                    break;
                case 41:
                    DisplayDialogue("AH, THAT IS THE FORMIDABLE FOE WHO STANDS BEFORE ME?", defaultLetterWait, hattyText);
                    break;
                case 42:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Huh?", defaultLetterWait, girlText);
                    break;
                case 43:
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("HO HO. WELCOME TO MY HUMBLE ABODE!", defaultLetterWait, hattyText);
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
                    DisplayDialogue("Do you take me for a volleyblock chump?", defaultLetterWait, girlText);
                    break;
                case 47:
                    DisplayDialogue("While looks can be deceiving, they can also reveal deep truths!", defaultLetterWait, hattyText);
                    break;
                case 48:
                    scenes.foyerScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlSide);
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("I have no doubt that this will be an entertaining match, but it will be a match you shall lose!", defaultLetterWait);
                    break; 
                case 49: // BATTLE
                    //IEnumerator FadeIntoScene(float time, string leftBoardChar, string rightBoardChar, string sceneName, string winPassword, string losePassword, string playerCase, float newTurnLength, int gamesCount, int setsCount, int storyIndex, bool isStory)
                    StartCoroutine(FadeIntoScene(1f, "girl", "hatty", "foyer", "RICHDOWN", "RICHDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 50:
                    leftSprite.SetSprite(sprites.girlSide);
                    rightSprite.SetSprite(sprites.hattyLose);
                    scenes.foyerScene.SetActive(true);
                    DisplayDialogue("How exquisite!", defaultLetterWait, hattyText);
                    break;
                case 51:
                    leftSprite.Shake(1f, 0.2f);
                    DisplayDialogue("HYAH!", defaultLetterWait, girlText);
                    break;
                case 52:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("How divine!", defaultLetterWait, hattyText);
                    break;
                case 53:
                    leftSprite.SetSprite(sprites.girlWin);
                    leftSprite.Shake(1f, 0.2f);
                    DisplayDialogue("HRAH!", defaultLetterWait, girlText);
                    break;
                case 54:
                    rightSprite.SetSprite(sprites.hattyDefeat);
                    DisplayDialogue("I GIVE!", defaultLetterWait, hattyText);
                    break;
                case 55:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("What an excellent performance miss. I'm truly taken aback.", defaultLetterWait);
                    break;
                case 56:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("Why thank you. You weren't too bad yourself.", defaultLetterWait, girlText);
                    break;
                case 57:
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("Your words humble me, but I lack in many areas. Being brought up with wealth as my ally has made me worse for the wear.", defaultLetterWait, hattyText);
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
                    DisplayDialogue("Well, I guess you could put it like that if you really wanted to...", defaultLetterWait, girlText);
                    break;
                case 61:
                    rightSprite.SetSprite(sprites.hattyWin);
                    DisplayDialogue("And I shall! Now, let me accompany you on your quest!", defaultLetterWait, hattyText);
                    break;
                case 62:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Wait what.", defaultLetterWait, girlText);
                    break;
                case 63:
                    leftSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("You think you can make it out on the road?", defaultLetterWait, buffText);
                    break;
                case 64:
                    DisplayDialogue("I'm pretty well trained and even I'm experiencing some blisters from all this walking!", defaultLetterWait);
                    break;
                case 65:
                    DisplayDialogue("Nonsense companion. No, I want to push my very self to the limits as I lead you to your next opponent.", defaultLetterWait, hattyText);
                    break;
                case 66:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("And who might that be?", defaultLetterWait, girlText);
                    break;
                case 67:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("Your next opponent should not be taken lightly.", defaultLetterWait, hattyText);
                    break;
                case 68:
                    DisplayDialogue("She's the maiden of science, the dynamic discoverer...", defaultLetterWait);
                    break;
                case 69:
                    rightSprite.SetSprite(sprites.hattyFront);
                    DisplayDialogue("The one and only... Professor Sylvia!", defaultLetterWait);
                    break;
                case 70:
                    leftSprite.SetSprite(null);
                    rightSprite.SetSprite(sprites.profLose);
                    scenes.foyerScene.SetActive(false);
                    scenes.labScene.SetActive(true);
                    DisplayDialogue("Ugh, this is going nowhere.", defaultLetterWait, profText);
                    break;
                case 71:
                    DisplayDialogue("Just what is this....", defaultLetterWait);
                    break;
                case 72:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("PROFESSOR SYLVIA! I CHALLENGE YOU TO A VOLLEYBLOCK MATCH!", defaultLetterWait, girlText);
                    break;
                case 73:
                    rightSprite.SetSprite(sprites.profDefeat);
                    DisplayDialogue("Now of all times....", defaultLetterWait, profText);
                    break;
                case 74:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Uh, sorry. Is now a bad time?", defaultLetterWait, girlText);
                    break;
                case 75:
                    leftSprite.SetSprite(sprites.hattyLose);
                    DisplayDialogue("My apologies Professor. I didn't mean to-", defaultLetterWait, hattyText);
                    break;
                case 76:
                    leftSprite.SetSprite(sprites.girlLose);
                    rightSprite.SetSprite(sprites.profSide);
                    DisplayDialogue("HA HA HA. No, now is as fine a time as any.", defaultLetterWait, profText);
                    break;
                case 77:
                    DisplayDialogue("Miss, you have no idea who you're dealing with!", defaultLetterWait);
                    break;
                case 78:
                    leftSprite.SetSprite(sprites.girlWin);
                    rightSprite.SetSprite(sprites.profWin);
                    DisplayDialogue("My data shows that you will never be able to beat me!", defaultLetterWait);
                    break;
                case 79:
                    StartCoroutine(FadeIntoScene(1f, "girl", "prof", "lab", "PROFDOWN", "PROFGIRL", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 80:
                    scenes.labScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlSide);
                    rightSprite.SetSprite(sprites.profLose);
                    DisplayDialogue("Hmph. Very well.", defaultLetterWait, profText);
                    break;
                case 81:
                    DisplayDialogue("Your next opponent is Master Flin atop Mount Block.", defaultLetterWait);
                    break;
                case 82:
                    DisplayDialogue("Congratulations, good luck, and goodbye.", defaultLetterWait);
                    break;
                case 83:
                    rightSprite.ExitStageSide(1f, true);
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("You too...", defaultLetterWait, girlText);
                    break;
                case 84:
                    rightSprite.SetSprite(null);
                    rightSprite.EnterStageBottom(100f);
                    DisplayDialogue("She seemed upset. Was it something I said?", defaultLetterWait);
                    break;
                case 85:
                    rightSprite.SetSprite(sprites.buffWin);
                    DisplayDialogue("Don't worry about it. She's probably just a sore loser.", defaultLetterWait, buffText);
                    break;
                case 86:
                    rightSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("Keep your head clear lass. You can't worry over such trivial matters!", defaultLetterWait, hattyText);
                    break;
                case 87:
                    DisplayDialogue("Yeah. You guys are right. Let's keep going.", defaultLetterWait, girlText);
                    break;
                case 88:
                    DisplayDialogue("    ", defaultLetterWait);
                    leftSprite.ExitStageOppositeSide(1f);
                    rightSprite.ExitStageSide(1f, true);
                    break;
                case 89:
                    DisplayDialogue(". . . ", defaultLetterWait, profText);
                    leftSprite.EnterStageSide(0.4f);
                    leftSprite.SetSprite(sprites.profLose);
                    break;
                case 90:
                    DisplayDialogue("A girl with such talent for the sport...", defaultLetterWait);
                    break;
                case 91:
                    DisplayDialogue("Magical blocks that grant energy when used in a sports-like competition...", defaultLetterWait);
                    break;
                case 92:
                    leftSprite.SetSprite(sprites.profDefeat);
                    DisplayDialogue("This isn't natural...", defaultLetterWait);
                    break;
                case 93:
                    scenes.labScene.SetActive(false);
                    scenes.shrineScene.SetActive(true);
                    leftSprite.EnterStageSide(1f);
                    leftSprite.SetSprite(sprites.girlDefeat);
                    DisplayDialogue("I can't believe Mount Block was actually a mountain...", defaultLetterWait, girlText);
                    break;
                case 94:
                    leftSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("You said yourself that this wasn't going to be easy, lass.", defaultLetterWait, hattyText);
                    break;
                case 95:
                    leftSprite.SetSprite(sprites.buffLose);
                    DisplayDialogue("WHEW... GAH... WAIT UP GUYS... hhhaaaaaaaaaaaa......", defaultLetterWait, buffText);
                    break;
                case 96:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("Are you going to be alright?", defaultLetterWait, girlText);
                    break;
                case 97:
                    leftSprite.SetSprite(sprites.buffLose);
                    DisplayDialogue("Just... not used to... this kind of training...", defaultLetterWait, buffText);
                    break;
                case 98:
                    leftSprite.SetSprite(sprites.hattySide);
                    DisplayDialogue("I recall you questioning my physical abilities earlier, no?", defaultLetterWait, hattyText);
                    break;
                case 99:
                    leftSprite.SetSprite(sprites.buffLose);
                    DisplayDialogue(". . .", defaultLetterWait, buffText);
                    break;
                case 100:
                    rightSprite.EnterStageSide(0.5f);
                    rightSprite.SetSprite(sprites.senseiSide);
                    DisplayDialogue("Who goes there?", defaultLetterWait, senseiText);
                    break;
                case 101:
                    leftSprite.SetSprite(sprites.girlSide);
                    DisplayDialogue("You must be Master Flin! It's an honor sir.", defaultLetterWait, girlText);
                    break;
                case 102:
                    rightSprite.SetSprite(sprites.senseiWin);
                    DisplayDialogue("And what exactly are you doing here at a time like this, child?", defaultLetterWait, senseiText);
                    break;
                case 103:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("What do you mean?", defaultLetterWait, girlText);
                    break;
                case 104:
                    rightSprite.SetSprite(sprites.senseiSide);
                    DisplayDialogue("Can't you feel it? The aura of the blocks...", defaultLetterWait, senseiText);
                    break;
                case 105:
                    DisplayDialogue("It's not natural.", defaultLetterWait);
                    break;
                case 106:
                    leftSprite.SetSprite(sprites.hattySide);
                    rightSprite.SetSprite(sprites.buffSide);
                    DisplayDialogue("PSH. What nonsense! Aura of the blocks?", defaultLetterWait, hattyText);
                    break;
                case 107:
                    DisplayDialogue("He's a sham of a shaman is what he is...", defaultLetterWait);
                    break;
                case 108:
                    rightSprite.SetSprite(sprites.buffWin);
                    DisplayDialogue("If you keep talking, you're going to be the next human sacrifice.", defaultLetterWait, buffText);
                    break;
                case 109:
                    leftSprite.SetSprite(sprites.hattyLose);
                    DisplayDialogue("EEK!", defaultLetterWait, hattyText);
                    break;
                case 110:
                    leftSprite.SetSprite(sprites.girlLose);
                    rightSprite.SetSprite(sprites.senseiSide);
                    DisplayDialogue("Ridicule me all you want, the end is nigh.", defaultLetterWait, senseiText);
                    break;
                case 111:
                    DisplayDialogue("If you want to waste your time on your quest to be some sort of champion, I'll humor you.", defaultLetterWait);
                    break;
                case 112:
                    rightSprite.SetSprite(sprites.senseiWin);
                    DisplayDialogue("But you'd be better off preparing for what's to come...", defaultLetterWait);
                    break;
                case 113:
                    StartCoroutine(FadeIntoScene(1f, "girl", "sensei", "shrine", "WISEDOWN", "WISEDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 114:
                    scenes.shrineScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlSide);
                    rightSprite.SetSprite(sprites.senseiSide);
                    DisplayDialogue("I hope I could help you somewhat. It's all become very clear to me...", defaultLetterWait, senseiText);
                    break;
                case 115:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("What do you mean?", defaultLetterWait, girlText);
                    break;
                case 116:
                    rightSprite.SetSprite(sprites.senseiWin);
                    DisplayDialogue("It's all coming undone. The seams are showing and the end is in sight.", defaultLetterWait, senseiText);
                    break;
                case 117:
                    DisplayDialogue("I pray that peace awaits us.", defaultLetterWait);
                    break;
                case 118:
                    DisplayDialogue("What do you mean?! What's happening?!", defaultLetterWait, girlText);
                    break;
                case 119:
                    DisplayDialogue("AAAAAAAAAAAHHHHHHHHHHHHHH", defaultLetterWait);
                    break;
                case 120:
                    scenes.shrineScene.SetActive(false);
                    scenes.voidScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlDefeat);
                    rightSprite.SetSprite(null);
                    localCam.backgroundColor = Color.black;
                    DisplayDialogue(". . . ", defaultLetterWait, girlText);
                    break;
                case 121:
                    DisplayDialogue(". . . . .", defaultLetterWait);
                    break;
                case 122:
                    DisplayDialogue(". . . . . . .", defaultLetterWait);
                    break;
                case 123:
                    DisplayDialogue("Ohhhhhhhh.....", defaultLetterWait);
                    break;
                case 124:
                    DisplayDialogue("Where am I?", defaultLetterWait);
                    break;
                case 125:
                    rightSprite.SetSprite(sprites.rawBuffSide);
                    DisplayDialogue("ARE YOU LOOKING TO BETTER YOURSELF?", defaultLetterWait, Color.black);
                    break;
                case 126:
                    leftSprite.SetSprite(sprites.girlLose);
                    DisplayDialogue("What? Who are you?", defaultLetterWait, girlText);
                    break;
                case 127:
                    StartCoroutine(FadeIntoScene(1f, "girl", "buffRaw", "black", "WISEDOWN", "WISEDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 128:
                    scenes.voidScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlLose);
                    rightSprite.SetSprite(sprites.rawHattySide);
                    localCam.backgroundColor = Color.black;
                    DisplayDialogue("DO YOU FEEL LIKE YOU ARE LACKING IN CERTAIN AREAS?", defaultLetterWait, Color.black);
                    break;
                case 129:
                    DisplayDialogue("Uh... I don't understand what's happening here.", defaultLetterWait, girlText);
                    break;
                case 130:
                    StartCoroutine(FadeIntoScene(1f, "girl", "hattyRaw", "black", "WISEDOWN", "WISEDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 131:
                    scenes.voidScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlLose);
                    rightSprite.SetSprite(sprites.rawProfSide);
                    localCam.backgroundColor = Color.black;
                    DisplayDialogue("HAVE YOU EVER FELT LOST?", defaultLetterWait, Color.black);
                    break;
                case 132:
                    leftSprite.SetSprite(sprites.girlDefeat);
                    DisplayDialogue("Please, what is happening here?", defaultLetterWait, girlText);
                    break;
                case 133:
                    StartCoroutine(FadeIntoScene(1f, "girl", "profRaw", "black", "WISEDOWN", "WISEDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 134:
                    scenes.voidScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlDefeat);
                    rightSprite.SetSprite(sprites.rawSenseiSide);
                    localCam.backgroundColor = Color.black;
                    DisplayDialogue("DO YOU EVER FEAR THE FUTURE?", defaultLetterWait, Color.black);
                    break;
                case 135:
                    DisplayDialogue("Please stop...", defaultLetterWait, girlText);
                    break;
                case 136:
                    StartCoroutine(FadeIntoScene(1f, "girl", "senseiRaw", "black", "WISEDOWN", "WISEDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 137:
                    scenes.voidScene.SetActive(true);
                    leftSprite.SetSprite(sprites.girlDefeat);
                    rightSprite.SetSprite(sprites.meSideRaw);
                    localCam.backgroundColor = Color.black;
                    localCam.orthographic = false;
                    DisplayDialogue("No more, please...", defaultLetterWait, girlText);
                    break;
                case 138:
                    DisplayDialogue("I have no idea what's happening... I just want to go home...", defaultLetterWait);
                    break;
                case 139:
                    rightSprite.SetSprite(sprites.meSideColor);
                    DisplayDialogue("What are you doing here?", defaultLetterWait, Color.black);
                    break;
                case 140:
                    DisplayDialogue("Honestly, I want to know.", defaultLetterWait);
                    break;
                case 141:
                    DisplayDialogue("Well you see, I'm looking to be the volleybl-", defaultLetterWait, girlText);
                    break;
                case 142:
                    DisplayDialogue("Not you.", defaultLetterWait, Color.black);
                    break;
                case 143:
                    DisplayDialogue("I don't care about your little adventure to become volleyblock champion or whatever dumb ideas you had.", defaultLetterWait);
                    break;
                case 144:
                    rightSprite.SetSprite(sprites.meFrontColor);
                    DisplayDialogue("I'm talking to YOU.", defaultLetterWait);
                    break;
                case 145:
                    rightSprite.transform.position = new Vector3(0f, rightSprite.transform.position.y, transform.position.z);
                    DisplayDialogue("What are you even doing here, honestly?", defaultLetterWait);
                    break;
                case 146:
                    DisplayDialogue("Can't you see that I've run out of patience here? Just look at the last few matches...", defaultLetterWait);
                    break;
                case 147:
                    DisplayDialogue("Did you really enjoy the poorly constucted narrative I wove together?", defaultLetterWait);
                    break;
                case 148:
                    DisplayDialogue("Did you enjoy looking at the childish art that I made simply because something needed to be there?", defaultLetterWait);
                    break;
                case 149:
                    DisplayDialogue("Was it actually the gameplay, which I'm pretty sure has a bunch of bugs in terms of how combos are counted?", defaultLetterWait);
                    break;
                case 150:
                    DisplayDialogue("I really wanted to make something here, you know. I believed in it at first.", defaultLetterWait);
                    break;
                case 151:
                    DisplayDialogue("My mind's always racing with game ideas whenever I see other people's projects.", defaultLetterWait);
                    break;
                case 152:
                    DisplayDialogue("I go into overdrive and try to think of how I could make a game like that, what kind of cool mechanics it would use...", defaultLetterWait);
                    break;
                case 153:
                    DisplayDialogue("And the one thing they all have in common is that they're too ambitious. They're just fantasies, dreams.", defaultLetterWait);
                    break;
                case 154:
                    DisplayDialogue("I'd never finish anything. I've hardly finished anything.", defaultLetterWait);
                    break;
                case 155:
                    DisplayDialogue("So, one day, I simply told myself this: what's the smallest game idea you can actually finish?", defaultLetterWait);
                    break;
                case 156:
                    DisplayDialogue("And that's what this is.", defaultLetterWait);
                    break;
                case 157:
                    DisplayDialogue("It's a simple concept, but it can be fleshed out to multiple game modes.", defaultLetterWait);
                    break;
                case 158:
                    DisplayDialogue("Heck, why do you think there's an arcade mode? This is the fleshed out part.", defaultLetterWait);
                    break;
                case 159:
                    DisplayDialogue("But of course, I started going crazy with ideas that would never be implemented.", defaultLetterWait);
                    break;
                case 160:
                    DisplayDialogue("I think I wanted to do a 3D overworld at one point, with 3D characters and cutscenes.", defaultLetterWait);
                    break;
                case 161:
                    DisplayDialogue("But finishing even the smallest, crappiest game takes way longer than you expect.", defaultLetterWait);
                    break;
                case 162:
                    DisplayDialogue("At the end of the day, you get burnt out. This is the best I could do.", defaultLetterWait);
                    break;
                case 163:
                    DisplayDialogue("But I did believe in it at first.", defaultLetterWait);
                    break;
                case 164:
                    DisplayDialogue("I have dreams of making a big game, one that I'm proud of.", defaultLetterWait);
                    break;
                case 165:
                    DisplayDialogue("Instead, I feel like I've been battling with imposter syndrome as of recently.", defaultLetterWait);
                    break;
                case 166:
                    DisplayDialogue("I mean, how did everyone online get so good at making games?", defaultLetterWait);
                    break;
                case 167:
                    DisplayDialogue("I tell myself that you just need to keep chugging along and that one day you'll get there.", defaultLetterWait);
                    break;
                case 168:
                    DisplayDialogue("I think I've made great progress and I learned a decent amount making this game.", defaultLetterWait);
                    break;
                case 169:
                    DisplayDialogue("But it's still disheartening, you know?", defaultLetterWait);
                    break;
                case 170:
                    DisplayDialogue("Like, I feel as if my life is going by so fast and this is all I have to show for it.", defaultLetterWait);
                    break;
                case 171:
                    DisplayDialogue("Well, venting about my life problems is a story for another game, but I worry about my future as a game maker.", defaultLetterWait);
                    break;
                case 172:
                    DisplayDialogue("Obviously, I'm not too satisfied with this one. I think I'm only releasing it because I've put way too much time into it.", defaultLetterWait);
                    break;
                case 173:
                    DisplayDialogue("Putting it out there will let me move on, in a sense, no matter how much people like or dislike the final product.", defaultLetterWait);
                    break;
                case 174:
                    DisplayDialogue("Maybe I can just move on.", defaultLetterWait);
                    break;
                case 175:
                    DisplayDialogue("Again, and I'm so sorry that your final boss is literally just an angsty guy venting, but I'm not satisfied with the game.", defaultLetterWait);
                    break;
                case 176:
                    DisplayDialogue("Kinda depressing since I tried so hard at one point.", defaultLetterWait);
                    break;
                case 177:
                    DisplayDialogue("There's so much to this game that's hidden away from view, but in the end the game by itself still sucks.", defaultLetterWait);
                    break;
                case 178:
                    DisplayDialogue("I realized that when I was playtesting my own game and found out that I wasn't having fun.", defaultLetterWait);
                    break;
                case 179:
                    DisplayDialogue("But at that point, I was too far in. What was I supposed to do?", defaultLetterWait);
                    break;
                case 180:
                    DisplayDialogue("I wanted to finish something and this is the best I could do I guess.", defaultLetterWait);
                    break;
                case 181:
                    DisplayDialogue("So that's why I'm asking you, why are you still here?", defaultLetterWait);
                    break;
                case 182:
                    DisplayDialogue("Did you enjoy it that much? I can't imagine you did.", defaultLetterWait);
                    break;
                case 183:
                    DisplayDialogue("So. Here I am as your last volleyblock master.", defaultLetterWait);
                    break;
                case 184:
                    DisplayDialogue("I guess I'll have to give you what you're looking for if you beat me.", defaultLetterWait);
                    break;
                case 185:
                    DisplayDialogue("Can't imagine it'll leave a lasting impression on you once you're done, but I take it you're not leaving.", defaultLetterWait);
                    break;
                case 186:
                    DisplayDialogue("Sorry for the rant.", defaultLetterWait);
                    break;
                case 187:
                    rightSprite.SetSprite(sprites.meFrontRaw);
                    DisplayDialogue("Ready?", defaultLetterWait);
                    break;
                case 188:
                    StartCoroutine(FadeIntoScene(1f, "girl", "me", "glitch", "WISEDOWN", "WISEDUDE", "Player VS CPU", 5f, 1, 1, globalSceneIndex, true));
                    break;
                case 189:
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

    void DisplayDialogue(string text, float letterWaitTime, Color textColor)
    {
        DisplayDialogue(text, letterWaitTime);
        mainText.color = textColor;
    }

    IEnumerator FadeIntoScene(float time, string leftBoardChar, string rightBoardChar, string sceneName, string winPassword, string losePassword, string playerCase, float newTurnLength, int gamesCount, int setsCount, int storyIndex, bool isStory)
    {
        canAdvanceText = false;
        StartCoroutine(fadeSmart.StartFade(time));
        yield return new WaitForSeconds(time);
        selectedChars.SetLeftBoard(leftBoardChar);
        selectedChars.SetRightBoard(rightBoardChar);
        selectedScene.SetSceneName(sceneName);
        passedObject.GetComponent<Passed>().StorePasswords(winPassword, losePassword);
        loadBattle.LoadNewBattle(playerCase, newTurnLength, gamesCount, setsCount, storyIndex, isStory);
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
