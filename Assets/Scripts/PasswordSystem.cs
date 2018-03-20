using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordSystem : MonoBehaviour {

    private string password = "";
    public UnityEngine.UI.Image[] letters;
    public Sprite[] alphabet;

    public Passed passed;
    public FadeSmart smartFade;

    private bool canEnterPassword = true;

    public void EnterPassword()
    {
        // Load to various scenes here
    }

    public void AddToPassword(string letter)
    {
        password += letter;
        if (password.Length > 8)
        {
            string newPassword = "";
            for (int i = 1; i < 9; i++)
            {
                newPassword += password[i];
            }
            password = newPassword;
        }

        for (int i = 0; i < 8; i++)
        {
            // Set Letter
            try
            {
                SetLetter(password[i], i);
            }
            catch
            {
                // Part of password not created yet
            }
        }
    }

    public void SetLetter(char letter, int currentLetter)
    {
        int index = System.Convert.ToInt32(letter) - 65;
        letters[currentLetter].sprite = alphabet[index];
    }

    public void ClearPassword()
    {
        password = "";
        foreach (UnityEngine.UI.Image letter in letters)
        {
            letter.sprite = null;
        }
    }

    public void PasswordConfirm()
    {
        if (canEnterPassword)
        {
            bool isStory = true;
            int sceneIndex = 0;
            switch (password)
            {
                default:
                    Debug.Log("INVALID PASSWORD");
                    ClearPassword();
                    isStory = false;
                    break;
                case "BUFFDUDE":
                    sceneIndex = 17;
                    break;
                case "BUFFDOWN":
                    sceneIndex = 20;
                    break;
                case "RICHDUDE":
                    sceneIndex = 48;
                    break;
                case "RICHDOWN":
                    sceneIndex = 50;
                    break;
                case "PROFGIRL":
                    sceneIndex = 78;
                    break;
                case "PROFDOWN":
                    sceneIndex = 80;
                    break;
                case "GURUDUDE":
                    sceneIndex = 112;
                    break;
                case "GURUDOWN":
                    sceneIndex = 114;
                    break;
                case "ICANTWIN":
                    sceneIndex = 125;
                    break;
                case "MAYBENOT":
                    sceneIndex = 128;
                    break;
                case "HESEMPTY":
                    sceneIndex = 128;
                    break;
                case "JUSTSTOP":
                    sceneIndex = 131;
                    break;
                case "GUIDEHIM":
                    sceneIndex = 131;
                    break;
                case "GUIDEDME":
                    sceneIndex = 134;
                    break;
                case "IMAFRAID":
                    sceneIndex = 134;
                    break;
                case "ICANWALK":
                    sceneIndex = 137;
                    break;
                case "PLEASENO":
                    sceneIndex = 137;
                    break;
                case "THANKYOU":
                    sceneIndex = 186;
                    break;
                case "CYALATER":
                    sceneIndex = 189;
                    break;
            }

            if (isStory)
            {
                passed.isStory = isStory;
                passed.storyIndex = sceneIndex;
                StartCoroutine(WaitToLoadStory(1f));
            }
        }
    }

    IEnumerator WaitToLoadStory(float time)
    {
        smartFade.StartFadeNoEnd(0.14f);
        yield return new WaitForSeconds(time);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
