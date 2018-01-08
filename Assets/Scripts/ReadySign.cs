using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadySign : MonoBehaviour {

    public Sprite ready, serve;

    public void Reset()
    {
        GetComponent<SpriteRenderer>().sprite = ready;
        GetComponent<Animator>().SetBool("MoveOut", false);
    }

    public void SwitchSign()
    {
        GetComponent<SpriteRenderer>().sprite = serve;
        GetComponent<Animator>().SetBool("MoveOut", true);
        // Move out
        // Start game
    }
}
