using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordLetter : MonoBehaviour {

    private PasswordSystem system;
    public string letter;

    private void Awake()
    {
        system = GameObject.Find("MenuLogic").GetComponent<PasswordSystem>();
    }

    public void PassLetter()
    {
        system.AddToPassword(letter);
    }
}
