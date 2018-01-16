using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordSystem : MonoBehaviour {

    private string password = "";

    public void EnterPassword()
    {
        // Load to various scenes here
    }

    public void AddToPassword(string letter)
    {
        password += letter;
        Debug.Log(password);
    }

    public void ClearPassword()
    {
        password = "";
    }
}
