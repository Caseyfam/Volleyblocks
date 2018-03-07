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
                case "BUFFDOWN":
                    sceneIndex = 20;
                    break;
                case "BUFFDUDE":
                    sceneIndex = 17;
                    break;
                case "RICHDOWN":
                    sceneIndex = 50;
                    break;
                case "RICHDUDE":
                    sceneIndex = 48;
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
