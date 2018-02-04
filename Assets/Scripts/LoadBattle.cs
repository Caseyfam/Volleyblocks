using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBattle : MonoBehaviour
{
    public bool useOldAI = false;
    public float turnLength = 1f;

    public void LoadNewBattle(string playerCase, float newTurnLength, int gamesCount, int setsCount)
    {
        try
        {
            GameObject.Find("PassedObject").GetComponent<Passed>().StoreValues(playerCase, newTurnLength, gamesCount, setsCount);
        }
        catch
        {
            Debug.LogError("PassedObject does not exist. Did you not load from Menu?");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadNewBattle(string playerCase, float newTurnLength, int gamesCount, int setsCount, int storyIndex)
    {
        try
        {
            GameObject.Find("PassedObject").GetComponent<Passed>().StoreValues(playerCase, newTurnLength, gamesCount, setsCount, storyIndex);
        }
        catch
        {
            Debug.LogError("PassedObject does not exist. Did you not load from Menu?");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // When called, load volley scene.
    // Save gameObject info.
    // Make sure empty isn't destroyed with this script.
    // In volley scene, set if playerWon.
    // Return to main script and do stuff.
}
