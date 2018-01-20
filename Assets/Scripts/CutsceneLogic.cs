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
