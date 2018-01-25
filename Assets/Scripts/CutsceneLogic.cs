using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLogic : MonoBehaviour
{
    public SpriteEmotes leftSprite, rightSprite;
    public UnityEngine.UI.Text mainText;

    private float defaultFontSize = 26f;
    private bool dialogueDoneDisplaying = true;

    private bool textCrawling = false;
    private bool sectionComplete = true;

    private int globalSceneIndex = 0;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (sectionComplete)
        {
            switch (globalSceneIndex)
            {
                default:
                case 0:
                    DisplayDialogue("This is a test thing!", 0.03f);
                    break;
                case 1:
                    DisplayDialogue("Round 2 baby!", 0.03f);
                    break;
                case 2:
                    DisplayDialogue("Round 3 why not?", 0.03f);
                    break;
                    // Fade from black to house
                    // Girl:
                    // Ahhhhh, what a lovely morning! The perfect morning
                    // to take on the volleyblock champions of the world!
                    // All my years of training have prepared me for this
                    // moment, to take down the 8 volleyblock masters
                    // who stand in my way of being the champion!

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
