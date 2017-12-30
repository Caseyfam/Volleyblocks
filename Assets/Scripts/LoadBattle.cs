using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBattle : MonoBehaviour
{
    public bool useOldAI = false;
    public float turnLength = 1f;

    public void LoadNewBattle(GameObject player)
    {
        GameObject.Find("PassedObject").GetComponent<PassedAI>().StoreValues(useOldAI, turnLength);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadNewBattle(float newTurnLength)
    {
        GameObject.Find("PassedObject").GetComponent<PassedAI>().StoreValues(useOldAI, newTurnLength);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // When called, load volley scene.
    // Save gameObject info.
    // Make sure empty isn't destroyed with this script.
    // In volley scene, set if playerWon.
    // Return to main script and do stuff.
}
