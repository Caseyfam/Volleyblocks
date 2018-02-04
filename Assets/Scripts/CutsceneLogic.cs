using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLogic : MonoBehaviour
{
    public SpriteEmotes leftSprite, rightSprite;
    public UnityEngine.UI.Text mainText;

    private float defaultLetterWait = 0.03f;

    [System.Serializable]
    public class Scenes
    {
        public GameObject beachScene, shrineScene;
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

    private int globalSceneIndex = 0;

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
                    break;
                case 20:
                    break;
                    // Fade from black to house
                    // Girl:
                    // Ok, I'll be careful. Bye everyone!
                    // All my years of training have prepared me for this
                    // moment, to take down the 4 volleyblock masters
                    // who stand in my way of being the volleyblock regional champion!

                    // Silhouttes of the masters

                    // To think that there are masters of the sport that
                    // remain elusive from the public eye. What secrets could
                    // they be keeping, what techniques have they mastered?
                    // I have to know, I have to learn! I need to be the best!
                    // Alright, first up we have Beach Boy Bobby. Let's go!

                    // Cut to beach scene

                    // Bobby:
                    // HRAH, HUT, HRAH, GRAHHHHHHH
                    // Whew, what a workout. Volleyblocks totally makes you work, huh?
                    // Girl:
                    // BEACH BOY BOBBY!
                    // Bobby:
                    // Huh?
                    // Girl:
                    // I challenge you to a duel for your badge! Let me prove I have
                    // what it takes to be the volleyblocks champion?
                    // Bobby:
                    // ...
                    // HAAAAAAAAAAAAAA
                    // FINALLY, ONE WHO DECLARES THEMSELVES A WORTHY CHALLENGER?
                    // HO HA HOOOOOOOOO
                    // LET'S GO. PROVE TO ME YOU HAVE WHAT IT TAKES TO BE
                    // A VOLLEYBLOCK MASTER

                    // Battle

                    // Bobby:
                    // Whewwww... wow..... That was pretty good kid....
                    // Wowww.... outta breath.... easy there Bobby...
                    // Girl:
                    // Uh, are you going to be ok?
                    // Bobby:
                    // YES. WHEW! WOW. What a battle, what a thrill! That isn't something
                    // you can just get training alone. I've been waiting for this day.
                    // Girl, let me join you on your quest?
                    // Girl: 
                    // Huh ?
                    // Bobby:
                    // Please! I have a feeling that following you will lead me to other
                    // warriors and masters of volleyblocks. There's so much I need to learn
                    // that long runs on the beach won't bring me!
                    // Girl:
                    // Well, I guess it couldn't hurt. Let's go!
                    // Bobby:
                    // Considering how elusive us masters are supposed to be, I doubt you
                    // thought of who you'd be fighting next?
                    // Girl:
                    // Yeah... you're right.
                    // Bobby:
                    // Every master holds the name of the next. Since I have lost, I guess
                    // I have no choice but to reveal my most closely guarded secret.
                    // The name of your next opponnet is... Sir Kensington!

                    // Cut to mansion scene

                    // Girl:
                    // Wow, he actually has a butler? Who has a butler nowadays?
                    // Bobby:
                    // I should get me one of those to bring me cold protein shakes
                    // after my morning jogs.
                    // ???:
                    // AH, THE FORMIDABLE FLOWER EMERGES
                    // Girl:
                    // Huh?
                    // Sir:
                    // WELCOME TO MY HUMBLE ABODE!
                    // The name's Kensington, but that's Sir Kensington to you.
                    // That is, of course, unless you can prove your mettle child!
                    // Girl:
                    // Do you take me for a volleyblock chump?
                    // Sir:
                    // While looks can be deceiving, they can also reveal deep truths!
                    // I have no doubt that this will be a fun match, but it will be
                    // a match you shall lose!

                    // Battle

                    // Sir:
                    // How exquisite!
                    // Girl:
                    // HYAH!
                    // Sir:
                    // How divine!
                    // Girl:
                    // HRAH!
                    // Sir:
                    // I GIVE. What an excellent performance miss. I'm truly taken aback.
                    // Girl:
                    // Why thank you. You weren't too bad yourself.
                    // Sir:
                    // No, I lack in many areas. Being brought up with wealth as my ally
                    // has made me worse for the wear. I've been coddled in these
                    // niceities for far too long. But look at you! Clawing your way
                    // from the bottom to achieve your goals! It is truly inspiring.
                    // Girl:
                    // Well, I guess you could put it like that if you really wanted to...
                    // Sir:
                    // And I shall! Now, I shall also accompany you on your quest!
                    // Girl:
                    // Wait what.
                    // Bobby:
                    // You think you can make it out on the road? I'm pretty well trained and
                    // even I'm experiencing some blisters from all this walking
                    // Sir:
                    // Nonsense, my companion. No, I want to push my very self to the limits
                    // as I lead you to your next opponent.
                    // Girl:
                    // And who might that be?
                    // Sir:
                    // Your next opponent will not be taken lightly. She's the maiden of science,
                    // the dynamic discoverer, the one and only: Professor Sylvia!
                    
                    
            }
        }
        if (!textCrawling)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                sectionComplete = true;
                globalSceneIndex++;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
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
