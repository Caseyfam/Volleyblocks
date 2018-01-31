using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordSystem : MonoBehaviour {

    private string password = "";
    public UnityEngine.UI.Image[] letters;
    public Sprite[] alphabet;

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
        Debug.Log(password);
    }

    public void SetLetter(char letter, int currentLetter)
    {
        int index = System.Convert.ToInt32(letter) - 65;
        letters[currentLetter].sprite = alphabet[index];
        Debug.Log(index);
    }

    public void ClearPassword()
    {
        password = "";
    }
}
